using Godot;
using System;

namespace ADKR.Game
{
    public partial class Robot : Combatable
    {
        public float ActivationRadius { get; set; } = 64f;
        public float ChargeRadius { get; set; } = 48f;

        public override void _Ready()
        {
            base._Ready();
            State = new RobotIdleState();
            RunSpeed = 32f;
            Faction = Faction.Robot;
        }
    }
}