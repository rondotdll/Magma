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
    /// Interaction logic for Injector.xaml
    /// </summary>
    public partial class Injector : Page
    {

        //private CheatManager GameCheats;

        public Injector()
        {
            InitializeComponent();
        }

        private void InjectorPage_Initialized(object sender, EventArgs e)
        {
            //E_VTStatusRing.ToolTip = new ToolTip(){ Content = "Sample Tooltip" };
        }

        private void InjectorPage_Loaded(object sender, EventArgs e)
        {
            // Creates a new "CheatManager" which is just makes it a bit easier to load / unload stuff from the UI
            // If I hadn't abstracted this class away this file would have gotten VERY messy.
            //GameCheats = new CheatManager(this, "csgo");
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void E_NextCheat_Click(object sender, RoutedEventArgs e)
        {
            //GameCheats.NextCheat();
            //E_LaunchButton.Content = $"LAUNCH {GameCheats.Selected.Name.ToUpper()}";

            //E_PrevCheat.IsEnabled = true;
            //E_PrevCheat.Foreground = new SolidColorBrush(Color.FromArgb(204, 228, 232, 240));

            //if (GameCheats.Selected.Index + 1 >= GameCheats.Available.Count())
            //{
            //    E_NextCheat.IsEnabled = false;
            //    E_NextCheat.Foreground = new SolidColorBrush(Color.FromArgb(51, 228, 232, 240));
            //} else
            //{
            //    E_NextCheat.IsEnabled = true;
            //    E_NextCheat.Foreground = new SolidColorBrush(Color.FromArgb(204, 228, 232, 240));
            //}
        }

        private void E_PrevCheat_Click(object sender, RoutedEventArgs e)
        {
            //GameCheats.PreviousCheat();
            //E_LaunchButton.Content = $"LAUNCH {GameCheats.Selected.Name.ToUpper()}";

            //E_NextCheat.IsEnabled = true;
            //E_NextCheat.Foreground = new SolidColorBrush(Color.FromArgb(204, 228, 232, 240));

            //if (GameCheats.Selected.Index <= 0)
            //{
            //    E_PrevCheat.IsEnabled = false;
            //    E_PrevCheat.Foreground = new SolidColorBrush(Color.FromArgb(51, 228, 232, 240));
            //}
            //else
            //{
            //    E_PrevCheat.IsEnabled = true;
            //    E_PrevCheat.Foreground = new SolidColorBrush(Color.FromArgb(204, 228, 232, 240));
            //}
        }
    }
}
