using Godot;
using System;

namespace ADKR.Game
{
    public partial class WorkshopInteractable : Interactable
    {
        public WorkshopInteractable()
        {
			InteractionText = "Verkstedet ";
        }

        protected override void Execute()
        {
			GD.Print("interacted!");
        }
    }
}