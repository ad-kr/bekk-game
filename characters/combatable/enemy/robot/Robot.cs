using Godot;
using System;

namespace ADKR.Game
{
    public partial class Robot : Enemy
    {
        public override void _Ready()
        {
            base._Ready();
            MaxHealth = 100f;
            Health = 100f;
            RunSpeed = 64f;
            Faction = Faction.Robot;

            AttackState = new RobotAttackState();
        }
    }
}