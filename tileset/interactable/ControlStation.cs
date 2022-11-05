using Godot;
using System;
using System.Collections.Generic;

namespace ADKR.Game
{
    public partial class ControlStation : Interactable
    {
        private Sprite2D _light;

        // public static readonly List<ControlStation> Stations = new();

        public ControlStation()
        {
            InteractionText = "Deaktiver ";
        }

        public override async void _Ready()
        {
            base._Ready();
            World.Stations.Add(this);
            _light = GetNode<Sprite2D>("Light");
            Visible = false;

            await ToSignal(GetTree(), "process_frame");
        }

        protected override async void Execute()
        {

            if (!Visible) return;

            GetTree().Paused = true;

            await ToSignal(GetTree().CreateTimer(1f), "timeout");

            AimIndicator.Instance.Visible = false;

            float originalSpeed = Camera.Instance.SmoothingSpeed;
            Camera.Instance.SmoothingSpeed = 3f;
            Camera.AttachTo(_light);

            await ToSignal(GetTree().CreateTimer(3f), "timeout");

            _light.Visible = false;

            await ToSignal(GetTree().CreateTimer(1.5f), "timeout");

            Camera.AttachTo(Player.Instance);

            await ToSignal(GetTree().CreateTimer(3f), "timeout");

            AimIndicator.Instance.Visible = true;

            Camera.Instance.SmoothingSpeed = originalSpeed;
            GetTree().Paused = false;
            World.Stations.Remove(this);

            if (World.Stations.Count == 0) SetBossObjective();

            QueueFree();
        }

        private static void SetBossObjective()
        {
            World.Instance.Objectives.Objective = new EnableBossObjective();
        }
    }
}