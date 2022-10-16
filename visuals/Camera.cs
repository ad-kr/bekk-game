using Godot;
using System;

namespace ADKR.Game
{
    public partial class Camera : Camera2D
    {
        public static Camera Instance { get; set; }

        public Camera()
        {
            Instance = this;
        }

        public static void AttachTo(Node2D node)
        {
            Node2D parent = Instance.GetParent<Node2D>();

            parent.RemoveChild(Instance);

            Instance.Position = parent.GlobalPosition - node.GlobalPosition;

            node.AddChild(Instance);
			
            Instance.Position = Vector2.Zero;
        }
    }

}