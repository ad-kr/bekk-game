using Godot;
using System;

namespace ADKR.Game
{
    public class BossIdleState : CharacterState<Enemy>
    {
        public override void Start()
        {
            base.Start();
            Char.Sprite.Frame = 0;
            Char.Sprite.Animation = "idle";
        }

        public override void Input(InputEvent e)
        {
            base.Input(e);
            if (e.IsActionPressed("ui_text_backspace"))
            {
				Char.State = new BossActivateState();
            }
        }
    }
}