using Godot;
using System;

namespace ADKR.Game
{
    public class StartObjective : Objective
    {
        public override void Start()
        {
            base.Start();
            Instruction = "this is the first objective :)";
        }
    }
}