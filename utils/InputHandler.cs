using System;
using System.Collections.Generic;
using Godot;

namespace ADKR.Game
{
    public class InputHandler
    {
        private static bool _isKeyboard = true;
        public static bool IsKeyboard
        {
            get => _isKeyboard;
            private set
            {
                _deviceSubscriptions.ForEach(s => s?.Invoke());
                _isKeyboard = value;
            }
        }

        private static readonly List<Action> _deviceSubscriptions = new();

        public static Vector2 GetAxisInput()
        {
            Vector2 joy = GetJoyDir();
            float joyLength = joy.Length();
            if (joyLength > 0f) return GetJoyDir();

            float xAxis = Input.GetAxis("ui_left", "ui_right");
            float yAxis = Input.GetAxis("ui_up", "ui_down");

            return new Vector2(xAxis, yAxis).Normalized();
        }

        public static Vector2 GetMouseDir()
        {
            Vector2 joy = GetJoyDir();
            float joyLength = joy.Length();
            if (joyLength > 0f) return GetJoyDir();

            Vector2 dir = Game.Instance.GetViewport().GetMousePosition() - (Game.Instance.GetViewportRect().Size / 2f);
            dir = dir.Normalized();
            return dir;
        }

        public static Vector2 GetJoyDir()
        {
            float xAxis = Input.GetAxis("joy_left", "joy_right");
            float yAxis = Input.GetAxis("joy_up", "joy_down");

            return new Vector2(xAxis, yAxis).Normalized();
        }

        public static void SetInputDevice(InputEvent e)
        {
            bool isKeyboard = !e.IsActionPressed("joy_input") && (e.IsActionPressed("key_input") || IsKeyboard);

            if (isKeyboard == IsKeyboard) return;

            IsKeyboard = isKeyboard;
        }

        public static void SubscribeDeviceChange(Action call)
        {
            _deviceSubscriptions.Add(call);
            IsKeyboard = IsKeyboard;
        }

        public static void ClearSubscription(Action call)
        {
            _deviceSubscriptions.Remove(call);
        }
    }
}
