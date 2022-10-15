using Godot;
using System;

namespace ADKR.Game
{
    public partial class ESCMenu : Control
    {
        public override void _Input(InputEvent e)
        {
            base._Input(e);

            if (Visible && e.IsActionPressed("restart"))
            {
				GetTree().Paused = false;
                Game.LoadMainMenu();
            }

            if (e.IsActionPressed("escape"))
            {
                Visible = !Visible;
                GetTree().Paused = Visible;
				TimeoutReminder.RefreshTimer();
            }
        }
    }
}