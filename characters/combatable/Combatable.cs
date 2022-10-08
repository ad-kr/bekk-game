using Godot;
using System;
using System.Collections.Generic;

namespace ADKR.Game
{
    public abstract partial class Combatable : Character
    {
        public static List<Combatable> Combatables { get; set; } = new();

        private float _health = 100f;
        public float Health
        {
            get => _health;
            set
            {
                OnHealthChange(value, _health);
                _health = value > MaxHealth ? MaxHealth : value;
                if (_health <= 0f) Die();
            }
        }
        public float MaxHealth { get; set; } = 100f;

        public bool IsDead { get => Health <= 0f; }

        public Faction Faction { get; set; }

        public override void _Ready()
        {
            base._Ready();
            Combatables.Add(this);
        }

        public virtual void OnHealthChange(float health, float prevHealth) { }

        public virtual void Die()
        {
            Combatables.Remove(this);
        }

    }
}