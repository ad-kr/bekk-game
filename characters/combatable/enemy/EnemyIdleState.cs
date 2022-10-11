using Godot;
using System;

namespace ADKR.Game
{
    public class EnemyIdleState : CharacterState<Enemy>
    {
        public override void Start()
        {
            base.Start();
            Char.Sprite.Frame = 0;
            Char.Sprite.Animation = "idle";
            Char.Sprite.Playing = true;
        }

        public override void Update(double delta)
        {
            base.Update(delta);
            foreach (Combatable combatable in Combatable.Combatables)
            {
                if (combatable == Char) continue;
                if (FactionValidator.GetRelation(Char.Faction, combatable.Faction) != FactionRelation.Hostile) continue;
                float dist = Char.Position.DistanceTo(combatable.Position);

                if (dist <= Char.ActivationRadius)
                {
                    Char.Target = combatable;
                    Char.State = Char.ActivateState;
                    return;
                }
            }
        }
    }
}