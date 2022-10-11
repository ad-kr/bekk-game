using Godot;
using System;

namespace ADKR.Game
{
    public partial class Player : Human
    {
        public override void _Ready()
        {
            base._Ready();
            State = new PlayerIdleState();
            Faction = Faction.Human;

            HealthBar.Instance.SetMinMax(0, (int)MaxHealth);
            HealthBar.Instance.SetValue(Health);
        }

        public override void OnHealthChange(float health, float prevHealth)
        {
            base.OnHealthChange(health, prevHealth);
            HealthBar.Instance.SetValue(health);
        }
    }
}