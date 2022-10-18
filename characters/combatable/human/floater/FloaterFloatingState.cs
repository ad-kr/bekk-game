using Godot;
using System;

namespace ADKR.Game
{
    public partial class FloaterFloatingState : CharacterState
    {

        private Tween _tween;

        public override async void Start()
        {
            base.Start();

            Vector2 startPos = Char.Sprite.Position;

            await Char.ToSignal(Char.GetTree().CreateTimer(GD.RandRange(0.1f, 2f)), "timeout");

            _tween = Char.CreateTween().SetLoops();
            _tween.TweenProperty(Char.Sprite, "position", startPos + new Vector2(0f, 2f), 3f);
            _tween.TweenProperty(Char.Sprite, "position", startPos - new Vector2(0f, 2f), 3f);
        }

        public override void End()
        {
            base.End();
            _tween.Kill();
        }
    }
}