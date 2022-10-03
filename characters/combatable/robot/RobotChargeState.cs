using Godot;

namespace ADKR.Game
{
    public class RobotChargeState : CharacterState<Robot>
    {
        private readonly Combatable _targetEnemy;

        public RobotChargeState(Combatable targetEnemy)
        {
            _targetEnemy = targetEnemy;
        }

        public async override void Start()
        {
            base.Start();

            Char.Sprite.Frame = 0;
            Char.Sprite.Animation = "charge";
            Char.Sprite.Playing = true;

            await Char.ToSignal(Char.Sprite, "animation_finished");

            if (Char.State is RobotChargeState) Char.State = new RobotAttackState(_targetEnemy);
        }

        public override void Update(double delta)
        {
            base.Update(delta);
            bool shouldRun = true;
            foreach (Combatable combatable in Combatable.Combatables)
            {
                if (combatable == Char) continue;
                if (FactionValidator.GetRelation(Char.Faction, combatable.Faction) != FactionRelation.Hostile) continue;
                float dist = Char.Position.DistanceTo(combatable.Position);

                if (dist <= Char.ChargeRadius)
                {
                    shouldRun = false;
                    return;
                }
            }

            if (shouldRun) Char.State = new RobotRunState();
        }
    }
}