using Godot;
using System;

namespace ADKR.Game
{
    public partial class Respawn : Node2D
    {
        public static Respawn Instance { get; set; }

        public Respawn()
        {
            Instance = this;
        }
    }
}