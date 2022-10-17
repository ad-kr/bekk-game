using Godot;
using System;
using System.Linq;

namespace ADKR.Game
{
    public class BossJumpState : CharacterState<Enemy>
    {
        public override async void Start()
        {
            base.Start();

            Char.Sprite.Frame = 0;
            Char.Sprite.Animation = "jump";
            Char.Sprite.Playing = true;
            Char.ZIndex = 9;
            Char.Invincible = true;

            await Char.ToSignal(Char.Sprite, "frame_changed");

            Tween tween = Char.CreateTween();
            tween.TweenProperty(Char.Sprite, "position", new Vector2(0f, -160f), 0.5f).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.Out);
            tween.TweenProperty(Char.Sprite, "position", new Vector2(0f, 0f), 0.5f).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.In);

            await Char.ToSignal(tween, "finished");

            Char.Sprite.Animation = "land";
            Char.Sprite.Frame = 0;

            if (!Char.IsDead) Char.State = new BossLandState();
        }

        public override void Update(double delta)
        {
            base.Update(delta);

            Char.Velocity = Char.Position.DirectionTo(Char.Target.Position) * Char.RunSpeed;

            Char.MoveAndSlide();
        }
    }
}