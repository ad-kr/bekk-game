using Godot;
using System;

namespace ADKR.Game
{
    public partial class RandomizePlayer : Control
    {
        public override void _Input(InputEvent e)
        {
            base._Input(e);

            if (e.IsActionPressed("interact")) Game.LoadWorld();

            if (e.IsActionPressed("restart")) Player.Instance.RandomizeSprite();
        }
    }
}