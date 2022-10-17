using Godot;
using System;

namespace ADKR.Game
{
    public partial class Human : Combatable
    {

        public CharacterHand TopHand { get; set; }
        public CharacterHand BottomHand { get; set; }

        public AnimatedSprite2D Hair { get; set; }
        public AnimatedSprite2D Clothes { get; set; }

        public int SkinID { get; set; } = 2;
        public int HairID { get; set; } = 2;
        public int ClothesID { get; set; } = 2;

        public override void _Ready()
        {
            base._Ready();

            Texture2D handTexture = GD.Load<Texture2D>("res://characters/combatable/human/hand.png");

            Clothes = Sprite.GetNode<AnimatedSprite2D>("Clothes");
            Hair = Clothes.GetNode<AnimatedSprite2D>("Hair");

            RandomizeSprite();

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

        public void RandomizeSprite()
        {
            GD.Randomize();
            SkinID = GD.RandRange(1, 3);
            HairID = GD.RandRange(1, 6);
            ClothesID = GD.RandRange(1, 3);
            SetSpriteLayers();
        }

        private void SetSpriteLayers()
        {
            Texture2D skinTex = GD.Load<Texture2D>($"res://characters/combatable/human/player/sprites/skin{SkinID}.png");
            Texture2D hairTex = GD.Load<Texture2D>($"res://characters/combatable/human/player/sprites/hair{HairID}.png");
            Texture2D clothesTex = GD.Load<Texture2D>($"res://characters/combatable/human/player/sprites/clothes{ClothesID}.png");
            for (int i = 0; i < 6; i++)
            {
                AtlasTexture skinAtlas = new()
                {
                    Atlas = skinTex,
                    Region = new Rect2(i * 16f, 0f, 16f, 24f)
                };
                Sprite.Frames.SetFrame("run", i, skinAtlas);
                AtlasTexture hairAtlas = new()
                {
                    Atlas = hairTex,
                    Region = new Rect2(i * 16f, 0f, 16f, 24f)
                };
                Hair.Frames.SetFrame("run", i, hairAtlas);
                AtlasTexture clothesAtlas = new()
                {
                    Atlas = clothesTex,
                    Region = new Rect2(i * 16f, 0f, 16f, 24f)
                };
                Clothes.Frames.SetFrame("run", i, clothesAtlas);
            }
        }

        protected override void SetFlip(bool isFlipped)
        {
            base.SetFlip(isFlipped);
            Hair.FlipH = isFlipped;
            Clothes.FlipH = isFlipped;
            TopHand.IsFlipped = isFlipped;
            BottomHand.IsFlipped = isFlipped;
        }
    }
}
