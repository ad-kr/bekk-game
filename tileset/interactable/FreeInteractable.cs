using System;
using System.Collections.Generic;
using Godot;

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

            Floater.SetAllFree();

            await Game.Instance.ToSignal(tree.CreateTimer(1f), "timeout");

            Player.Instance.State = new PlayerEmptyState();

            await Game.Instance.ToSignal(tree.CreateTimer(1f), "timeout");

            Camera.Instance.SmoothingSpeed = 1f;
            Camera.AttachTo(Respawn.Instance);

            await Game.Instance.ToSignal(tree.CreateTimer(2f), "timeout");

            DialogueBox
                .Talk(FadeToBlack,
                "Du klarte det, du beseiret robotene!",
                "Uten maskinene, kan Bekk endelig få en ny start!",
                "Sammen kan vi bygge opp Bekk på nytt!",
                "...Meeen aller først en kaffepause.");
        }

        private async void FadeToBlack()
        {
            await Game
                .Instance
                .ToSignal(Game.Instance.GetTree().CreateTimer(2f), "timeout");

            Tween tween = Game.Instance.CreateTween();
            tween
                .TweenProperty(ScreenCover.Instance, "color", Colors.Black, 5f);

            await Game.Instance.ToSignal(tween, "finished");

            await Game
                .Instance
                .ToSignal(Game.Instance.GetTree().CreateTimer(2f), "timeout");

            Game.LoadMainMenu();
        }
    }
}
