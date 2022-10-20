using Godot;
using System;

namespace ADKR.Game
{
    public partial class Crawler : Enemy
    {
        public override void _Ready()
        {
            base._Ready();
            MaxHealth = 15f;
            Health = 15f;
            AttackRadius = 16f;
            RunSpeed = 96f;
            Faction = Faction.Robot;

            AttackState = new CrawlerAttackState();
            ActivateState = new CrawlerActivateState();
            ChargeState = new CrawlerChargeState();
            DeactivateState = new CrawlerDeactivateState();
        }
    }
}