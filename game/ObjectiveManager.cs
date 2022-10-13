using Godot;
using System;
using System.Collections.Generic;

namespace ADKR.Game
{
    public partial class ObjectiveManager
    {
        private Objective _objective;

        public ObjectiveManager()
        {
			//Set default first objective
        }

        public void SetObjective(Objective objective)
        {
            _objective = objective;
			_objective.Start();
        }
    }
}