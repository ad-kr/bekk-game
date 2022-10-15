using Godot;
using System;

namespace ADKR.Game
{
    public partial class TimeoutReminder : RichTextLabel
    {
        public static TimeoutReminder Instance { get; set; }

        public TimeoutReminder()
        {
            Instance = this;
        }

        private const float Timeout = 120000f;
        private float _elapsed = Timeout;

        public override void _PhysicsProcess(double delta)
        {
            base._PhysicsProcess(delta);
            _elapsed -= (float)delta * 1000f;

            bool isElapsedWarned = _elapsed <= 20000f;
            Instance.Visible = isElapsedWarned;

            if (isElapsedWarned)
            {
                Instance.Text = $"[center]Spillet restarter om {Mathf.Ceil(_elapsed / 1000f)} sekunder. \n Beveg deg for å fortsette å spille.";
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