using Godot;
using System;

namespace ADKR.Game
{
    public partial class Robot : Combatable
    {
        public float ActivationRadius { get; set; } = 64f;
        public float ChargeRadius { get; set; } = 48f;
        public NavigationAgent2D NavigationAgent { get; set; }

        public override void _Ready()
        {
            base._Ready();
            NavigationAgent = GetNode<NavigationAgent2D>("NavigationAgent2d");
            State = new RobotIdleState();
            RunSpeed = 64f;
            Faction = Faction.Robot;
        }
    }
}