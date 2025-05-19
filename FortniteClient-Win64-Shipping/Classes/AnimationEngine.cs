using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;

namespace FortniteClient_Win64_Shipping.Classes
{
    class AnimationEngine
    {
        public static Task FadeOut(UIElement element, double durationSeconds = 0.5)
        {
            var tcs = new TaskCompletionSource<bool>();

            var animation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(durationSeconds),
                FillBehavior = FillBehavior.HoldEnd
            };
            animation.Completed += (s, e) => tcs.SetResult(true);
            element.BeginAnimation(UIElement.OpacityProperty, animation);

            return tcs.Task;
        }

        public static Task FadeIn(UIElement element, double durationSeconds = 0.5)
        {
            var tcs = new TaskCompletionSource<bool>();

            var animation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(durationSeconds),
                FillBehavior = FillBehavior.HoldEnd
            };
            animation.Completed += (s, e) => tcs.SetResult(true);
            element.BeginAnimation(UIElement.OpacityProperty, animation);

            return tcs.Task;
        }
    }
}
