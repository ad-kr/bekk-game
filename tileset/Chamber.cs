using Godot;
using System;
using System.Collections.Generic;

namespace ADKR.Game
{
    public partial class Chamber : AnimatedSprite2D
    {

        public static List<Chamber> Chambers { get; set; } = new();

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            Chambers.Add(this);
        }

        public static void CloseChambers()
        {
            Chambers.ForEach(chamber => chamber.Playing = true);
        }
    }
}