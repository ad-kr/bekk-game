using Godot;
using System;

namespace ADKR.Extensions
{
    public static class TweenExtension
    {
        public static MethodTweener TweenMethod(this Tween tween, Action<Variant> call, Variant from, Variant to, float duration)
        {
            TweenUtility util = new()
            {
                Action = call
            };

            return tween.TweenMethod(new Callable(util, nameof(util.ActionMethod)), from, to, duration);
        }
    }


    public partial class TweenUtility : Node
    {
        public Action<Variant> Action { get; set; }
        public void ActionMethod(Variant parameter)
        {
            Action(parameter);
        }
    }
}
