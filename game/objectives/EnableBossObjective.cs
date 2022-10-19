using Godot;
using System;

namespace ADKR.Game
{
    public class EnableBossObjective : Objective
    {
        public override void Start()
        {
            base.Start();

			Instruction = "Deaktiver det sentrale systemet i Hovedøya.";

            BossInteractable _interactable = ResourceLoader.Load<PackedScene>("res://tileset/interactable/BossInteractable.tscn").Instantiate<BossInteractable>();
            Player.Instance.GetParent().AddChild(_interactable);
        }
    }
}