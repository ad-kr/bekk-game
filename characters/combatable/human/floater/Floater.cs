using Godot;
using System;
using System.Collections.Generic;

namespace ADKR.Game
{
    public partial class Floater : Human
    {
        // private static readonly List<Floater> _floaters = new();

        private Vector2 _startingPosition;

        public override void _Ready()
        {
            base._Ready();

            World.Floaters.Add(this);
            _startingPosition = Position;

            RandomizeSprite();

            State = new FloaterFloatingState();
        }

        public static async void SetAllFree()
        {

            World.Floaters.ForEach(floater =>
            {
                floater.Position = floater._startingPosition;
                floater.State = new FloaterEmptyState();
            });

            await Game.Instance.ToSignal(Game.Instance.GetTree().CreateTimer(1f), "timeout");

            World.Floaters.ForEach(floater =>
            {
                floater.State = new FloaterFreeState();
            });
        }
    }
}