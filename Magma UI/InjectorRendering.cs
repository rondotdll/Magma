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
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;

namespace Magma
{

    class Cheat
    {
        public string Name { get; private set; }
        public int Index { get; private set; }
        public bool Working { get; private set; }
        public BitmapImage Artwork { get; private set; }
        public string Configs { get; private set; }
        public string Remote { get; private set; }
        

        public Cheat(string name, int index, bool working, BitmapImage artwork, string configs, string remote)
        {
            this.Name = name;
            this.Index = index;
            this.Working = working;
            this.Artwork = artwork;
            this.Configs = configs;
            this.Remote = remote;
        }
    }

    class CheatManager
    {
        private JArray SampleData = new JArray();

        private Dictionary<bool, SolidColorBrush> FavoriteButtonColors = new Dictionary<bool, SolidColorBrush>()
        {
            { true, new SolidColorBrush(Color.FromRgb(239, 71, 111)) },
            { false, new SolidColorBrush(Color.FromArgb(64, 228, 232, 240)) },
        };

        private Image Canvas;
        private Grid Container;

        public List<Cheat> Available { get; private set; }

        public Cheat Selected { get; private set; }

        public CheatManager(Page context, string game)
        {
            if (context != Globals.InjectorPage)
            {
                throw new Exception("Invalid Context.");
            }

            this.Available = new List<Cheat>();
            this.Available.Clear();

            this.Canvas = Globals.InjectorPage.E_CheatCanvas;
            this.Container = Globals.InjectorPage.E_CheatContainer;

            string jsonstr = @"
            {
                'd': [
                    {
                        'name': 'Osiris',
                        'working': true,
                        'image': 'https://github.com/s7davidj/MagmaResources/raw/main/Images/Osiris.png',
                        'configs': 'https://google.com/'
                    },{
                        'name': 'RaweTrip',
                        'working': true,
                        'image': 'https://github.com/s7davidj/MagmaResources/raw/main/Images/RaweTrip.png',
                        'configs': 'https://studio7.dev/'
                    },{
                        'name': 'Airflow',
                        'working': true,
                        'image': 'https://github.com/s7davidj/MagmaResources/raw/main/Images/Airflow.png',
                        'configs': 'https://github.com/'
                    }
                ]
            }";

            SampleData = (JArray)JObject.Parse(jsonstr)["d"];

            int i = 0;

            if (SampleData.Count() == 0)
            {
                ShowCheatButtons(false);

                Canvas.Source = new BitmapImage(new Uri("pack://application:,,,/Images/None.png"));
                return;
            }

            foreach (JObject cheat in SampleData)
            {
                Available.Add(new Cheat(
                    (string)cheat["name"], 
                    i, 
                    (bool)cheat["working"],
                    new BitmapImage(new Uri((string)cheat["image"])), 
                    (string)cheat["configs"], 
                    (string)cheat["source"]
                ));

                i++;
            }

            ChangeSelected(Available);
        }

        public void NextCheat()
        {

            if (Available.Count() == 0)
            {
                ShowCheatButtons(false);
                return;
            }

            if (this.Selected.Index >= Available.Count() - 1)
                return;

            ShowCheatButtons(true);

            var FadeInAnimation = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                FillBehavior = FillBehavior.Stop,
                BeginTime = TimeSpan.FromSeconds(1),
                Duration = new Duration(TimeSpan.FromSeconds(0.5))
            };
            var FadeOutAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                FillBehavior = FillBehavior.Stop,
                BeginTime = TimeSpan.FromSeconds(1),
                Duration = new Duration(TimeSpan.FromSeconds(0.5))
            };

            var FirstStoryBoard = new Storyboard();

            FirstStoryBoard.Children.Add(FadeOutAnimation);

            Storyboard.SetTarget(FadeOutAnimation, this.Container);
            Storyboard.SetTargetProperty(FadeOutAnimation, new PropertyPath(Image.OpacityProperty));
            FirstStoryBoard.Completed += delegate
            {
                this.Canvas.Source = Available[this.Selected.Index + 1].Artwork;
                var SecondStoryBoard = new Storyboard();

                SecondStoryBoard.Children.Add(FadeInAnimation);
                Storyboard.SetTarget(FadeInAnimation, this.Container);
                Storyboard.SetTargetProperty(FadeInAnimation, new PropertyPath(Image.OpacityProperty));
                SecondStoryBoard.Begin();
            };

            FirstStoryBoard.Begin();

        }

        public void ChangeSelected(int index)
        {
            try
            {
                this.Canvas.Source = Available[index].Artwork;
                ShowCheatButtons(true);
            } catch
            {
                return;
            }
        }

        private void ShowCheatButtons(bool visible)
        {
            if (!visible)
            {
                Globals.InjectorPage.E_FavoriteButton.Visibility = Visibility.Hidden;
                Globals.InjectorPage.E_BrowseConfigsButton.Visibility = Visibility.Hidden;
            } else
            {
                Globals.InjectorPage.E_FavoriteButton.Visibility = Visibility.Visible;
                Globals.InjectorPage.E_BrowseConfigsButton.Visibility = Visibility.Visible;
            }
        }
    }
}
