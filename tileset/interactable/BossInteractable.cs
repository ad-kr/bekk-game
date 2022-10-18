using Godot;
using System;
using System.Collections.Generic;

namespace ADKR.Game
{
    public partial class BossInteractable : Interactable
    {

        public BossInteractable()
        {
            InteractionText = "Deaktiver AI ";
        }

        public override void _Ready()
        {
            base._Ready();
            Position = new Vector2(48f, -343f);
        }

        protected override void Execute()
        {
            World.Instance.Objectives.Objective = new BossObjective();
            QueueFree();
        }
    }
}