using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace Magma
{
    internal enum AccentState
    {
        ACCENT_DISABLED = 0,
        ACCENT_ENABLE_GRADIENT = 1,
        ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
        ACCENT_ENABLE_BLURBEHIND = 3,
        ACCENT_ENABLE_ACRYLICBLURBEHIND = 4,
        ACCENT_INVALID_STATE = 5
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct AccentPolicy
    {
        public AccentState AccentState;
        public uint AccentFlags;
        public uint GradientColor;
        public uint AnimationId;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowCompositionAttributeData
    {
        public WindowCompositionAttribute Attribute;
        public IntPtr Data;
        public int SizeOfData;
    }

    internal enum WindowCompositionAttribute
    {
        // ...
        WCA_ACCENT_POLICY = 19
        // ...
    }


    internal class Internal
    {
        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);


        public static double AcrylicOpacity = 0.01;
        public static uint AcrylicTint = 0x000000; /* Uses BGR color format */
        private static int[] TintRGB = new int[3];

        private static WindowInteropHelper windowHelper;
        private static AccentPolicy accent = new AccentPolicy();
        private static Int32 accentStructSize = Marshal.SizeOf(accent);
        private static WindowCompositionAttributeData attributeData = new WindowCompositionAttributeData();

        public static uint RGBToWinHex(int red, int green, int blue) // Used to convert from RGB standard value to Microsoft's weird gay ass BGR Hex format.
        {
            TintRGB[0] = red;
            TintRGB[1] = green;
            TintRGB[2] = blue;
            return (uint)Convert.ToInt32("0x" + blue.ToString("X2") + green.ToString("X2") + red.ToString("X2"), 16);
        }

        /* Enable Acrylic Effect Function */
        public static void EnableBlur(Window context) // Works by directly attaching the application to the native windows Acrylic sampling API (low-level shit)
        {
            context.Background = null;
            windowHelper = new WindowInteropHelper(context);

            accent.AccentState = AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND;
            accent.GradientColor = ((uint)(AcrylicOpacity * 255.0) << 24) | (AcrylicTint & 0xFFFFFF);

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            attributeData.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
            attributeData.SizeOfData = accentStructSize;
            attributeData.Data = accentPtr;

            SetWindowCompositionAttribute(windowHelper.Handle, ref attributeData);

            Marshal.FreeHGlobal(accentPtr);
        }

        /* Disable Acrylic Effect Function */

        public static void DisableBlur(Window context)
        {
            SetWindowCompositionAttribute(windowHelper.Handle, ref attributeData);

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.FreeHGlobal(accentPtr);
            context.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb((byte)(0.95 * 255.0), (byte)TintRGB[0], (byte)TintRGB[1], (byte)TintRGB[2]));
        }

        [DllImport("dwmapi.dll", PreserveSig = true)]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        private static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins pMarInset);

        /// <summary>
        /// Drops a standard shadow to a WPF Window, even if the window is borderless. Only works with DWM (Windows Vista or newer).
        /// This method is much more efficient than setting AllowsTransparency to true and using the DropShadow effect,
        /// as AllowsTransparency involves a huge performance issue (hardware acceleration is turned off for all the window).
        /// </summary>
        /// <param name="window">Window to which the shadow will be applied</param>

        public static bool DropShadow(Window window)
        {
            try
            {
                WindowInteropHelper helper = new WindowInteropHelper(window);
                int val = 2;
                int ret1 = DwmSetWindowAttribute(helper.Handle, 2, ref val, 4);

                if (ret1 == 0)
                {
                    Margins m = new Margins { Bottom = 0, Left = 0, Right = 0, Top = 0 };
                    int ret2 = DwmExtendFrameIntoClientArea(helper.Handle, ref m);
                    return ret2 == 0;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Probably dwmapi.dll not found (incompatible OS)
                return false;
            }
        }
    }
}

