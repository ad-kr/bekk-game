using Godot;
using System;
using System.Collections.Generic;

namespace ADKR.Game
{
    public partial class World : Node
    {
        public static World Instance { get; set; }

        public ObjectiveManager Objectives { get; set; }

        //This is a fix and it's kinda not well thought out
        public static List<Combatable> Combatables { get; set; }
        public static List<Floater> Floaters { get; set; }
        public static List<Chamber> Chambers { get; set; }
        public static List<ControlStation> Stations { get; set; }

        public World()
        {
            Instance = this;
            
            Combatables = new();
            Floaters = new();
            Chambers = new();
            Stations = new();
        }
    }
}
