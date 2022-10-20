using Godot;
using System;

namespace ADKR.Game
{
    public partial class Robot : Enemy
    {
        public override void _Ready()
        {
            base._Ready();
            MaxHealth = 50f;
            Health = 50f;
            RunSpeed = 64f;
            Faction = Faction.Robot;

            AttackState = new RobotAttackState();
        }
    }
}