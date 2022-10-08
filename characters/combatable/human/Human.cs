using Godot;
using System;

namespace ADKR.Game
{
	public partial class Human : Combatable
	{

		public CharacterHand TopHand { get; set; }
		public CharacterHand BottomHand { get; set; }

        public override void _Ready()
		{
			base._Ready();

			Texture2D handTexture = GD.Load<Texture2D>("res://characters/combatable/human/hand.png");

			TopHand = new(handTexture);
			BottomHand = new(handTexture);

			Sprite.AddChild(TopHand);
			Sprite.AddChild(BottomHand);

			TopHand.Offset = 8f;
			TopHand.Angle = 90f;
			TopHand.PivotOffset = new Vector2(-2f, -2f);

			BottomHand.Offset = 8f;
			BottomHand.Angle = 45f;
			BottomHand.PivotOffset = new Vector2(-1f, -2f);
			BottomHand.ZIndex = -1;
		}

		protected override void SetFlip(bool isFlipped)
		{
			base.SetFlip(isFlipped);
			TopHand.IsFlipped = isFlipped;
			BottomHand.IsFlipped = isFlipped;
		}
	}
}
