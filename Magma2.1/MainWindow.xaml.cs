using System;
using AnemoAPI;
using System.IO;
using System.Linq;
using System.Text;
using ClubDarkAPI;
using EasyExploits;
using WeAreDevs_API;
using System.Windows;
using System.Windows.Data;
using MaterialDesignThemes;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Threading.Tasks;
using ICSharpCode.AvalonEdit;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Net;
using System.Diagnostics;

namespace Magma2._1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static AnemoAPI.Anemo ANMO = new AnemoAPI.Anemo();
        public static EasyExploits.Module EZXP = new EasyExploits.Module();
        public static ClubDarkAPI.ExploitAPI CDRK = new ClubDarkAPI.ExploitAPI();
        public static WeAreDevs_API.ExploitAPI WRD = new WeAreDevs_API.ExploitAPI();

        public int CarouselID = 2;

        string[] config;
        public bool setTopClicked = false;
        public bool ScriptsCollapsed = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }

        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            Animations.Initialize();

            ScriptTextBox.TextArea.TextView.LinkTextForegroundBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF80CBC4"));
            ScriptTextBox.SyntaxHighlighting = ResourceLoader.LoadHighlightingDefinition("Lua.xshd");

            /* Create Config File If Corrupted / Doesn't Exist */

            if (!File.Exists(System.IO.Path.GetFullPath("config ( DO NOT EDIT! ).txt")))
            {
                File.WriteAllText(System.IO.Path.GetFullPath("config ( DO NOT EDIT! ).txt"), "classic\nanmo\nfalse\nfalse\n./Scripts\nfalse");
            }

            if (File.ReadLines(System.IO.Path.GetFullPath("config ( DO NOT EDIT! ).txt")).Count() <= 5)
            {
                File.WriteAllText(System.IO.Path.GetFullPath("config ( DO NOT EDIT! ).txt"), "classic\nezxp\nfalse\nfalse\n./Scripts\nfalse");
            }

            /* Import And Read Config File */

            config = File.ReadAllLines(System.IO.Path.GetFullPath("config ( DO NOT EDIT! ).txt"));

            /*if (config[1] == "ezxp")
            {
                ApiType.SelectedIndex = 2;
            }
            else if (config[1] == "anmo")
            {
                ApiType.SelectedIndex = 1;
            }
            else if (config[1] == "cdrk")
            {
                ApiType.SelectedIndex = 6;
            }
            else if (config[1] == "wrd")
            {
                ApiType.SelectedIndex = 3;
            }
            else if (config[1] == "csq")
            {
                ApiType.SelectedIndex = 4;
            }

            if (config[2] == "false")
            {
                AcrylicEffect.IsChecked = false;
            }
            else if (config[2] == "true")
            {
                AcrylicEffect.IsChecked = true;
            }

            if (config[3] == "false")
            {
                AdvUnfocus.IsChecked = false;
            }
            else if (config[3] == "true")
            {
                AdvUnfocus.IsChecked = true;
            }

            if (config[5] == "false")
            {
                AttachFromHome.IsChecked = false;
            }
            else if (config[5] == "true")
            {
                AttachFromHome.IsChecked = true;
            }
            */
            /* Check To Make Sure Scripts Directory Exists */

            if (Directory.Exists(System.IO.Path.GetFullPath(config[4])))
            {

                string paththing = "";

                if (System.IO.Path.GetFileName(System.IO.Path.GetFullPath(config[4])).Length >= 11)
                {
                    paththing = $"{(System.IO.Path.GetFileName(System.IO.Path.GetFullPath(config[4]))).Substring(0, 10)}...";
                }
                else
                {
                    paththing = $"{System.IO.Path.GetFileName(System.IO.Path.GetFullPath(config[4]))}";
                }

                LocalFunctions.PopulateListBox(ScriptsListBox, config[4], "*.txt");
                LocalFunctions.PopulateListBox(ScriptsListBox, config[4], "*.lua");
            }

        }

        private void DragBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();

        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CollapseButton_Click(object sender, RoutedEventArgs e)
        {

            if (ScriptsCollapsed == true)
            {
                ScriptsContainer.BeginAnimation(WidthProperty, Animations.ScriptsUnCollapse);
                TextBoxContainer.BeginAnimation(WidthProperty, Animations.TextBoxShrink);
                CollapseButtonIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.ArrowRightThick;
                CollapseButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#7FFFFFFF"));
                CollapseButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#00282A2F"));
                ScriptsCollapsed = false;

            }
            else if (ScriptsCollapsed == false)
            {
                ScriptsContainer.BeginAnimation(WidthProperty, Animations.ScriptsCollapse);
                TextBoxContainer.BeginAnimation(WidthProperty, Animations.TextBoxExpand);
                CollapseButtonIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.ArrowLeftThick;
                CollapseButton.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFFFF"));
                CollapseButton.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF282A2F"));
                ScriptsCollapsed = true;
            }

        }

        private void ScriptsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string itemName = ScriptsListBox.Items[ScriptsListBox.SelectedIndex].ToString().Replace("System.Windows.Controls.ListBoxItem: ", "");
                ScriptTextBox.Text = File.ReadAllText($"{System.IO.Path.GetFullPath(config[4])}/{itemName}");
            }
            catch
            {

            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void KeepOnTopButton_Click(object sender, RoutedEventArgs e)
        {
            if (setTopClicked == false)
            {
                this.Topmost = true;
                KeepOnTopButton.ToolTip = new ToolTip { Content = "Keep On Top (Yes)" };
                KeepOnTopButton.Foreground = (Brush)(new System.Windows.Media.BrushConverter()).ConvertFromString("#DDFFFFFF");
                setTopClicked = true;
            }
            else if (setTopClicked == true)
            {
                this.Topmost = false;
                KeepOnTopButton.ToolTip = new ToolTip { Content = "Keep On Top (No)" };
                KeepOnTopButton.Foreground = (Brush)(new System.Windows.Media.BrushConverter()).ConvertFromString("#7FFFFFFF");
                setTopClicked = false;
            }

        }

        private void HideConfig(object sender, EventArgs e)
        {
            ConfigGrid.Visibility = Visibility.Hidden;

        }

        private void ShowConfig(object sender, EventArgs e)
        {
            ConfigGrid.Visibility = Visibility.Visible;
        }

        private void HideExecutor(object sender, EventArgs e)
        {
            ConfigGrid.Visibility = Visibility.Hidden;
        }

        private void ShowExecutor(object sender, EventArgs e)
        {
            ConfigGrid.Visibility = Visibility.Visible;
        }

        private void ChangeScreen(string Screen)
        {
            if (Screen.ToLower() == "config" || Screen.ToLower() == "0")
            {
                ConfigGrid.Visibility = Visibility.Visible;
                ConfigGrid.Height = 400;
                ConfigGrid.Width = 675;

                ExecutorGrid.Visibility = Visibility.Hidden;
                ExecutorGrid.Height = 0;
                ExecutorGrid.Width = 0;
            }
        }

        private void AdvancedUnfocus_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (100 - Math.Floor(AdvancedUnfocus.Value * 10) == 100)
            {
                UnfocusLabel.Content = "- Disabled";
            }
            else if (100 - Math.Floor(AdvancedUnfocus.Value * 10) <= 15)
            {
                UnfocusLabel.Content = "- 15%";
            }
            else
            {
                UnfocusLabel.Content = "- " + Convert.ToString(100 - Math.Floor(AdvancedUnfocus.Value * 10)) + "%";
            }
        }

        // Ok, So I would like to appologize for the next bit of code I wrote which controls the Carousel for the Really Easy Injector Mode. 
        // If you feel like organizing it in a more logical sense feel free to lmk what you come up with lmao

        private void REInjectRight_Click(object sender, RoutedEventArgs e)
        {
            ExponentialEase SmoothEase = new ExponentialEase();
            SmoothEase.EasingMode = EasingMode.EaseOut;

            DoubleAnimation Pos15Opacity = new DoubleAnimation();
            Pos15Opacity.EasingFunction = SmoothEase;
            Pos15Opacity.From = 1;
            Pos15Opacity.To = 0.5;
            Pos15Opacity.Duration = TimeSpan.FromSeconds(0.25);

            DoubleAnimation Pos51Opacity = new DoubleAnimation();
            Pos51Opacity.EasingFunction = SmoothEase;
            Pos51Opacity.From = 0.5;
            Pos51Opacity.To = 1;
            Pos51Opacity.Duration = TimeSpan.FromSeconds(0.25);

            DoubleAnimation Pos52Opacity = new DoubleAnimation();
            Pos52Opacity.EasingFunction = SmoothEase;
            Pos52Opacity.From = 0.5;
            Pos52Opacity.To = 0.25;
            Pos52Opacity.Duration = TimeSpan.FromSeconds(0.25);

            DoubleAnimation Pos25Opacity = new DoubleAnimation();
            Pos25Opacity.EasingFunction = SmoothEase;
            Pos25Opacity.From = 0.25;
            Pos25Opacity.To = 0.5;
            Pos25Opacity.Duration = TimeSpan.FromSeconds(0.25);

            DoubleAnimation Pos20Opacity = new DoubleAnimation();
            Pos20Opacity.EasingFunction = SmoothEase;
            Pos20Opacity.From = 0.25;
            Pos20Opacity.To = 0;
            Pos20Opacity.Duration = TimeSpan.FromSeconds(0.25);

            DoubleAnimation Pos02Opacity = new DoubleAnimation();
            Pos02Opacity.EasingFunction = SmoothEase;
            Pos02Opacity.From = 0;
            Pos02Opacity.To = 0.25;
            Pos02Opacity.Duration = TimeSpan.FromSeconds(0.25);

            if (CarouselID == 0)
            {
                CarouselID = 1;

                ThicknessAnimation CSGOPos = new ThicknessAnimation();
                CSGOPos.From = new Thickness(228, 53, 227, 53);
                CSGOPos.To = new Thickness(135, 88, 373, 88);
                CSGOPos.Duration = TimeSpan.FromSeconds(0.25);
                CSGOPos.EasingFunction = SmoothEase;

                ThicknessAnimation GTAPos = new ThicknessAnimation();
                GTAPos.From = new Thickness(373, 88, 135, 88);
                GTAPos.To = new Thickness(228, 53, 227, 53);
                GTAPos.Duration = TimeSpan.FromSeconds(0.25);
                GTAPos.EasingFunction = SmoothEase;

                ThicknessAnimation GMODPos = new ThicknessAnimation();
                GMODPos.From = new Thickness(472, 107, 65, 109);
                GMODPos.To = new Thickness(373, 88, 135, 88);
                GMODPos.Duration = TimeSpan.FromSeconds(0.25);
                GMODPos.EasingFunction = SmoothEase;

                //TF2.BeginAnimation(MarginProperty, TF2Pos);
                TF2.BeginAnimation(OpacityProperty, Pos02Opacity);

                CSGO.BeginAnimation(MarginProperty, CSGOPos);
                CSGO.BeginAnimation(OpacityProperty, Pos15Opacity);

                GMOD.BeginAnimation(MarginProperty, GMODPos);
                GMOD.BeginAnimation(OpacityProperty, Pos25Opacity);

                GTAV.BeginAnimation(MarginProperty, GTAPos);
                GTAV.BeginAnimation(OpacityProperty, Pos51Opacity);

                Canvas.SetZIndex(CSGO, 2);
                Canvas.SetZIndex(GTAV, 3);
                Canvas.SetZIndex(GMOD, 2);
                Canvas.SetZIndex(TF2, 1);

                REInjectorLabel.Content = "ZeroTwo Menu (CheatSquad.gg)";

            }

            else if (CarouselID == 1)
            {
                CarouselID = 2;

                ThicknessAnimation GTAPos = new ThicknessAnimation();
                GTAPos.From = new Thickness(228, 53, 227, 53);
                GTAPos.To = new Thickness(135, 88, 373, 88);
                GTAPos.Duration = TimeSpan.FromSeconds(0.25);
                GTAPos.EasingFunction = SmoothEase;

                ThicknessAnimation GMODPos = new ThicknessAnimation();
                GMODPos.From = new Thickness(373, 88, 135, 88);
                GMODPos.To = new Thickness(228, 53, 227, 53);
                GMODPos.Duration = TimeSpan.FromSeconds(0.25);
                GMODPos.EasingFunction = SmoothEase;

                ThicknessAnimation CSGOPos = new ThicknessAnimation();
                CSGOPos.From = new Thickness(135, 88, 373, 88);
                CSGOPos.To = new Thickness(65, 107, 472, 109);
                CSGOPos.Duration = TimeSpan.FromSeconds(0.25);
                CSGOPos.EasingFunction = SmoothEase;

                ThicknessAnimation TF2Pos = new ThicknessAnimation();
                TF2Pos.From = new Thickness(472, 107, 65, 109);
                TF2Pos.To = new Thickness(373, 88, 135, 88);
                TF2Pos.Duration = TimeSpan.FromSeconds(0.25);
                TF2Pos.EasingFunction = SmoothEase;

                TF2.BeginAnimation(MarginProperty, TF2Pos);
                TF2.BeginAnimation(OpacityProperty, Pos52Opacity);

                CSGO.BeginAnimation(MarginProperty, CSGOPos);
                CSGO.BeginAnimation(OpacityProperty, Pos52Opacity);

                GMOD.BeginAnimation(MarginProperty, GMODPos);
                GMOD.BeginAnimation(OpacityProperty, Pos51Opacity);

                GTAV.BeginAnimation(MarginProperty, GTAPos);
                GTAV.BeginAnimation(OpacityProperty, Pos15Opacity);

                Canvas.SetZIndex(CSGO, 1);
                Canvas.SetZIndex(GTAV, 2);
                Canvas.SetZIndex(GMOD, 3);
                Canvas.SetZIndex(TF2, 2);

                REInjectorLabel.Content = "AOSHax MultiHack (CheatSquad.gg)";

            }

            else if (CarouselID == 2)
            {
                CarouselID = 3;

                ThicknessAnimation GMODPos = new ThicknessAnimation();
                GMODPos.From = new Thickness(228, 53, 227, 53);
                GMODPos.To = new Thickness(135, 88, 373, 88);
                GMODPos.Duration = TimeSpan.FromSeconds(0.25);
                GMODPos.EasingFunction = SmoothEase;

                ThicknessAnimation TF2Pos = new ThicknessAnimation();
                TF2Pos.From = new Thickness(373, 88, 135, 88);
                TF2Pos.To = new Thickness(228, 53, 227, 53);
                TF2Pos.Duration = TimeSpan.FromSeconds(0.25);
                TF2Pos.EasingFunction = SmoothEase;

                ThicknessAnimation GTAPos = new ThicknessAnimation();
                GTAPos.From = new Thickness(135, 88, 373, 88);
                GTAPos.To = new Thickness(65, 107, 472, 109);
                GTAPos.Duration = TimeSpan.FromSeconds(0.25);
                GTAPos.EasingFunction = SmoothEase;

                CSGO.BeginAnimation(OpacityProperty, Pos20Opacity);

                GTAV.BeginAnimation(MarginProperty, GTAPos);
                GTAV.BeginAnimation(OpacityProperty, Pos52Opacity);

                TF2.BeginAnimation(MarginProperty, TF2Pos);
                TF2.BeginAnimation(OpacityProperty, Pos51Opacity);

                GMOD.BeginAnimation(MarginProperty, GMODPos);
                GMOD.BeginAnimation(OpacityProperty, Pos15Opacity);

                Canvas.SetZIndex(TF2, 3);
                Canvas.SetZIndex(GMOD, 2);
                Canvas.SetZIndex(GTAV, 1);

                REInjectorLabel.Content = "SEOwned (UnknownCheats.me)";

            }
        }

        private void REInjectLeft_Click(object sender, RoutedEventArgs e)
        {
            ExponentialEase SmoothEase = new ExponentialEase();
            SmoothEase.EasingMode = EasingMode.EaseOut;

            DoubleAnimation Pos15Opacity = new DoubleAnimation();
            Pos15Opacity.EasingFunction = SmoothEase;
            Pos15Opacity.From = 1;
            Pos15Opacity.To = 0.5;
            Pos15Opacity.Duration = TimeSpan.FromSeconds(0.25);

            DoubleAnimation Pos51Opacity = new DoubleAnimation();
            Pos51Opacity.EasingFunction = SmoothEase;
            Pos51Opacity.From = 0.5;
            Pos51Opacity.To = 1;
            Pos51Opacity.Duration = TimeSpan.FromSeconds(0.25);

            DoubleAnimation Pos52Opacity = new DoubleAnimation();
            Pos52Opacity.EasingFunction = SmoothEase;
            Pos52Opacity.From = 0.5;
            Pos52Opacity.To = 0.25;
            Pos52Opacity.Duration = TimeSpan.FromSeconds(0.25);

            DoubleAnimation Pos25Opacity = new DoubleAnimation();
            Pos25Opacity.EasingFunction = SmoothEase;
            Pos25Opacity.From = 0.25;
            Pos25Opacity.To = 0.5;
            Pos25Opacity.Duration = TimeSpan.FromSeconds(0.25);

            DoubleAnimation Pos20Opacity = new DoubleAnimation();
            Pos20Opacity.EasingFunction = SmoothEase;
            Pos20Opacity.From = 0.25;
            Pos20Opacity.To = 0;
            Pos20Opacity.Duration = TimeSpan.FromSeconds(0.25);

            DoubleAnimation Pos02Opacity = new DoubleAnimation();
            Pos02Opacity.EasingFunction = SmoothEase;
            Pos02Opacity.From = 0;
            Pos02Opacity.To = 0.25;
            Pos02Opacity.Duration = TimeSpan.FromSeconds(0.25);

            if (CarouselID == 1)
            {
                CarouselID = 0;

                ThicknessAnimation CSGOPos = new ThicknessAnimation();
                CSGOPos.From = new Thickness(135, 88, 373, 88);
                CSGOPos.To = new Thickness(228, 53, 227, 53);
                CSGOPos.Duration = TimeSpan.FromSeconds(0.25);
                CSGOPos.EasingFunction = SmoothEase;

                ThicknessAnimation GTAPos = new ThicknessAnimation();
                GTAPos.From = new Thickness(228, 53, 227, 53);
                GTAPos.To = new Thickness(373, 88, 135, 88);
                GTAPos.Duration = TimeSpan.FromSeconds(0.25);
                GTAPos.EasingFunction = SmoothEase;

                ThicknessAnimation GMODPos = new ThicknessAnimation();
                GMODPos.From = new Thickness(373, 88, 135, 88);
                GMODPos.To = new Thickness(472, 107, 65, 109);
                GMODPos.Duration = TimeSpan.FromSeconds(0.25);
                GMODPos.EasingFunction = SmoothEase;

                //TF2.BeginAnimation(MarginProperty, TF2Pos);
                TF2.BeginAnimation(OpacityProperty, Pos20Opacity);

                CSGO.BeginAnimation(MarginProperty, CSGOPos);
                CSGO.BeginAnimation(OpacityProperty, Pos51Opacity);

                GMOD.BeginAnimation(MarginProperty, GMODPos);
                GMOD.BeginAnimation(OpacityProperty, Pos52Opacity);

                GTAV.BeginAnimation(MarginProperty, GTAPos);
                GTAV.BeginAnimation(OpacityProperty, Pos15Opacity);

                Canvas.SetZIndex(CSGO, 3);
                Canvas.SetZIndex(GTAV, 2);
                Canvas.SetZIndex(GMOD, 1);
                Canvas.SetZIndex(TF2, 1);

                REInjectorLabel.Content = "CSGhost v4.2 (UnknownCheats.me)";

            }

            else if (CarouselID == 2)
            {
                CarouselID = 1;

                ThicknessAnimation GTAPos = new ThicknessAnimation();
                GTAPos.From = new Thickness(135, 88, 373, 88);
                GTAPos.To = new Thickness(228, 53, 227, 53);
                GTAPos.Duration = TimeSpan.FromSeconds(0.25);
                GTAPos.EasingFunction = SmoothEase;

                ThicknessAnimation GMODPos = new ThicknessAnimation();
                GMODPos.From = new Thickness(228, 53, 227, 53);
                GMODPos.To = new Thickness(373, 88, 135, 88);
                GMODPos.Duration = TimeSpan.FromSeconds(0.25);
                GMODPos.EasingFunction = SmoothEase;

                ThicknessAnimation CSGOPos = new ThicknessAnimation();
                CSGOPos.From = new Thickness(65, 107, 472, 109);
                CSGOPos.To = new Thickness(135, 88, 373, 88);
                CSGOPos.Duration = TimeSpan.FromSeconds(0.25);
                CSGOPos.EasingFunction = SmoothEase;

                ThicknessAnimation TF2Pos = new ThicknessAnimation();
                TF2Pos.From = new Thickness(373, 88, 135, 88);
                TF2Pos.To = new Thickness(472, 107, 65, 109);
                TF2Pos.Duration = TimeSpan.FromSeconds(0.25);
                TF2Pos.EasingFunction = SmoothEase;

                TF2.BeginAnimation(MarginProperty, TF2Pos);
                TF2.BeginAnimation(OpacityProperty, Pos52Opacity);

                CSGO.BeginAnimation(MarginProperty, CSGOPos);
                CSGO.BeginAnimation(OpacityProperty, Pos25Opacity);

                GMOD.BeginAnimation(MarginProperty, GMODPos);
                GMOD.BeginAnimation(OpacityProperty, Pos15Opacity);

                GTAV.BeginAnimation(MarginProperty, GTAPos);
                GTAV.BeginAnimation(OpacityProperty, Pos51Opacity);

                Canvas.SetZIndex(GTAV, 3);
                Canvas.SetZIndex(GMOD, 2);
                Canvas.SetZIndex(CSGO, 2);
                Canvas.SetZIndex(TF2, 1);

                REInjectorLabel.Content = "ZeroTwo Menu (CheatSquad.gg)";

            }

            if (CarouselID == 3)
            {
                CarouselID = 2;

                ThicknessAnimation GMODPos12 = new ThicknessAnimation();
                GMODPos12.From = new Thickness(135, 88, 373, 88);
                GMODPos12.To = new Thickness(228, 53, 227, 53);
                GMODPos12.Duration = TimeSpan.FromSeconds(0.25);
                GMODPos12.EasingFunction = SmoothEase;

                ThicknessAnimation TF2Pos23 = new ThicknessAnimation();
                TF2Pos23.From = new Thickness(228, 53, 227, 53);
                TF2Pos23.To = new Thickness(373, 88, 135, 88);
                TF2Pos23.Duration = TimeSpan.FromSeconds(0.25);
                TF2Pos23.EasingFunction = SmoothEase;

                ThicknessAnimation GTAPos = new ThicknessAnimation();
                GTAPos.From = new Thickness(65, 107, 472, 109);
                GTAPos.To = new Thickness(135, 88, 373, 88);
                GTAPos.Duration = TimeSpan.FromSeconds(0.25);
                GTAPos.EasingFunction = SmoothEase;

                CSGO.BeginAnimation(OpacityProperty, Pos02Opacity);

                GTAV.BeginAnimation(MarginProperty, GTAPos);
                GTAV.BeginAnimation(OpacityProperty, Pos52Opacity);

                TF2.BeginAnimation(MarginProperty, TF2Pos23);
                TF2.BeginAnimation(OpacityProperty, Pos15Opacity);

                GMOD.BeginAnimation(MarginProperty, GMODPos12);
                GMOD.BeginAnimation(OpacityProperty, Pos51Opacity);

                Canvas.SetZIndex(GMOD, 3);
                Canvas.SetZIndex(TF2, 2);

                REInjectorLabel.Content = "AOSMultiHack (CheatSquad.gg)";

            }

        }

        private void REInjectSelect_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new WebClient())
            {
                if (CarouselID == 0)
                {
                    Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\MagmaResources\\CSGO\\");
                    client.DownloadFile("https://github.com/Not316tb/MagmaResources/raw/main/CSGO/CSGhostv4.2.exe", $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\MagmaResources\\CSGO\\CSGhost.exe");

                    Process[] processes = Process.GetProcessesByName("csgo.exe");
                    if (processes.Length <= 0)
                    {
                        MessageBox.Show("Doesn't look like CS:GO is running, make sure you have the game open and your antivirus disabled.", "Err: Process Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else if (CarouselID == 1)
                {
                    Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\MagmaResources\\GTAV\\Kiddions\\");
                    client.DownloadFile("https://github.com/Not316tb/MagmaResources/raw/main/GTAV/modest-menu.exe", $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\MagmaResources\\GTAV\\Kiddions\\modest-menu.exe");
                    client.DownloadFile("https://github.com/Not316tb/MagmaResources/raw/main/GTAV/config.json", $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\MagmaResources\\GTAV\\Kiddions\\config.json");

                    Process[] processes = Process.GetProcessesByName("gtav.exe");
                    if (processes.Length <= 0)
                    {
                        MessageBox.Show("Doesn't look like GTAV is running, make sure you have the game open and your antivirus disabled.", "Err: Process Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else if (CarouselID == 2)
                {
                    Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\MagmaResources\\GMOD\\");
                    client.DownloadFile("https://github.com/Not316tb/MagmaResources/raw/main/GMOD/AOSMultiHack.dll", $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\MagmaResources\\GMOD\\AOSMultiHack.dll");

                    Process[] processes = Process.GetProcessesByName("hl2.exe");
                    if (processes.Length <= 0)
                    {
                        MessageBox.Show("Doesn't look like Garry's Mod is running, make sure you have the game open and your antivirus disabled.", "Err: Process Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }
        }
    }
}
