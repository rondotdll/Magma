using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Controls;
using Newtonsoft.Json.Linq;
using System.Windows.Media.Effects;
using System.Windows.Media;
using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
using System.Diagnostics;

namespace Magma
{
    class CheatCollection
    {
        private JArray SampleData = new JArray();
        private Dictionary<string, SolidColorBrush> TypeColor = new Dictionary<string, SolidColorBrush>()
        {
            { "legit", new SolidColorBrush(Color.FromRgb(128, 147, 241)) },
            { "hybrid", new SolidColorBrush(Color.FromRgb(179, 136, 235)) },
            { "rage", new SolidColorBrush(Color.FromRgb(247, 174, 248)) }
        };
        private Dictionary<string, PackIconMaterialKind> TypeIcon = new Dictionary<string, PackIconMaterialKind>()
        {
            { "legit", PackIconMaterialKind.TargetAccount},
            { "hybrid", PackIconMaterialKind.YinYang},
            { "rage", PackIconMaterialKind.RobotDead},
        };
        private Dictionary<string, SolidColorBrush> StatusColor = new Dictionary<string, SolidColorBrush>()
        {
            { "working", new SolidColorBrush(Color.FromRgb(6, 214, 160)) },
            { "unstable", new SolidColorBrush(Color.FromRgb(255, 209, 102)) },
        };
        private Dictionary<bool, SolidColorBrush> FavoriteButtonColors = new Dictionary<bool, SolidColorBrush>()
        {
            { true, new SolidColorBrush(Color.FromRgb(239, 71, 111)) },
            { false, new SolidColorBrush(Color.FromArgb(64, 228, 232, 240)) },
        };

        public CheatCollection(ref Grid Container, string Game)
        {
            Container.Children.Clear();

            string jsonstr = @"
            {
                'd': [
                    {
                        'name': 'Osiris',
                        'type': 'legit',
                        'status': 'working',
                        'configs': 'https://google.com/'
                    },{
                        'name': 'RaweTrip',
                        'type': 'hybrid',
                        'status': 'unstable',
                        'configs': 'https://studio7.dev/'
                    },{
                        'name': 'Airflow',
                        'type': 'rage',
                        'status': 'working',
                        'configs': 'https://github.com/'
                    }
                ]
            }";

            SampleData = (JArray)JObject.Parse(jsonstr)["d"];

            int i;
            for (i=0; i < SampleData.Count; i++)
            {
                Container.RowDefinitions.Add(new RowDefinition()
                {
                    Height = new GridLength(122.5, GridUnitType.Pixel)
                }); 
            }

            i=0;
            foreach (JObject cheat in SampleData)
            {
                var Inner = new Grid()
                {
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    ClipToBounds = true
                };

                // Title Label
                Inner.Children.Add(new Label()
                {
                    Content = cheat["name"],
                    FontFamily = (FontFamily)Application.Current.FindResource("JBMono"),
                    FontWeight = FontWeights.Normal,
                    FontSize = 30,
                    Margin = new Thickness(8, 6, 0, 0),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                });

                // Type Badge
                string type = cheat["type"].ToString().ToLower();
                Inner.Children.Add(new Wpf.Ui.Controls.Badge()
                {
                    Content = type.ToUpper(),
                    FontFamily = (FontFamily)Application.Current.FindResource("QuickSand"),
                    FontWeight = FontWeights.Bold,
                    FontSize = 12,
                    Background = null,
                    Foreground = TypeColor[type],
                    Margin = new Thickness(0, 0, 8, 32),
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Bottom,
                });

                // Type BG Icon
                Inner.Children.Add(new PackIconMaterial()
                {
                    Width = 150,
                    Height = 150,
                    Margin = new Thickness(0, 0, 0, -50),
                    Foreground = new SolidColorBrush(Color.FromArgb(5, 255, 255, 255)),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    Kind = TypeIcon[type]
                });

                // Status Badge
                string status = cheat["status"].ToString().ToLower();
                Inner.Children.Add(new Wpf.Ui.Controls.Badge()
                {
                    Content = status.ToUpper(),
                    FontFamily = (FontFamily)Application.Current.FindResource("QuickSand"),
                    FontWeight = FontWeights.Bold,
                    FontSize = 12,
                    Background = null,
                    Foreground = StatusColor[status],
                    Margin = new Thickness(0, 0, 8, 10),
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Bottom,
                });

                // Browse Configs Button
                Inner.Children.Add(CreateCardButton(1, cheat["configs"].ToString()));

                // Favorite (Heart) Button
                Inner.Children.Add(CreateCardButton(2));


                var Card = new Border()
                {
                    //Effect = new DropShadowEffect()
                    //{
                    //    ShadowDepth = 0,
                    //    Direction = 0,
                    //    BlurRadius = 15,
                    //    Color = StatusColor[status].Color,
                    //    Opacity = 0.25
                    //},
                    //BorderBrush = new LinearGradientBrush()
                    //{
                    //    StartPoint = new Point(0, 0),
                    //    EndPoint = new Point(0, 0),
                    //    GradientStops = new GradientStopCollection()
                    //    {
                    //        new GradientStop()
                    //        {
                    //            Offset = 0.33,
                    //            Color = Color.FromArgb(23, 255, 255, 255)
                    //        },
                    //        new GradientStop()
                    //        {
                    //            Offset = 1,
                    //            Color = Color.FromArgb(18, 255, 255, 255)
                    //        }
                    //    }
                    //},
                    Child = Inner,
                    Margin = new Thickness(0, 0, 10, 20),
                    //BorderThickness = new Thickness(1, 1, 1, 1),
                    CornerRadius = new CornerRadius(4),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    Background = new SolidColorBrush(Color.FromRgb(53, 55, 59)),
                };

                Container.Children.Add(Card);
                Grid.SetRow(Card, i);
                i++;
            }
        }

        private Button CreateCardButton(int type, string url = "")
        {
            Button Output = new Button()
            {
                Padding = new Thickness(0, 0, 0, 0),
                
                Width = 30,
                Height = 30,
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Left,
                Background = null,
                BorderBrush = null,
                
            };

            switch (type)
            {
                case 1:
                    Output.Margin = new Thickness(8, 0, 0, 8);
                    Output.Foreground = new SolidColorBrush(Color.FromArgb(64, 228, 232, 240));
                    Output.Click += (object sender, RoutedEventArgs e) =>
                    {
                        Process.Start(url);
                    };

                    Output.Content = new PackIconFontAwesome()
                    {
                        Kind = PackIconFontAwesomeKind.PasteSolid,
                        Height = 18,
                        Width = 18,
                    };
                    break;

                case 2:
                    Output.Margin = new Thickness(46, 0, 0, 8);
                    Output.Foreground = FavoriteButtonColors[true];

                    Output.Content = new PackIconFontAwesome()
                    {
                        Kind = PackIconFontAwesomeKind.HeartRegular,
                        Height = 18,
                        Width = 18,
                    };

                    if (true)
                        ((PackIconFontAwesome)Output.Content).Kind = PackIconFontAwesomeKind.HeartSolid;
                    break;

                default:
                    return null;
            }

            return Output;
        }
    }
}
