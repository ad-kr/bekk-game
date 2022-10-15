using Godot;
using System;

namespace ADKR.Game
{
    public class EnemyRunState : CharacterState<Enemy>
    {
        private const float _radiusFactor = 1.5f;

        private float _time = 0f;

        public override void Start()
        {
            base.Start();
            _time = 0f;

            Char.Sprite.Animation = "run";
            Char.Sprite.Playing = true;
            Char.NavigationAgent.SetTargetLocation(Char.Target.Position);
        }

        public override void PhysicsUpdate(double delta)
        {
            base.Update(delta);

            bool shouldDeactivate = true;
            foreach (Combatable combatable in Combatable.Combatables)
            {
                if (combatable == Char) continue;
                if (FactionValidator.GetRelation(Char.Faction, combatable.Faction) != FactionRelation.Hostile) continue;
                float dist = Char.Position.DistanceTo(combatable.Position);

                if (dist <= Char.AttackRadius)
                {
                    Char.State = Char.ChargeState;
                    return;
                }

                if (dist <= Char.ActivationRadius * _radiusFactor) shouldDeactivate = false;
            }

            if (shouldDeactivate)
            {
                Char.State = Char.DeactivateState;
                return;
            }

            _time += (float)delta;
            if (_time * 1000f > 200f)
            {
                _time = 0f;
                Char.NavigationAgent.SetTargetLocation(Char.Target.Position);
            }

            if (Char.Velocity.Length() > Char.RunSpeed) Char.Velocity = Char.Velocity.Normalized() * Char.RunSpeed;

            Vector2 nextPos = Char.NavigationAgent.GetNextLocation();
            Vector2 dir = Char.GlobalPosition.DirectionTo(nextPos);
            Vector2 desiredVelocity = dir * Char.RunSpeed;
            Vector2 steering = (desiredVelocity - Char.Velocity) * (float)delta;


            Char.Velocity += steering;
            Char.IsFlipped = IsFlipped(Char.Velocity.Normalized() * -1f);

            Char.MoveAndSlide();
        }

        private bool IsFlipped(Vector2 dir)
        {
            if (dir.x == 0) return Char.Sprite.FlipH;
            if (dir.x > 0f) return false;
            return true;
        }
    }
}
