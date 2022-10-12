using Godot;
using System;

namespace ADKR.Game
{
    public abstract partial class Weapon : Node2D
    {
        public abstract void Attack();
    }
}