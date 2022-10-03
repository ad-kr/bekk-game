using Godot;
using System;

namespace ADKR.Game
{
    public class RobotRunState : CharacterState<Robot>
    {

        private const float _radiusFactor = 1.5f;

        public override void Start()
        {
            base.Start();

            Char.Sprite.Animation = "run";
            Char.Sprite.Playing = true;
        }

        public override void Update(double delta)
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

            //Run/pathfinding

            if (shouldDeactivate) Char.State = new RobotDeactivateState();
        }
    }
}