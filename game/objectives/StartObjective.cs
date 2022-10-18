using Godot;
using System;

namespace ADKR.Game
{
    public class StartObjective : Objective
    {
        public override async void Start()
        {
            base.Start();
            Instruction = "this is the first objective :)";
            await Game.Instance.ToSignal(Game.Instance.GetTree(), "process_frame");
            Player.Instance.State = new PlayerIdleState();
            
            World.Instance.Objectives.Objective = new FreeConsulentsObjective();
        }
    }
}