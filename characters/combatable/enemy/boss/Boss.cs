using Godot;
using System;

namespace ADKR.Game
{
    public partial class Boss : Enemy
    {

        public static Boss Instance { get; set; }

        public Sprite2D Shadow { get; set; }

        public Sprite2D Puff { get; set; }

        public Boss()
        {
            Instance = this;
        }

        public override void _Ready()
        {
            base._Ready();

            MaxHealth = 200f;
            Health = 200f;

            Shadow = GetNode<Sprite2D>("Shadow");

            Puff = GetNode<Sprite2D>("Puff");
            Puff.Scale = new Vector2(0.1f, 0.1f);
            Puff.Visible = false;

            RunSpeed = 96f;
            Faction = Faction.Robot;
            State = new BossIdleState();
            Invincible = true;
        }

        public override void _ExitTree()
        {
            base._ExitTree();
            World.Instance.Objectives.Objective = new FreeConsulentsObjective();
        }
    }
}