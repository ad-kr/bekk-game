using Godot;
using System;

namespace ADKR.Game
{
    public partial class Robot : Enemy
    {

        public override void _Ready()
        {
            base._Ready();
            Health = 10;
            RunSpeed = 64f;
            Faction = Faction.Robot;

            AttackState = new RobotAttackState();
        }

        public override void _Process(double delta)
        {
            base._Process(delta);
        }
    }
}