using Godot;
using System.Linq;
using System.Collections.Generic;
using System;

namespace ADKR.Game
{
    public partial class DialogueBox : NinePatchRect
    {
        public static DialogueBox Instance { get; set; }

        private const float RevealDuration = 0.5f;
        private const float PrintSpeed = 30f;

        private RichTextLabel _label;

        private readonly Queue<string> _texts = new();

        private Action _onDone;

        public DialogueBox()
        {
            Instance = this;
            Visible = false;
            Scale = new Vector2(0.0f, 1f);
        }

        public override void _Ready()
        {
            base._Ready();
            _label = GetNode<RichTextLabel>("Text");
        }

        public static async void Talk(params string[] text)
        {
            if (Instance.Visible) return;
            Instance._label.Text = "";

            foreach (string s in text) Instance._texts.Enqueue(s);

            await Open();

            ExecuteNext();
        }

        public static void Talk(Action onDone, params string[] text)
        {
            Instance._onDone = onDone;
            Talk(text);
        }

        private static async void ExecuteNext()
        {
            Instance._label.Text = Instance._texts.Dequeue();
            Instance._label.VisibleRatio = 0f;
            float printDuration = Instance._label.Text.Length / PrintSpeed;

            Tween tween = Instance.CreateTween();
            tween.TweenProperty(Instance._label, "visible_ratio", 1f, printDuration);

            await Instance.ToSignal(tween, "finished");

            float delay = Instance._label.Text.Length * 0.1f;
            delay = Mathf.Max(delay, 2f);

            await Instance.ToSignal(Instance.GetTree().CreateTimer(delay), "timeout");

            if (Instance._texts.Count > 0)
            {
                ExecuteNext();
                return;
            }

            Close(Instance._onDone);
        }

        private static SignalAwaiter Open()
        {
            if (Instance.Visible) return null;

            Instance.Visible = true;
            Instance.GetTree().Paused = true;

            Tween tween = Instance.CreateTween();
            tween.TweenProperty(Instance, "scale", Vector2.One, RevealDuration);

            return Instance.ToSignal(tween, "finished");
        }

        private static async void Close(Action onClose = null)
        {
            if (!Instance.Visible) return;

            Tween tween = Instance.CreateTween();
            tween.TweenProperty(Instance, "scale", new Vector2(0.0f, 1f), RevealDuration);

            await Instance.ToSignal(tween, "finished");
            Instance.Visible = false;
            Instance.GetTree().Paused = false;
            onClose?.Invoke();
        }
    }
}