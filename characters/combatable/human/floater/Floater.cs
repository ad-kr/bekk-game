using Godot;
using System;

namespace ADKR.Game
{
    public partial class Floater : Human
    {
        public override void _Ready()
        {
            base._Ready();

            RandomizeSprite();

            State = new FloaterFloatingState();
        }
    }
}