using System;
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
    /// Interaction logic for ExecutorSideBar.xaml
    /// </summary>
    public partial class ExecutorSideBar : Page
    {
        private ListBoxManager ScriptsManager; 

        public ExecutorSideBar()
        {
            InitializeComponent();
        }
        
        private void Page_Initialized(object sender, EventArgs e)
        {
            ScriptsManager = new ListBoxManager(ref ScriptsListBox);

            ScriptsListBox.Padding = new Thickness(0, 0, -2, 0);

            if (ScriptsScrollViewer.ComputedVerticalScrollBarVisibility == Visibility.Visible)
                ScriptsListBox.Padding = new Thickness(0, 0, 3, 0);
        }

        private void SearchBarTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EzAnimate.FadeOut(SettingsButtonBorder);
            EzAnimate.ResizeX(SearchBarBorder, 120);
        }

        private void SearchBarBorder_LostFocus(object sender, RoutedEventArgs e)
        {
            EzAnimate.ResizeX(SearchBarBorder, 80);
            EzAnimate.FadeIn(SettingsButtonBorder);
            
        }
    }
}
