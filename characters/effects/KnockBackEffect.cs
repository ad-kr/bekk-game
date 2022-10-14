using Godot;
using System;
using ADKR.Extensions;

namespace ADKR.Game
{
    public partial class KnockBackEffect : CharacterEffect
    {
        private Vector2 _dir;

        public KnockBackEffect(Vector2 dir)
        {
            _dir = dir;
            IsUnique = true;
        }

        public override async void Start()
        {
            base.Start();

            float _prev = Time.GetTicksMsec();
            Vector2 impulse = Vector2.Zero;

            Tween tween = Char.CreateTween();
            tween.TweenMethod(v =>
            {
                float value = (float)v;
                float delta = Time.GetTicksMsec() - _prev;
                _prev = Time.GetTicksMsec();
                delta /= 1000f;

                Char.Velocity -= impulse;
                impulse = _dir * Char.RunSpeed * 500f * value * delta;

                Char.Velocity += impulse;
                Char.MoveAndSlide();
            }, 1f, 0f, 0.3f);

            await Char.ToSignal(tween, "finished");

            Char.RemoveEffect(this);
        }
    }
}