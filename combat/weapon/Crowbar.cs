using Godot;
using System;

namespace ADKR.Game
{
    public partial class Crowbar : Weapon
    {
        public override void _Ready()
        {
            Sprite2D sprite = new()
            {
                Texture = GD.Load<Texture2D>("res://combat/weapon/crowbar.png"),
                Position = new Vector2(1f, -3f),
            };

            ShowBehindParent = true;

            AddChild(sprite);
        }

        public override void Attack()
        {
            //throw new NotImplementedException();
        }
    }
}