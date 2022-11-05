using Godot;
using System;
using System.Collections.Generic;

namespace ADKR.Game
{
    public partial class Chamber : AnimatedSprite2D
    {

        // public static List<Chamber> World.Chambers { get; set; } = new();

        public override void _Ready()
        {
            World.Chambers.Add(this);
        }

        public static SignalAwaiter CloseChambers()
        {
            World.Chambers?.ForEach(chamber => chamber.Playing = true);
            return Game.Instance.ToSignal(World.Chambers[0], "animation_finished");
        }
    }
}