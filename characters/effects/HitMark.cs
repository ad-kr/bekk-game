using Godot;
using System;

namespace ADKR.Game
{
    public partial class HitMark : CharacterEffect
    {
        private readonly float _damage;

        public HitMark(float damage)
        {
            _damage = damage;
        }

        public override async void Start()
        {
            base.Start();

            RichTextLabel label = ResourceLoader.Load<PackedScene>("res://characters/effects/HitMark.tscn").Instantiate<RichTextLabel>();
            label.Text = Mathf.Round(_damage).ToString();
            Node2D labelContainer = new()
            {
                ZIndex = 1
            };

            labelContainer.AddChild(label);
            Char.GetParent().AddChild(labelContainer);
            labelContainer.Position = Char.Position;

            Tween tween = Char.CreateTween().SetEase(Tween.EaseType.Out);
            tween.TweenProperty(label, "position", label.Position - new Vector2(0f, 20f), 1f);

            await Char.ToSignal(tween, "finished");
            label.QueueFree();

            Char.RemoveEffect(this);
        }
    }
}