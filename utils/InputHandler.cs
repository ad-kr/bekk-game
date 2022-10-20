using Godot;
using System;

namespace ADKR.Game
{
    public class InputHandler
    {
        public static Vector2 GetAxisInput()
        {
            Vector2 joy = GetJoyDir();
            float joyLength = joy.Length();
            if(joyLength > 0f) return GetJoyDir();

            float xAxis = Input.GetAxis("ui_left", "ui_right");
            float yAxis = Input.GetAxis("ui_up", "ui_down");

            return new Vector2(xAxis, yAxis).Normalized();
        }

        public static Vector2 GetMouseDir()
        {
            Vector2 joy = GetJoyDir();
            float joyLength = joy.Length();
            if(joyLength > 0f) return GetJoyDir();
            
            Vector2 dir = Game.Instance.GetViewport().GetMousePosition() - (Game.Instance.GetViewportRect().Size / 2f);
            dir = dir.Normalized();
            return dir;
        }

        public static Vector2 GetJoyDir(){
            float xAxis = Input.GetAxis("joy_left", "joy_right");
            float yAxis = Input.GetAxis("joy_up", "joy_down");

            return new Vector2(xAxis, yAxis).Normalized();
        }
    }
}