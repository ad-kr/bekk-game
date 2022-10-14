using Godot;
using System;

namespace ADKR.Game
{
    public partial class ScrapCounter : HBoxContainer
    {
        public static ScrapCounter Instance { get; set; }

        private RichTextLabel _label;

        private int _count = 0;
        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                if (_label != null) _label.Text = _count.ToString();
            }
        }

        public ScrapCounter()
        {
            Instance = this;
        }

        public override void _Ready()
        {
            base._Ready();
            _label = GetNode<RichTextLabel>("RichTextLabel");
            Count = 0;
        }
    }
}