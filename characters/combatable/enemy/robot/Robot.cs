using Godot;
using System;

namespace ADKR.Game
{
    public partial class Robot : Enemy
    {

        public override void _Ready()
        {
            base._Ready();
            RunSpeed = 64f;
            Faction = Faction.Robot;
            
            AttackState = new RobotAttackState();
        }
    }
}