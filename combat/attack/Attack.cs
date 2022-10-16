using Godot;
using System;

namespace ADKR.Game
{
    public class Attack
    {
        private readonly AttackOptions _options;
        private readonly Combatable[] _targets;

        public Attack(AttackOptions options, params Combatable[] targets)
        {
            _options = options;
            _targets = targets;
        }

        public void Execute(float delta = 1f)
        {
            GD.Randomize();
            foreach (Combatable target in _targets)
            {
                if (target.Invincible) continue;
                float damage = (float)GD.RandRange(_options.MinDamage, _options.MaxDamage);
                target.Health -= damage * delta;
                _options.OnHit?.Invoke(target, damage);
            }
        }
    }

    public struct AttackOptions
    {
        public float MinDamage { get; set; }
        public float MaxDamage { get; set; }
        public Action<Combatable, float> OnHit { get; set; }
    }
}