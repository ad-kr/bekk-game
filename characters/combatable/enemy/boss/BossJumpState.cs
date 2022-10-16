using Godot;
using System;
using System.Linq;

namespace ADKR.Game
{
    public class BossJumpState : CharacterState<Enemy>
    {

        private float _time;

        public override async void Start()
        {
            base.Start();
            _time = 0f;

            Char.Sprite.Frame = 0;
            Char.Sprite.Animation = "jump";
            Char.Sprite.Playing = true;
            Char.ZIndex = 10;
            Char.Invincible = true;

            await Char.ToSignal(Char.Sprite, "frame_changed");

            Tween tween = Char.CreateTween();
            tween.TweenProperty(Char.Sprite, "position", new Vector2(0f, -128f), 0.5f).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.Out);
            tween.TweenProperty(Char.Sprite, "position", new Vector2(0f, 0f), 0.5f).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.In);

            await Char.ToSignal(tween, "finished");

            Char.Sprite.Animation = "land";
            Char.Sprite.Frame = 0;

            if (!Char.IsDead) Char.State = new BossLandState();
        }

        public override void Update(double delta)
        {
            base.Update(delta);

            // _time += (float)delta;
            // if (_time * 1000f > 200f)
            // {
            //     _time = 0f;
            //     Char.NavigationAgent.SetTargetLocation(Char.Target.Position);
            // }

            // if (Char.Velocity.Length() > Char.RunSpeed) Char.Velocity = Char.Velocity.Normalized() * Char.RunSpeed;

            // Vector2 nextPos = Char.NavigationAgent.GetNextLocation();
            // Vector2 dir = Char.GlobalPosition.DirectionTo(nextPos);
            // Vector2 desiredVelocity = dir * Char.RunSpeed;
            // Vector2 steering = (desiredVelocity - Char.Velocity) * (float)delta;


            // Char.Velocity += steering;

            Char.Velocity = Char.Position.DirectionTo(Char.Target.Position) * Char.RunSpeed;

            Char.MoveAndSlide();
        }
    }
}