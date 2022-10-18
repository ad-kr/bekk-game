using Godot;
using System;

namespace ADKR.Game
{
    public abstract class Objective
    {
        private string _instruction = "";

        public string Instruction
        {
            get => _instruction;
            set
            {
                _instruction = value;
                if (Godot.Object.IsInstanceValid(ObjectiveLabel.Instance) && ObjectiveLabel.Instance != null) ObjectiveLabel.Instance.Text = _instruction;
            }
        }

        public virtual void Start() { }

        public virtual void End() { }
    }
}