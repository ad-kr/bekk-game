using Godot;
using System;

namespace ADKR.Game
{
    public partial class Character : CharacterBody2D
    {
        public float RunSpeed { get; set; } = 64f;

        public CharacterState State { get; set; }

    }
}
