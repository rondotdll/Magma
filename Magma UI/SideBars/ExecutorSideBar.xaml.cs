using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Magma
{
    /// <summary>
    /// Interaction logic for ExecutorSideBar.xaml
    /// </summary>
    public partial class ExecutorSideBar : Page
    {
        public static ListBoxManager ScriptsManager;

        private static string ScriptsFolder = System.IO.Path.GetFullPath("./Scripts");

        public ExecutorSideBar()
        {
            InitializeComponent();
        }
        
        private void Page_Initialized(object sender, EventArgs e)
        {
            ScriptsManager = new ListBoxManager(ref ScriptsListBox);
            ScriptsManager.PopulateFromDirectory("C:\\Users\\David\\Documents\\GitHub\\Magma-v2.1\\Magma UI\\bin\\Debug UI\\Scripts", " *.txt | *.lua");

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

        private void SearchBarTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ScriptsManager.FilterItems(SearchBarTextBox.Text);
        }

        private void SearchBarButton_Click(object sender, RoutedEventArgs e)
        {
            ScriptsManager.FilterItems(SearchBarTextBox.Text);
        }

        private void ScriptsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string scriptName = ScriptsManager.SelectedItem.Text;

            if (File.Exists($"{ScriptsFolder}/{scriptName}.txt"))
                Executor.TabManager.Selected.TextEditor.Text = File.ReadAllText($"{ScriptsFolder}/{scriptName}.txt");
            else if (File.Exists($"{ScriptsFolder}/{scriptName}.lua"))
                Executor.TabManager.Selected.TextEditor.Text = File.ReadAllText($"{ScriptsFolder}/{scriptName}.lua");
            else
                MessageBox.Show("The script you selected doesn't exist... did you delete it on accident?", "File Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
