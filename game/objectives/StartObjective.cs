using Godot;
using System;

namespace ADKR.Game
{
    public class StartObjective : Objective
    {
        public override async void Start()
        {
            base.Start();
            Instruction = "";
            await Game.Instance.ToSignal(Game.Instance.GetTree(), "process_frame");
            // Player.Instance.State = new PlayerEmptyState();
            Player.Instance.State = new PlayerIdleState();
            Player.Instance.EquippedWeapon = new Crowbar();
            AimIndicator.Instance.Visible = true;
            
            // World.Instance.Objectives.Objective = new IntroObjective();
            //World.Instance.Objectives.Objective = new ApproachObjective();
        }
    }
}