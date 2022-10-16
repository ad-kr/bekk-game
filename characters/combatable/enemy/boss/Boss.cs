using Godot;
using System;

namespace ADKR.Game
{
    public partial class Boss : Enemy
    {
        public override void _Ready()
        {
            base._Ready();
            // RunSpeed = 32f;
            Faction = Faction.Robot;
            State = new BossIdleState();
            Invincible = true;
        }
    }
}