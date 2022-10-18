using Godot;
using System;

namespace ADKR.Game
{
    public partial class IntroObjective : Objective
    {
        public override async void Start()
        {
            base.Start();

            //color rect fadein
            ScreenCover.Instance.Color = Colors.Black;
            Tween screenTween = Game.Instance.CreateTween();
            screenTween.TweenProperty(ScreenCover.Instance, "color", new Color(Colors.Black, 0f), 5f);

            await Game.Instance.ToSignal(screenTween, "finished");

			

            //Set player start position

            //Walk in

            //Talk

            //Set new objective approachObjective
        }
    }

}