using Godot;
using System;

namespace ADKR.Game
{
    public partial class Player : Human
    {

        public static Player Instance { get; set; }

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

        public Player()
        {
            Instance = this;
        }

        public override async void _Ready()
        {
            base._Ready();
            MaxHealth = 1000;
            Health = 1000;
            State = new PlayerEmptyState();
            Faction = Faction.Human;

            HealthBar.Instance.SetMinMax(0, (int)MaxHealth);
            HealthBar.Instance.SetValue(Health);

            await ToSignal(GetTree().CreateTimer(1f), "timeout");

            EquippedWeapon = new Crowbar();

            BossInteractable _interactable = ResourceLoader.Load<PackedScene>("res://tileset/interactable/BossInteractable.tscn").Instantiate<BossInteractable>();
            Instance.GetParent().AddChild(_interactable);
        }

        public override void _Input(InputEvent e)
        {
            base._Input(e);
            if (EquippedWeapon != null && e.IsActionPressed("attack") && State is not PlayerEmptyState)
            {
                if (State is PlayerAttackState) return;

                State = new PlayerAttackState(InputHandler.GetMouseDir());
            }
        }

        public override void OnHealthChange(float health, float prevHealth)
        {
            base.OnHealthChange(health, prevHealth);
            HealthBar.Instance.SetValue(health);
        }

        public override async void Die()
        {
            base.Die();
            Position = Respawn.Instance.Position;
            Combatables.Add(this);
            Health = MaxHealth;

            await ToSignal(GetTree().CreateTimer(0.5f), "timeout");

            Tween tween = CreateTween().SetLoops(10);
            tween.TweenProperty(Sprite, "modulate", new Color(Colors.White, 0.25f), 0.1f);
            tween.TweenProperty(Sprite, "modulate", Colors.White, 0.1f);
        }
    }
}