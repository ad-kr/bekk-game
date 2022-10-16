using Godot;
using System;

namespace ADKR.Game
{
    public partial class CrawlerAttackState : CharacterState<Crawler>
    {
        public override async void Start()
        {
            base.Start();

            Char.Sprite.Frame = 0;
            Char.Sprite.Animation = "attack";
            Char.Sprite.Playing = true;

            await Char.ToSignal(Char.Sprite, "frame_changed");

            float dist = Char.Position.DistanceTo(Char.Target.Position);

            if (Char.Target.IsDead || dist > Char.AttackRadius)
            {
                await Char.ToSignal(Char.GetTree().CreateTimer(0.5f), "timeout");
                if (!Char.IsDead) Char.State = Char.RunState;
                return;
            }

            Attack attack = new(new AttackOptions()
            {
                MinDamage = 10f,
                MaxDamage = 15f,
                OnHit = (target, damage) =>
                {
                    target.ApplyEffect(new SlowDownEffect());
                    target.ApplyEffect(new HitEffect());
                    target.ApplyEffect(new HitMark(damage));
                }
            }, Char.Target);
            attack.Execute();

            await Char.ToSignal(Char.Sprite, "animation_finished");
            await Char.ToSignal(Char.GetTree().CreateTimer(0.5f), "timeout");

            if (!Char.IsDead) Char.State = Char.RunState;
        }
    }
}