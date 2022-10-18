using Godot;
using System;
using System.Collections.Generic;

namespace ADKR.Game
{
    public partial class ControlStation : Interactable
    {
        private Sprite2D _light;

        public static readonly List<ControlStation> Stations = new();

        public ControlStation()
        {
            InteractionText = "Deaktiver ";
        }

        public override async void _Ready()
        {
            base._Ready();
            Stations.Add(this);
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
            Stations.Remove(this);

            if (Stations.Count == 0) SetBossObjective();

            QueueFree();
        }

        private static void SetBossObjective()
        {
            BossInteractable _interactable = ResourceLoader.Load<PackedScene>("res://tileset/interactable/BossInteractable.tscn").Instantiate<BossInteractable>();
            Player.Instance.GetParent().AddChild(_interactable);
        }
    }
}