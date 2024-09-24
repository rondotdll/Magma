using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Magma
{
    /// <summary>
    /// Interaction logic for MagmaMixSideBar.xaml
    /// </summary>
    public partial class MagmaMixSideBar : Page
    {
        public static ListBoxManager ScriptsManager;

        private static string ScriptsFolder = System.IO.Path.GetFullPath("./Scripts");

        public MagmaMixSideBar()
        {
            InitializeComponent();
        }
        
        private void Page_Initialized(object sender, EventArgs e)
        {
            ScriptsManager = new ListBoxManager(ref CheatsListBox);

            // Populate the listbox from parsed JSON file containing "mix" repos
            ScriptsManager.PopulateFromDirectory("C:\\Users\\David\\Documents\\GitHub\\Magma-v2.1\\Magma UI\\bin\\Debug UI\\Scripts", " *.txt | *.lua");

            CheatsListBox.Padding = new Thickness(0, 0, -2, 0);

            if (CheatsScrollViewer.ComputedVerticalScrollBarVisibility == Visibility.Visible)
                CheatsListBox.Padding = new Thickness(0, 0, 3, 0);
        }

        private void SearchBarTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EzAnimate.FadeOut(SettingsButtonBorder);
            EzAnimate.ResizeMargins(SearchBarBorder, new Thickness(10, 0, -35, 10));
        }

        private void SearchBarBorder_LostFocus(object sender, RoutedEventArgs e)
        {
            EzAnimate.ResizeMargins(SearchBarBorder, new Thickness(10, 0, 5, 10));
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

            if (CheatsListBox.SelectedIndex < 0)
                return;

            if (File.Exists($"{ScriptsFolder}/{scriptName}.txt"))
                Executor.TabManager.Selected.TextEditor.Text = File.ReadAllText($"{ScriptsFolder}/{scriptName}.txt");
            else if (File.Exists($"{ScriptsFolder}/{scriptName}.lua"))
                Executor.TabManager.Selected.TextEditor.Text = File.ReadAllText($"{ScriptsFolder}/{scriptName}.lua");
            else
                MessageBox.Show("The cheat you selected doesn't exist... did you remove it on accident?", "Cheat Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
