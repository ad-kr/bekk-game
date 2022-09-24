using Godot;
using System;

namespace ADKR.Game
{

    public partial class Player : Human
    {
        public override void _Ready()
        {
            base._Ready();
            Sprite = GetNode<AnimatedSprite2D>("Sprite");
            State = new PlayerIdleState();
        }
    }
}