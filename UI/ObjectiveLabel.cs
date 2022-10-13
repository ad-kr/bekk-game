using Godot;
using System;

namespace ADKR.Game
{
    public partial class ObjectiveLabel : RichTextLabel
    {
        public static ObjectiveLabel Instance { get; set; }

        public ObjectiveLabel()
        {
            Instance = this;
			World.Instance.Objectives = new ObjectiveManager();
        }
    }
}