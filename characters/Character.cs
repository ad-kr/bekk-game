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

        public override void _Process(double delta)
        {
            base._Process(delta);
            State?.Update(delta);
        }

    }
}
