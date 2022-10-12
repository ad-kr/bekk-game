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
                _equippedWeapon.Player = this;
                _equippedWeapon.TopHand = TopHand;
                _equippedWeapon.BottomHand = BottomHand;
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

            await ToSignal(GetTree().CreateTimer(1f), "timeout");

            EquippedWeapon = new Crowbar();
        }

        public override void _Input(InputEvent e)
        {
            base._Input(e);
            if (EquippedWeapon != null && e is InputEventMouseButton mouseEvent && e.IsActionPressed("attack"))
            {
                if (State is PlayerAttackState) return;
                Vector2 dir = mouseEvent.Position - (GetViewportRect().Size / 2f);
                dir = dir.Normalized();

                State = new PlayerAttackState(dir);
            }
        }

        public override void OnHealthChange(float health, float prevHealth)
        {
            base.OnHealthChange(health, prevHealth);
            HealthBar.Instance.SetValue(health);
        }
    }
}