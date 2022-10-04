using Godot;
using System;

namespace ADKR.Game
{
    public class RobotAttackState : CharacterState<Robot>
    {
        private readonly Combatable _target;

        public RobotAttackState(Combatable target)
        {
            _target = target;
        }
    }
}