using Godot;
using System;

namespace ADKR.Game
{
    public partial class Pushable : RigidBody2D
    {
        public AnimatedSprite2D Sprite { get; set; }

        public override void _Ready()
        {
            Sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2d");
        }

        public override void _PhysicsProcess(double delta)
        {
            Sprite.Playing = false;
            float speed = LinearVelocity.LengthSquared();
            if (speed <= 0f) return;
            Sprite.Playing = true;
            if (Mathf.Abs(LinearVelocity.x) < 50f) return;
            Sprite.FlipH = LinearVelocity.x > 0f;
        }
    }
}