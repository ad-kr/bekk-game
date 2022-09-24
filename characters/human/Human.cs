using Godot;
using System;

namespace ADKR.Game
{
    public partial class Human : Character
    {

        public CharacterHand TopHand { get; set; }
        public CharacterHand BottomHand { get; set; }

        public override void _Ready()
        {
            base._Ready();

            Texture2D handTexture = GD.Load<Texture2D>("res://characters/human/hand.png");

            TopHand = new(handTexture);
            BottomHand = new(handTexture);


            AddChild(TopHand);
            AddChild(BottomHand);

            TopHand.SetHand(45f, 8f);
        }
    }
}