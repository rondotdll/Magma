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

namespace Magma.Controls    
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class GameCard : UserControl
    {
        public GameCard()
        {
            InitializeComponent();
        }

        // Dependency property for ImageSource
        public static readonly DependencyProperty ImageSource_P =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(GameCard),
                new PropertyMetadata(null));

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSource_P); }
            set { SetValue(ImageSource_P, value); }
        }

        // Dependency property for CornerRadius
        public static readonly DependencyProperty BorderRadius_P =
            DependencyProperty.Register("BorderRadius_P", typeof(CornerRadius), typeof(GameCard),
                new PropertyMetadata(null));

        public CornerRadius BorderRadius
        {
            get { return (CornerRadius)GetValue(BorderRadius_P); }
            set { SetValue(BorderRadius_P, value); }
        }

}
}
