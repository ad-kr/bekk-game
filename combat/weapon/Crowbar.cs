using Godot;
using System;
using ADKR.Extensions;
using System.Collections.Generic;

namespace ADKR.Game
{
    public partial class Crowbar : Weapon
    {

        private float _defaultBottomAngle;

        private const float AttackRadius = 24f;
        private const float AttackAngle = 120f;

        public override void _Ready()
        {
            Sprite2D sprite = new()
            {
                Texture = GD.Load<Texture2D>("res://combat/weapon/crowbar.png"),
                Position = new Vector2(1f, -5f),
            };

            ShowBehindParent = true;

            AddChild(sprite);
        }

        public override SignalAwaiter Attack(Vector2 dir)
        {
            TopHand.Angle = -45f;
            BottomHand.Angle = 130f;

            Tween tween = CreateTween().SetParallel();
            tween.TweenMethod(value =>
            {
                float angle = (float)value;
                TopHand.Angle = angle;
            }, TopHand.Angle, 130f, 0.2f);
            tween.TweenMethod(value =>
            {
                float angle = (float)value;
                BottomHand.Angle = angle;
            }, BottomHand.Angle, _defaultBottomAngle, 0.2f);

            List<Combatable> targets = new();
            foreach (Combatable combatable in Combatable.Combatables)
            {
                if (combatable == Player) continue;
                if (FactionValidator.GetRelation(Player.Faction, combatable.Faction) != FactionRelation.Hostile) continue;
                float dist = Player.Position.DistanceTo(combatable.Position);

                if (dist <= AttackRadius || (combatable is Boss && dist <= AttackRadius * 2f))
                {
                    float angleTo = dir.AngleTo(combatable.Position - Player.Position);
                    angleTo = Mathf.RadToDeg(angleTo);

                    if (Mathf.Abs(angleTo) < AttackAngle * 0.5f) targets.Add(combatable);
                }
            }

            Attack attack = new(new AttackOptions()
            {
                MinDamage = 3f,
                MaxDamage = 6f,
                OnHit = (target, damage) =>
                {
                    target.ApplyEffect(new SlowDownEffect());
                    target.ApplyEffect(new HitEffect());
                    target.ApplyEffect(new HitMark(damage));
                    target.ApplyEffect(new KnockBackEffect(dir));
                }
            }, targets.ToArray());
            attack.Execute();

            return ToSignal(tween, "finished");
        }
    }
}