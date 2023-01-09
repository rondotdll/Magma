using System;
using Wpf.Ui.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
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
            Internal.AcrylicTint = Internal.RGBToWinHex(20, 21, 23);
            if (!Internal.DropShadow(this))
            {
                Window window = (Window)sender;

                Internal.DropShadow(window); 
            }

            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Internal.EnableBlur(this);
            MainContainer.Navigate(Globals.ExecutorPage);
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
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
            this.WindowState = WindowState.Minimized;
        }

        private void KeepOnTopButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (((Window)sender).WindowState == WindowState.Maximized)
            {
                OuterGrid.Margin = new Thickness(5, 5, 5, 5);
                TopBar.Margin = new Thickness(0, 0, 0, 0);
                Internal.DisableBlur(this);
            } else
            {
                OuterGrid.Margin = new Thickness(0, 0, 0, 0);
                TopBar.Margin = new Thickness(35, 0, 0, 0);
                Internal.EnableBlur(this);
            }

        }
    }
}
