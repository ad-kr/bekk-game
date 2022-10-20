using Godot;
using System;

namespace ADKR.Game
{
    public abstract partial class Interactable : Area2D
    {
        private Node2D _label;
        private bool _isPlayerEntered;
        private Tween _tween;

        public string InteractionText { get; set; } = "";

        public override void _Ready()
        {
            base._Ready();

            _label = ResourceLoader.Load<PackedScene>("res://tileset/interactable/InteractableLabel.tscn").Instantiate<Node2D>();
            _label.GetChild<RichTextLabel>(0).Text = $"[center]{InteractionText}[color=#40E2A0]©[/color]";
            _label.Modulate = Colors.Transparent;
            _label.Position = new Vector2(0f, -20f);

            AddChild(_label);

            Connect("body_entered", new Callable(this, nameof(OnEnter)));
            Connect("body_exited", new Callable(this, nameof(OnExit)));
        }

        public override void _Input(InputEvent e)
        {
            base._Input(e);
            if (e.IsActionPressed("interact") && _isPlayerEntered) Execute();
        }

        public override void _ExitTree()
        {
            base._ExitTree();
            Disconnect("body_entered", new Callable(this, nameof(OnEnter)));
            Disconnect("body_exited", new Callable(this, nameof(OnExit)));
        }

        private void OnEnter(Node body)
        {
            if (body is not Player) return;
            _isPlayerEntered = true;
            _tween?.Kill();
            _tween = CreateTween().SetParallel().SetEase(Tween.EaseType.Out);
            _tween.TweenProperty(_label, "modulate", Colors.White, 0.1f);
            _tween.TweenProperty(_label, "position", new Vector2(0f, -32f), 0.1f);
        }

        private void OnExit(Node body)
        {
            if (body is not Player) return;
            _isPlayerEntered = false;
            _tween?.Kill();
            _tween = CreateTween().SetParallel();
            _tween.TweenProperty(_label, "modulate", Colors.Transparent, 0.1f);
            _tween.TweenProperty(_label, "position", new Vector2(0f, -20f), 0.1f);
        }

        protected abstract void Execute();
    }
}