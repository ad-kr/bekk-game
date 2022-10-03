using Godot;
using System;

namespace ADKR.Game
{
    public class RobotAttackState : CharacterState<Robot>
    {
        private Combatable _targetEnemy;

        public RobotAttackState(Combatable targetEnemy)
        {
            _targetEnemy = targetEnemy;
        }
    }
}