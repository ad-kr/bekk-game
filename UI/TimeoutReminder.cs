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
    }

}