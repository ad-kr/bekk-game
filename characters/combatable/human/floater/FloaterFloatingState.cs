using Godot;
using System;

namespace ADKR.Game
{
    public partial class FloaterFloatingState : CharacterState
    {

        private Tween _tween;

        public override void Start()
        {
            base.Start();

            Vector2 startPos = Char.Sprite.Position;

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