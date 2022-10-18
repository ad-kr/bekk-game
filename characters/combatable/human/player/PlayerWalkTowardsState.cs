using Godot;
using ADKR.Extensions;
using System;

namespace ADKR.Game
{
    public class PlayerWalkTowardsState : CharacterState<Player>
    {

        private Vector2 _target;

        private const float WalkSpeed = 32f;

        private readonly Action _callback;

        public PlayerWalkTowardsState(Vector2 position, Action callback = null)
        {
            _target = position;
            _callback = callback;
        }

        public override async void Start()
        {
            base.Start();

            float distance = Char.Position.DistanceTo(_target);
            float duration = distance / WalkSpeed;

            Char.Sprite.Frame = 1;
            Char.Hair.Frame = 1;
            Char.Clothes.Frame = 1;
            Char.Sprite.Playing = true;
            Char.Hair.Playing = true;
            Char.Clothes.Playing = true;

            Char.IsFlipped = IsFlipped(Char.Position.DirectionTo(_target));

            Tween tween = Char.CreateTween();
            tween.TweenProperty(Char, "position", _target, duration).SetEase(Tween.EaseType.InOut);

            await Char.ToSignal(tween, "finished");

            Char.Sprite.Playing = false;
            Char.Sprite.Frame = 0;
            Char.Hair.Playing = false;
            Char.Hair.Frame = 0;
            Char.Clothes.Playing = false;
            Char.Clothes.Frame = 0;

            _callback?.Invoke();
        }

        private bool IsFlipped(Vector2 dir)
        {
            if (dir.x == 0) return Char.Sprite.FlipH;
            if (dir.x > 0f) return false;
            return true;
        }
    }
}