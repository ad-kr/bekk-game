using Godot;
using System;

namespace ADKR.Game
{
    public partial class World : Node
    {
        public static World Instance { get; set; }

        public ObjectiveManager Objectives { get; set; }

        public World()
        {
            Instance = this;
        }
    }
}
