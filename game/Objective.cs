using Godot;
using System;

namespace ADKR.Game
{
    public abstract class Objective
    {

        public string Instruction { get; set; } = "";

        public virtual void Start() { }

        public virtual void End() { }
    }
}