using Godot;
using System;

namespace ADKR.Game
{
    public partial class Light : Sprite2D
    {
        private Texture2D _texture;
        private readonly Color? _tint;

        public Light(Texture2D texture, Color? tint = null)
        {
            _texture = texture;
            _tint = tint;

            ScreenOverlay.Instance.AddChild(this);
        }

        public override void _Ready()
        {
            base._Ready();
            Material = GD.Load<Material>("res://visuals/materials/LightMaterial.tres");

            Texture ??= _texture;
            if (_tint != null) ((ShaderMaterial)Material).SetShaderParameter("tint", (Color)_tint);
        }
    }
}
