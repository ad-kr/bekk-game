using Godot;
using System;

namespace ADKR.Game
{
    public abstract partial class Weapon : Node2D
    {
        public Player Player { get; set; }
        public CharacterHand TopHand { get; set; }
        public CharacterHand BottomHand { get; set; }

        public abstract SignalAwaiter Attack(Vector2 dir);
    }
}