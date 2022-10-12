using Godot;
using System;

namespace ADKR.Game
{
    public partial class Player : Human
    {
        private Weapon _equippedWeapon = null;

        public Weapon EquippedWeapon
        {
            get => _equippedWeapon;
            set
            {
                _equippedWeapon?.QueueFree();
                _equippedWeapon = value;
                TopHand.HandSprite.AddChild(value);
            }
        }

        public override async void _Ready()
        {
            base._Ready();
            State = new PlayerIdleState();
            Faction = Faction.Human;

            HealthBar.Instance.SetMinMax(0, (int)MaxHealth);
            HealthBar.Instance.SetValue(Health);

            await ToSignal(GetTree().CreateTimer(3f), "timeout");

            EquippedWeapon = new Crowbar();
        }

        public override void OnHealthChange(float health, float prevHealth)
        {
            base.OnHealthChange(health, prevHealth);
            HealthBar.Instance.SetValue(health);
        }
    }
}