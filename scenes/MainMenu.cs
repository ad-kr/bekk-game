using Godot;
using System;

namespace ADKR.Game
{
    public partial class MainMenu : Control
    {
        private RichTextLabel _label;

        public override void _Ready()
        {
            _label = GetNode<RichTextLabel>("RichTextLabel");
            Tween tween = CreateTween();
            tween.SetLoops();
            tween.TweenProperty(_label, "modulate", new Color(Colors.White, 0.3f), 0.4f);
            tween.TweenProperty(_label, "modulate", Colors.White, 0.4f);
            GetTree().Paused = false;
        }

        public override void _Input(InputEvent e)
        {
            base._Input(e);

            if (e.IsActionPressed("interact")) Game.LoadRandomizer();
        }
    }
}