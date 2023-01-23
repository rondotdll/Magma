using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Magma
{
    public static class EzAnimate
    {
        // This function just simplifies the process of actually playing a DoubleAnimation
        private static void DoDoubleAnimation(DoubleAnimation doubleAnimation, DependencyObject target, object property, EventHandler onFinish = null)
        {
            var sb = new Storyboard();

            sb.Children.Add(doubleAnimation);
            
            if (onFinish != null)
                sb.Completed += onFinish;

            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(property));
            Storyboard.SetTarget(doubleAnimation, target);
            sb.Begin();
        }

        // This method is mostly just used to resize our FluidContainer control
        public static void ResizeX(in FrameworkElement target, double newWidth, double duration = 400, EventHandler onFinish = null)
        {
            var ResizeAnimation = new DoubleAnimation()
            {
                From = target.Width,
                To = newWidth,
                Duration = new Duration(TimeSpan.FromMilliseconds(duration)),
                EasingFunction = new QuadraticEase()
                {
                    EasingMode = EasingMode.EaseInOut
                }
            };

            DoDoubleAnimation(ResizeAnimation, target, FrameworkElement.WidthProperty, onFinish);   
        }

        // Used for fading controls into view
        public static void FadeIn(in FrameworkElement target, double duration = 280, EventHandler onFinish = null)
        {
            var FadeInAnimation = new DoubleAnimation()
            {
                From = target.Opacity,
                To = 1,
                Duration = new Duration(TimeSpan.FromMilliseconds(duration * (1 - target.Opacity))),
                EasingFunction = new QuadraticEase()
                {
                    EasingMode = EasingMode.EaseInOut
                }
            };

            DoDoubleAnimation(FadeInAnimation, target, FrameworkElement.OpacityProperty, onFinish);
        }

        // Used for fading controls out of view
        public static void FadeOut(in FrameworkElement target, double duration = 280, EventHandler onFinish = null)
        {
            var FadeInAnimation = new DoubleAnimation()
            {
                From = target.Opacity,
                To = 0,
                Duration = new Duration(TimeSpan.FromMilliseconds(duration * target.Opacity)), // Time adjustment for partially transparent controls
                EasingFunction = new QuadraticEase()
                {
                    EasingMode = EasingMode.EaseInOut
                }
            };

            DoDoubleAnimation(FadeInAnimation, target, FrameworkElement.OpacityProperty, onFinish);
        }
    }
}
