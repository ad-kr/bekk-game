using Godot;
using System;

namespace ADKR.Game
{
    public partial class Robot : Enemy
    {
        public float ChargeRadius { get; set; } = 48f;

        public override void _Ready()
        {
            base._Ready();
            // State = new RobotIdleState();
            RunSpeed = 64f;
            Faction = Faction.Robot;
        }
    }
}