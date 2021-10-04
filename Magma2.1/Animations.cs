using System;
using System.Windows.Media.Animation;

namespace Magma2._1
{
    class Animations
    {
        // Define Easing Functions

        public static ExponentialEase SmoothEase = new ExponentialEase();

        // Define Animations

        public static DoubleAnimation ScriptsCollapse = new DoubleAnimation();
        public static DoubleAnimation ScriptsUnCollapse = new DoubleAnimation();
        public static DoubleAnimation TextBoxExpand = new DoubleAnimation();
        public static DoubleAnimation TextBoxShrink = new DoubleAnimation();
        public static DoubleAnimation GridViewMinimizeW = new DoubleAnimation();
        public static DoubleAnimation GridViewMaximizeW = new DoubleAnimation();
        public static DoubleAnimation GridViewMinimizeH = new DoubleAnimation();
        public static DoubleAnimation GridViewMaximizeH = new DoubleAnimation();


        public static void Initialize()
        {
            // Make SmoothEase function for both EaseIn and EaseOut

            SmoothEase.EasingMode = EasingMode.EaseInOut;

            // Set Properties for Scripts List Collapse Animation

            ScriptsCollapse.From = 180;
            ScriptsCollapse.To = 0;
            ScriptsCollapse.Duration = TimeSpan.FromSeconds(0.75);
            ScriptsCollapse.EasingFunction = SmoothEase;

            // Set Properties for Scripts List UnCollapse Animation

            ScriptsUnCollapse.From = 0;
            ScriptsUnCollapse.To = 180;
            ScriptsUnCollapse.Duration = TimeSpan.FromSeconds(0.75);
            ScriptsUnCollapse.EasingFunction = SmoothEase;

            // Set Properties for TextBox Expand Animation

            TextBoxExpand.From = 495;
            TextBoxExpand.To = 675;
            TextBoxExpand.Duration = TimeSpan.FromSeconds(0.75);
            TextBoxExpand.EasingFunction = SmoothEase;

            // Set Properties for TextBox Shrink Animation

            TextBoxShrink.From = 675;
            TextBoxShrink.To = 495;
            TextBoxShrink.Duration = TimeSpan.FromSeconds(0.75);
            TextBoxShrink.EasingFunction = SmoothEase;

            // Set Properties for GridView Minimize Width Animation

            GridViewMinimizeW.From = 675;
            GridViewMinimizeW.To = 0;
            GridViewMinimizeW.Duration = TimeSpan.FromSeconds(1);
            GridViewMinimizeW.EasingFunction = SmoothEase;

            // Set Properties for GridView Maximize Width Animation

            GridViewMaximizeW.From = 0;
            GridViewMaximizeW.To = 675;
            GridViewMaximizeW.Duration = TimeSpan.FromSeconds(1);
            GridViewMaximizeW.EasingFunction = SmoothEase;

            // Set Properties for GridView Minimize Height Animation

            GridViewMinimizeH.From = 400;
            GridViewMinimizeH.To = 0;
            GridViewMinimizeH.Duration = TimeSpan.FromSeconds(1);
            GridViewMinimizeH.EasingFunction = SmoothEase;

            // Set Properties for GridView Maximize Height Animation

            GridViewMaximizeH.From = 0;
            GridViewMaximizeH.To = 400;
            GridViewMaximizeH.Duration = TimeSpan.FromSeconds(1);
            GridViewMaximizeH.EasingFunction = SmoothEase;

        }


    }
}
