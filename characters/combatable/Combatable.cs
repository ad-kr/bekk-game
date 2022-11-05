using Godot;
using System;
using System.Collections.Generic;

namespace ADKR.Game
{
    public abstract partial class Combatable : Character
    {
        // public static List<Combatable> World.Combatables { get; set; } = new();

        private float _health = 100f;
        public float Health
        {
            get => _health;
            set
            {
                float prevHealth = _health;
                _health = value > MaxHealth ? MaxHealth : value;
                OnHealthChange(_health, prevHealth);
                if (_health <= 0f) Die();
            }
        }
        public float MaxHealth { get; set; } = 100f;

        public bool IsDead { get => Health <= 0f; }

        public Faction Faction { get; set; }

        public bool Invincible { get; set; } = false;

        public override void _Ready()
        {
            base._Ready();
            World.Combatables.Add(this);
        }

        public virtual void OnHealthChange(float health, float prevHealth) { }

        public virtual void Die()
        {
            World.Combatables.Remove(this);
        }

        public override void _ExitTree()
        {
            base._ExitTree();
            World.Combatables.Remove(this);
        }

    }
}