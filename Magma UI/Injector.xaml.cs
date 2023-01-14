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

        private CheatManager GameCheats;

        public Injector()
        {
            InitializeComponent();
        }

        private void InjectorPage_Loaded(object sender, EventArgs e)
        {
             GameCheats = new CheatManager(this, "");
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void E_NextCheat_Click(object sender, RoutedEventArgs e)
        {
            GameCheats.NextCheat();
        }
    }
}
