using Godot;
using System;

namespace ADKR.Game
{
    public partial class ApproachObjective : Objective
    {
        public override void Start()
        {
            base.Start();
            Instruction = "Undersøk lyset i Kaffiskjæret";
            Player.Instance.State = new PlayerIdleState();
        }
    }
}