using Godot;
using System;

namespace ADKR.Game
{
    public partial class Player : Character
    {

        public override void _Ready()
        {
            base._Ready();
			State = new PlayerIdleState();
        }

        public override void _Process(double delta)
        {
            base._Process(delta);

            float xAxis = Input.GetAxis("ui_left", "ui_right");
            float yAxis = Input.GetAxis("ui_up", "ui_down");

            Vector2 dir = new Vector2(xAxis, yAxis).Normalized();

            Velocity = dir * RunSpeed;

            MoveAndSlide();
        }
    }
}