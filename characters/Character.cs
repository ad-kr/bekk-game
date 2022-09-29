using Godot;
using System;

namespace ADKR.Game
{
    public partial class Character : CharacterBody2D
    {

        public AnimatedSprite2D Sprite { get; set; }

        public float RunSpeed { get; set; } = 64f;

        private CharacterState _state;

        public CharacterState State
        {
            get => _state;
            set
            {
                _state?.End();
                _state = value;
                _state.SetCharacter(this);
                _state?.Start();
            }
        }

        private bool _isFlipped;
        public bool IsFlipped
        {
            get => _isFlipped;
            set
            {
                _isFlipped = value;
                SetFlip(value);
            }
        }

        public override void _Ready()
        {
            base._Ready();
            Sprite = GetNode<AnimatedSprite2D>("Sprite");
        }

        public override void _Process(double delta)
        {
            base._Process(delta);
            State?.Update(delta);
        }

        protected virtual void SetFlip(bool isFlipped)
        {
            Sprite.FlipH = isFlipped;
        }

    }
}
