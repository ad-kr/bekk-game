using Godot;
using System;

namespace ADKR.Game
{
    public partial class IntroObjective : Objective
    {

        private float _originalCameraSpeed;

        public override async void Start()
        {
            base.Start();

            Player.Instance.Position = new Vector2(-48f, 450f);

            ScreenCover.Instance.Color = Colors.Black;

            await Game.Wait(2f);

            Tween screenTween = Game.Instance.CreateTween();
            screenTween.TweenProperty(ScreenCover.Instance, "color", new Color(Colors.Black, 0f), 5f);

            await Game.Instance.ToSignal(screenTween, "finished");

            await Game.Wait(1f);

            var walk = new PlayerWalkTowardsState(Player.Instance.Position + new Vector2(32f, 0f));
            Player.Instance.State = walk;

            await walk.Finished;

            await Game.Wait(1f);

            DialogueBox.Talk(PanToChambers, "???", "Hva skjedde her?", "Hvor er alle konsulentene?");
        }

        private async void PanToChambers()
        {
            _originalCameraSpeed = Camera.Instance.SmoothingSpeed;
            Camera.Instance.SmoothingSpeed = 1f;
            Camera.AttachTo(Respawn.Instance);

            await Game.Wait(6f);

            DialogueBox.Talk(PanBack, "Hva er det lyset som kommer fra Kaffiskjæret?");
        }

        private async void PanBack()
        {
            await Game.Wait(1f);

            Camera.AttachTo(Player.Instance);

            await Game.Wait(6f);

            Camera.Instance.SmoothingSpeed = _originalCameraSpeed;

            DialogueBox.Talk(SwitchObjective, "Dette må jeg undersøke...");
        }

        private void SwitchObjective()
        {
            World.Instance.Objectives.Objective = new ApproachObjective();
        }
    }

}