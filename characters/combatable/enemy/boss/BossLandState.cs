using Godot;
using System;
using System.Linq;

namespace ADKR.Game
{
    public class BossLandState : CharacterState<Boss>
    {

        private const float BossAttackRadius = 40f;

        public override async void Start()
        {
            base.Start();

            Char.Sprite.Frame = 1;
            Char.Sprite.Animation = "land";
            Char.Sprite.Playing = true;
            Char.ZIndex = 1;
            Char.Invincible = false;

            float dist = Char.Position.DistanceTo(Char.Target.Position);

            if (dist < BossAttackRadius) AttackPlayer();

            ShakeScreen();
            Puff();

            await Char.ToSignal(Char.Sprite, "animation_finished");

            if (!Char.IsDead) Char.State = new BossChargeState();
        }

        private async void Puff()
        {
            Char.Puff.Visible = true;
            Char.Puff.Scale = Vector2.Zero;

            Tween tween = Char.CreateTween();
            tween.TweenProperty(Char.Puff, "scale", new Vector2(2f, 2f), 0.3f).SetEase(Tween.EaseType.Out);
            tween.Parallel().TweenProperty(Char.Puff, "modulate", Colors.Transparent, 0.15f).SetDelay(0.15f).SetEase(Tween.EaseType.In);

            await Char.ToSignal(tween, "finished");

            Char.Puff.Visible = false;
            Char.Puff.Modulate = Colors.White;
        }

        private void AttackPlayer()
        {
            Vector2 dir = Char.Position.DirectionTo(Char.Target.Position);

            Attack attack = new(new AttackOptions()
            {
                MinDamage = 25f,
                MaxDamage = 35f,
                OnHit = (target, damage) =>
                {
                    target.ApplyEffect(new SlowDownEffect());
                    target.ApplyEffect(new HitEffect());
                    target.ApplyEffect(new HitMark(damage));
                    target.ApplyEffect(new KnockBackEffect(dir));
                }
            }, Char.Target);
            attack.Execute();
        }

        private void ShakeScreen()
        {
            GD.Randomize();
            Vector2 rand1 = new Vector2((float)GD.RandRange(-1f, 1f), (float)GD.RandRange(-1f, 1f)).Normalized() * (float)GD.RandRange(4f, 16f);
            Vector2 rand2 = new Vector2((float)GD.RandRange(-1f, 1f), (float)GD.RandRange(-1f, 1f)).Normalized() * (float)GD.RandRange(4f, 16f);
            Tween tween = Char.CreateTween();
            tween.TweenProperty(Camera.Instance, "offset", rand1, 0.1f);
            tween.TweenProperty(Camera.Instance, "offset", rand2, 0.1f);
            tween.TweenProperty(Camera.Instance, "offset", Vector2.Zero, 0.1f);
        }
    }
}