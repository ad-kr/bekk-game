using Godot;
using System;

namespace ADKR.Game
{
    public class RobotRunState : CharacterState<Robot>
    {
        private const float _radiusFactor = 1.5f;
        private readonly Combatable _target;

        private float _time = 0f;

        public RobotRunState(Combatable target)
        {
            _target = target;
        }

        public override void Start()
        {
            base.Start();

            Char.Sprite.Animation = "run";
            Char.Sprite.Playing = true;
            Char.NavigationAgent.SetTargetLocation(_target.Position);
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

                if (dist <= Char.ChargeRadius)
                {
                    Char.State = new RobotChargeState(combatable);
                    return;
                }

                if (dist <= Char.ActivationRadius * _radiusFactor) shouldDeactivate = false;
            }

            if (shouldDeactivate)
            {
                Char.State = new RobotDeactivateState();
                return;
            }

            _time += (float)delta;
            if (_time * 1000f > 200f)
            {
                _time = 0f;
                Char.NavigationAgent.SetTargetLocation(_target.Position);
            }

            Vector2 nextPos = Char.NavigationAgent.GetNextLocation();
            Vector2 dir = Char.GlobalPosition.DirectionTo(nextPos);
            Vector2 desiredVelocity = dir * Char.RunSpeed;
            Vector2 steering = (desiredVelocity - Char.Velocity) * (float)delta * 1f;
            Char.Velocity += steering;

            Char.MoveAndSlide();
        }
    }
}
