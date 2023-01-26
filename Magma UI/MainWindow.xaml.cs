/* Original Name: ScriptHub
 * Current Name: Magma
 * Creator: Studio 7 Development
 * 
 * Version: 2.1.0 [Official Release]
 * 
 * Holy shit bro, the source you see here in v2.1 is COMPLETELY REBUILT 
 * FROM THE GROUND UP, and god DAMN did it take a while. Anyways here
 * you are, have fun I guess.
 * 
 * (Now with actual documentation!)
 * 
 * =====================================================================
 * 
 * A note from david: 
 * 
 * Odly enough, this project is my first ever to be finalized
 * and made availilble to the public. What initially started
 * as an experiment more than anything swiftly grew to much
 * much more.
 * 
 * Magma is, and always will be free and open-sourced
 * meaning that anyone can edit this code and make their
 * own version of this program. This being said, I am
 * aware that there will unfortunately be people who try 
 * and profit off of this code, and if you are someone who
 * has suffered from the wrath of one of these scum bags,
 * I am truly sorry.
 * 
 * While I wish you didn't, I do permit the profitable 
 * redistribution of MODIFIED VERSIONS of Magma, as long as
 * attributions and/or credit is given to "s7davidj", for
 * the creation of the original Magma source.
 * 
 * However, please note that unauthorized redistribution of the 
 * official version of Magma is STRICTLY PROHIBITED.
 * 
 * With that said, have fun making tweaks to my code, I am
 * excited to see what you do with it. Feel free to join my
 * discord server and share your creations with me!
 * 
 * https://discord.gg/H3qPn377Qv
 * 
 * - s7davidj (AKA Not316tb)
 * 
 * =====================================================================
 * 
 * Special Thanks to 
 *   
 *   [+] Legrandite#1362
 *   [+] Kaizer_#6969
 *   [+] redone#9149
 *   [+] SnakyCodes#2518
 *   [+] Diamonds.#7438
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.Expressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Magma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static Application BaseApp = Application.Current; 

        public MainWindow()
        {
            /* Special***
             * Keyword
             * Function
             * Types
             */

            InitializeComponent();
            foreach (CompletionItem item in Globals.Special)
            {
                Globals.MasterCompletionList.Add(item);
            }
            foreach (CompletionItem item in Globals.Keywords)
            {
                Globals.MasterCompletionList.Add(item);
            }
            foreach (CompletionItem item in Globals.Logic)
            {
                Globals.MasterCompletionList.Add(item);
            }
            foreach (CompletionItem item in Globals.Functions)
            {
                Globals.MasterCompletionList.Add(item);
            }
            foreach (CompletionItem item in Globals.Types)
            {
                Globals.MasterCompletionList.Add(item);
            }
            foreach (CompletionItem item in Globals.Strings)
            {
                Globals.MasterCompletionList.Add(item);
            }
            
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            this.BorderThickness = new Thickness(1);
            Internal.AcrylicTint = Internal.RGBToWinHex(20, 21, 23);
            if (!Internal.DropShadow(this))
            {
                Window window = (Window)sender;

                Internal.DropShadow(window); 
            }

            StatusFrame.Navigate(Globals.HomeStatusBar);

            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Internal.EnableBlur(this);
            //MainContainer.Navigate(Globals.ExecutorPage);
        }

        private void DragBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            //this.WindowState = WindowState.Minimized; // Disabled for testing

            EzAnimate.FadeOut(MainFrame, 280, (a, aa) =>
            {
                EzAnimate.ResizeX(FluidContainer, 650, 400, (b, bb) =>
                {
                    MainFrame.Navigate(Globals.ExecutorPage);
                    EzAnimate.FadeIn(MainFrame);
                });
            });

            EzAnimate.FadeOut(StatusFrame, 280, (a, aa) =>
            {
                StatusFrame.Navigate(Globals.ExecutorStatusBar);
                EzAnimate.FadeIn(StatusFrame);
                
            });

            EzAnimate.FadeOut(SideBarFrame, 280, (a, aa) =>
            {
                SideBarFrame.Navigate(Globals.ExecutorSideBarPage);
                EzAnimate.FadeIn(SideBarFrame);

            });


        }

        private void KeepOnTopButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Topmost)
            {
                this.Topmost = false;

                KeepOnTopButton.Foreground = new SolidColorBrush((Color)BaseApp.Resources["TextSuperLight"]);
                KeepOnTopButton.ToolTip = new ToolTip { Content = "Pin Magma to always be visible" };
            } else 
            {
                this.Topmost = true;

                KeepOnTopButton.Foreground = new SolidColorBrush((Color)BaseApp.Resources["TextLight"]);
                KeepOnTopButton.ToolTip = new ToolTip { Content = "UnPin Magma from always being visible" };
            }
        }

        private void LuaTabButton_Click(object sender, RoutedEventArgs e)
        {
            //MainContainer.Navigate(Globals.ExecutorPage);
        }

        private void InjectorTabButton_Click(object sender, RoutedEventArgs e)
        {
            //MainContainer.Navigate(Globals.InjectorPage);
        }

    }
}
