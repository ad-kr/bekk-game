using Godot;
using System;

namespace ADKR.Game
{
    public partial class ControlStation : Interactable
    {
        private Sprite2D _light;

        public override void _Ready()
        {
            base._Ready();
            _light = GetNode<Sprite2D>("Light");
        }

        protected override async void Execute()
        {
            GetTree().Paused = true;

            await ToSignal(GetTree().CreateTimer(1f), "timeout");

            AimIndicator.Instance.Visible = false;

            float originalSpeed = Camera.Instance.SmoothingSpeed;
            Camera.Instance.SmoothingSpeed = 1f;
            Camera.AttachTo(_light);

            await ToSignal(GetTree().CreateTimer(5f), "timeout");

            _light.Visible = false;

            await ToSignal(GetTree().CreateTimer(2f), "timeout");

            Camera.AttachTo(Player.Instance);

            await ToSignal(GetTree().CreateTimer(5f), "timeout");

            AimIndicator.Instance.Visible = true;

            Camera.Instance.SmoothingSpeed = originalSpeed;
            GetTree().Paused = false;
            QueueFree();
        }
    }
}