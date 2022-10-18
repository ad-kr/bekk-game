using Godot;
using System;
using System.Collections.Generic;

namespace ADKR.Game
{
    public partial class FreeInteractable : Interactable
    {
        public FreeInteractable()
        {
            InteractionText = "Frigjør konsulentene ";
        }

        public override void _Ready()
        {
            base._Ready();
            Position = new Vector2(330f, -200f);
        }

        protected override async void Execute()
        {
            SceneTree tree = GetTree();
            QueueFree();

            Player.Instance.Faction = Faction.Robot;

            HealthBar.Instance.Visible = false;
            ScrapCounter.Instance.Visible = false;
            ObjectiveLabel.Instance.Visible = false;
            AimIndicator.Instance.Visible = false;

            await Chamber.CloseChambers();

            Floater.SetAllFree(); //Max 2 second

            await Game.Instance.ToSignal(tree.CreateTimer(1f), "timeout");

            Player.Instance.State = new PlayerEmptyState();

            await Game.Instance.ToSignal(tree.CreateTimer(1f), "timeout");

            Camera.AttachTo(Respawn.Instance);

            await Game.Instance.ToSignal(tree.CreateTimer(2f), "timeout");

            DialogueBox.Talk(FadeToBlack, "Du har klart det...", "Den onde AI-en er beseiret.", "Vi kan endelig få tilbake konsulentjobbene våre.");

        }

        private void FadeToBlack()
        {
            //Create fadein overlay
        }
    }
}