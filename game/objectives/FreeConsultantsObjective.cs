using Godot;
using System;

namespace ADKR.Game
{
    public class FreeConsulentsObjective : Objective
    {
        public override async void Start()
        {
            base.Start();

            await Game.Instance.ToSignal(Game.Instance.GetTree().CreateTimer(2f), "timeout");

            Instruction = "Frigjør konsulentene i Kaffiskjæret";

            if (Godot.Object.IsInstanceValid(AimIndicator.Instance)) AimIndicator.Instance.Visible = false;

            Sprite2D mask1 = World.Instance.GetNode<Sprite2D>("%ChamberMask");
            Sprite2D mask2 = World.Instance.GetNode<Sprite2D>("%ChamberMask2");
            Sprite2D mask3 = World.Instance.GetNode<Sprite2D>("%ChamberMask3");

            Game.Instance.GetTree().Paused = true;
            await Game.Instance.ToSignal(Game.Instance.GetTree().CreateTimer(1f), "timeout");

            float originalSpeed = Camera.Instance.SmoothingSpeed;
            Camera.Instance.SmoothingSpeed = 3f;
            Camera.AttachTo(Respawn.Instance);

            await Game.Instance.ToSignal(Game.Instance.GetTree().CreateTimer(3f), "timeout");

            //Turn off the lights
            mask1.QueueFree();
            mask2.QueueFree();
            mask3.QueueFree();

            //Add FreeInteractable
            FreeInteractable _interactable = ResourceLoader.Load<PackedScene>("res://tileset/interactable/FreeInteractable.tscn").Instantiate<FreeInteractable>();
            Player.Instance.GetParent().AddChild(_interactable);

            await Game.Instance.ToSignal(Game.Instance.GetTree().CreateTimer(2f), "timeout");

            Camera.AttachTo(Player.Instance);

            await Game.Instance.ToSignal(Game.Instance.GetTree().CreateTimer(3f), "timeout");

            if (Godot.Object.IsInstanceValid(AimIndicator.Instance)) AimIndicator.Instance.Visible = true;

            Camera.Instance.SmoothingSpeed = originalSpeed;
            Game.Instance.GetTree().Paused = false;

        }
    }
}