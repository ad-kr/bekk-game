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
            Player.Instance.State = new PlayerIdleState();

            // World.Instance.Objectives.Objective = new FreeConsulentsObjective();
            Player.Instance.State = new PlayerWalkTowardsState(Player.Instance.Position - new Vector2(48f, 0f));
        }
    }
}