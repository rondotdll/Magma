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

        // This function is the same as the previous, but instead of DoubleAnimations its for ThicknessAnimations
        private static void DoThicknessAnimation(ThicknessAnimation thicknessAnimation, DependencyObject target, object property, EventHandler onFinish = null)
        {
            var sb = new Storyboard();

            sb.Children.Add(thicknessAnimation);

            if (onFinish != null)
                sb.Completed += onFinish;

            Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath(property));
            Storyboard.SetTarget(thicknessAnimation, target);
            sb.Begin();
        }

        public static void ResizeMargins(in FrameworkElement target, Thickness newMargin, double duration = 400, EventHandler onFinish = null)
        {
            var ResizeMarginsAnimation = new ThicknessAnimation()
            {
                From = target.Margin,
                To = newMargin,
                Duration = new Duration(TimeSpan.FromMilliseconds(duration)),
                EasingFunction = new QuadraticEase()
                {
                    EasingMode = EasingMode.EaseInOut
                }
            };

            DoThicknessAnimation(ResizeMarginsAnimation, target, FrameworkElement.MarginProperty, onFinish);
        }

        // This method is mostly just used to resize our FluidContainer control
        public static void ResizeX(in FrameworkElement target, double newWidth, double duration = 400, EventHandler onFinish = null)
        {
            var ResizeAnimation = new DoubleAnimation()
            {
                From = target.ActualWidth,
                To = newWidth,
                Duration = new Duration(TimeSpan.FromMilliseconds(duration)),
                EasingFunction = new QuadraticEase()
                {
                    EasingMode = EasingMode.EaseInOut
                }
            };

            DoDoubleAnimation(ResizeAnimation, target, FrameworkElement.WidthProperty, onFinish);
        }

        // Self explanatory, fades an object to a specified "newOpacity". (Called from "FadeOut" and "FadeIn")
        public static void FadeOpacity(in FrameworkElement target, double newOpacity, double duration = 280, EventHandler onFinish = null)
        {
            var FadeAnimation = new DoubleAnimation()
            {
                From = target.Opacity,
                To = newOpacity,
                Duration = new Duration(TimeSpan.FromMilliseconds(duration * (1 - target.Opacity))),
                EasingFunction = new QuadraticEase()
                {
                    EasingMode = EasingMode.EaseInOut
                }
            };

            DoDoubleAnimation(FadeAnimation, target, FrameworkElement.OpacityProperty, onFinish);
        }

        // Used for fading controls into view
        public static void FadeIn(in FrameworkElement target, double duration = 280, EventHandler onFinish = null)
        {
            FadeOpacity(target, 1, duration, onFinish);
        }

        // Used for fading controls out of view
        public static void FadeOut(in FrameworkElement target, double duration = 280, EventHandler onFinish = null)
        {
            FadeOpacity(target, 0, duration, onFinish);
        }
    }
}
