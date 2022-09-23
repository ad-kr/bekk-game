using Godot;
using System;

namespace ADKR.Game
{
    public class InputHandler
    {
        public static Vector2 GetAxisInput()
        {
            float xAxis = Input.GetAxis("ui_left", "ui_right");
            float yAxis = Input.GetAxis("ui_up", "ui_down");

            return new Vector2(xAxis, yAxis).Normalized();
        }
    }
}