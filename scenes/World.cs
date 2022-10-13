using Godot;
using System;

namespace ADKR.Game
{
    public partial class World : Node
    {
        public static World Instance { get; set; }

        public ObjectiveManager Objectives { get; set; }

        private const float Timeout = 120000f;
        private float _elapsed = Timeout;

        public World()
        {
            Instance = this;
        }

        public override void _Ready()
        {
            base._Ready();
            // Objectives = new();
        }

        public override void _PhysicsProcess(double delta)
        {
            base._PhysicsProcess(delta);
            _elapsed -= (float)delta * 1000f;

            bool isElapsedWarned = _elapsed <= 20000f;
            TimeoutReminder.Instance.Visible = isElapsedWarned;

            if (isElapsedWarned)
            {
                TimeoutReminder.Instance.Text = $"[center]Spillet restarter om {Mathf.Ceil(_elapsed / 1000f)} sekunder. \n Beveg deg for å fortsette å spille.";
            }

            if (_elapsed < 0)
            {
                Game.LoadMainMenu();
            }
        }

        public static void RefreshTimer()
        {
            Instance._elapsed = Timeout;
        }
    }
}
