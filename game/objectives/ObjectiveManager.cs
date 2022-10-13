using Godot;
using System;
using System.Collections.Generic;

namespace ADKR.Game
{
    public partial class ObjectiveManager
    {
        private Objective _objective;

        public Objective Objective
        {
            get => _objective;
            set
            {
                _objective?.End();
                _objective = value;
                _objective?.Start();
            }
        }

        public ObjectiveManager()
        {
            Objective = new StartObjective();
        }

        public void SetObjective(Objective objective)
        {
            _objective = objective;
            _objective.Start();
        }
    }
}