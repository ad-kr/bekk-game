using Godot;
using System;

namespace ADKR.Game
{
    public partial class Boss : Enemy
    {

        public static Boss Instance { get; set; }

        public Sprite2D Puff { get; set; }

        public Boss()
        {
            Instance = this;
        }

        public override void _Ready()
        {
            base._Ready();

            Puff = GetNode<Sprite2D>("Puff");
            Puff.Scale = new Vector2(0.1f, 0.1f);
            Puff.Visible = false;

            RunSpeed = 96f;
            Faction = Faction.Robot;
            State = new BossIdleState();
            Invincible = true;
        }
    }
}