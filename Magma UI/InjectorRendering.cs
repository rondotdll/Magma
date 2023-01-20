//using System;
//using System.Windows;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Newtonsoft.Json;
//using System.Threading.Tasks;
//using System.Windows.Controls;
//using Newtonsoft.Json.Linq;
//using System.Windows.Media.Effects;
//using System.Windows.Media;
//using System.Diagnostics;
//using System.ComponentModel;
//using System.Windows.Media.Imaging;
//using System.Windows.Media.Animation;

//namespace Magma
//{

//    class Cheat
//    {
//        public string Name { get; private set; }
//        public int Index { get; private set; }
//        public bool Working { get; private set; }
//        public BitmapImage Artwork { get; private set; }
//        public string Configs { get; private set; }
//        public string Remote { get; private set; }
        

//        public Cheat(string name, int index, bool working, BitmapImage artwork, string configs, string remote)
//        {
//            this.Name = name;
//            this.Index = index;
//            this.Working = working;
//            this.Artwork = artwork;
//            this.Configs = configs;
//            this.Remote = remote;
//        }
//    }

//    // [+]=======================================================================================[+]
//    //
//    //       CheatManager Class (Helps making UI modifications for artwork and buttons easier)
//    //
//    // [+]=======================================================================================[+]
//    class CheatManager
//    {
//        // [+]===============================================================================================[+]
//        //
//        //       Static Variables (These variables are static, and have no relevance to the class itself.)
//        //
//        // [+]===============================================================================================[+]

//        private static JArray SampleData = new JArray();

//        private static Dictionary<bool, SolidColorBrush> FavoriteButtonColors = new Dictionary<bool, SolidColorBrush>()
//        {
//            { true, new SolidColorBrush(Color.FromRgb(239, 71, 111)) },
//            { false, new SolidColorBrush(Color.FromArgb(64, 228, 232, 240)) },
//        };

//        private static DoubleAnimation FadeInAnimation = new DoubleAnimation
//        {
//            From = 0.0,
//            To = 1.0,
//            FillBehavior = FillBehavior.Stop,
//            BeginTime = TimeSpan.FromSeconds(0),
//            Duration = new Duration(TimeSpan.FromSeconds(0.15))
//        };
//        private static DoubleAnimation FadeOutAnimation = new DoubleAnimation
//        {
//            From = 1.0,
//            To = 0.0,
//            FillBehavior = FillBehavior.Stop,
//            BeginTime = TimeSpan.FromSeconds(0),
//            Duration = new Duration(TimeSpan.FromSeconds(0.15))
//        };

//        // [+]=================================================================================[+]
//        //
//        //       Class Variables (These variables are actually relevant to the class itself)
//        //
//        // [+]=================================================================================[+]

//        private Image Canvas; // Variable for the Image object which holds each cheat's artwork
//        private Grid Container; // Variable for the Grid object which contains the Image (Canvas), as well as the like and config buttons

//        public List<Cheat> Available { get; private set; } // Represents all currently available cheats for the selected game
//        public string Game { get; private set; } // Represents the Game that the currently available cheats are for

//        public Cheat Selected { get; private set; } // Represents the currently selected cheat (whichever one is showing in the UI)

//        public CheatManager(Page context, string game) // Class Initializer for the CheatManager
//        {
//            if (context != Globals.InjectorPage) // Verify that the context is for the correct window (or else our code would McShit™️ itself)
//            {
//                throw new Exception("Invalid Context.");
//            }

//            // Reset the Available list (just to be safe, as it shouldn't be populated yet)
//            this.Available = new List<Cheat>();
//            this.Available.Clear();

//            // Define our Canvas and Grid Container objects
//            this.Canvas = Globals.InjectorPage.E_CheatCanvas;
//            this.Container = Globals.InjectorPage.E_CheatContainer;

//            string jsonstr = @"
//            {
//                'd': [
//                    {
//                        'name': 'Osiris',
//                        'working': true,
//                        'image': 'https://github.com/s7davidj/MagmaResources/raw/main/Images/Osiris.png',
//                        'configs': 'https://google.com/'
//                    },{
//                        'name': 'RaweTrip',
//                        'working': true,
//                        'image': 'https://github.com/s7davidj/MagmaResources/raw/main/Images/RaweTrip.png',
//                        'configs': 'https://studio7.dev/'
//                    },{
//                        'name': 'Airflow',
//                        'working': true,
//                        'image': 'https://github.com/s7davidj/MagmaResources/raw/main/Images/Airflow.png',
//                        'configs': 'https://github.com/'
//                    }
//                ]
//            }";

//            SampleData = (JArray)JObject.Parse(jsonstr)["d"];

//            int i = 0;

//            if (game == "")
//            {
//                ShowCheatButtons(false);
//                Globals.InjectorPage.E_NextCheat.IsEnabled = false;
//                Globals.InjectorPage.E_NextCheat.Foreground = new SolidColorBrush(Color.FromArgb(51, 228, 232, 240));
//                Globals.InjectorPage.E_PrevCheat.IsEnabled = false;
//                Globals.InjectorPage.E_PrevCheat.Foreground = new SolidColorBrush(Color.FromArgb(51, 228, 232, 240));
//                Globals.InjectorPage.E_AddCheat.IsEnabled = false;
//                Globals.InjectorPage.E_AddCheat.Foreground = new SolidColorBrush(Color.FromArgb(51, 228, 232, 240));

//                Canvas.Source = new BitmapImage(new Uri("pack://application:,,,/Images/NoGame.png"));
//                return;
//            }

//            if (SampleData.Count() == 0)
//            {
//                ShowCheatButtons(false);
//                Globals.InjectorPage.E_NextCheat.IsEnabled = false;
//                Globals.InjectorPage.E_NextCheat.Foreground = new SolidColorBrush(Color.FromArgb(51, 228, 232, 240));
//                Globals.InjectorPage.E_PrevCheat.IsEnabled = false;
//                Globals.InjectorPage.E_PrevCheat.Foreground = new SolidColorBrush(Color.FromArgb(51, 228, 232, 240));
//                Globals.InjectorPage.E_AddCheat.IsEnabled = false;
//                Globals.InjectorPage.E_AddCheat.Foreground = new SolidColorBrush(Color.FromArgb(51, 228, 232, 240));

//                Canvas.Source = new BitmapImage(new Uri("pack://application:,,,/Images/None.png"));
//                return;
//            }

//            foreach (JObject cheat in SampleData)
//            {
//                Available.Add(new Cheat(
//                    (string)cheat["name"], 
//                    i, 
//                    (bool)cheat["working"],
//                    new BitmapImage(new Uri((string)cheat["image"], UriKind.Absolute)), 
//                    (string)cheat["configs"], 
//                    (string)cheat["source"]
//                ));

//                i++;
//            }

//            ChangeSelected(Available.First());
//        }

//        public void NextCheat()
//        {

//            if (Available.Count() == 0)
//            {
//                ShowCheatButtons(false);
//                return;
//            }

//            if (this.Selected.Index >= Available.Count() - 1)
//                return;

//            ShowCheatButtons(true);
//            this.Selected = Available[this.Selected.Index + 1];
//            Globals.InjectorPage.E_VTStatusRing.IsIndeterminate = true;
//            UpdateCanvas();
//        }
//        public void PreviousCheat()
//        {

//            if (Available.Count() == 0)
//            {
//                ShowCheatButtons(false);
//                return;
//            }

//            if (this.Selected.Index <= 0)
//                return;

//            ShowCheatButtons(true);
//            this.Selected = Available[this.Selected.Index - 1];
//            Globals.InjectorPage.E_VTStatusRing.IsIndeterminate = true;
//            UpdateCanvas();
//        }

//        public void ChangeSelected(Cheat cheat)
//        {
//            try
//            {
//                this.Selected = cheat;
//                this.Canvas.Source = cheat.Artwork;
//                ShowCheatButtons(true);
//                UpdateCanvas();
//            } 
//            catch
//            {
//                return;
//            }
//        }

//        public void UpdateCanvas()
//        {
//            var FirstStoryBoard = new Storyboard();

//            FirstStoryBoard.Children.Add(FadeOutAnimation);

//            Storyboard.SetTarget(FadeOutAnimation, this.Container);
//            Storyboard.SetTargetProperty(FadeOutAnimation, new PropertyPath(Image.OpacityProperty));
//            FirstStoryBoard.Completed += delegate
//            {
//                this.Canvas.Source = this.Selected.Artwork;

//                if (Available.Count() == 0)
//                    this.Canvas.Source = new BitmapImage(new Uri("pack://application:,,,/Images/None.png")); ;

//                var SecondStoryBoard = new Storyboard();

//                SecondStoryBoard.Children.Add(FadeInAnimation);
//                Storyboard.SetTarget(FadeInAnimation, this.Container);
//                Storyboard.SetTargetProperty(FadeInAnimation, new PropertyPath(Image.OpacityProperty));
//                SecondStoryBoard.Completed += delegate
//                {
//                    Globals.InjectorPage.E_VTStatusRing.IsIndeterminate = false;
//                };
//                SecondStoryBoard.Begin();
//            };

//            FirstStoryBoard.Begin();
//        }

//        private void ShowCheatButtons(bool visible)
//        {
//            if (!visible)
//            {
//                Globals.InjectorPage.E_FavoriteButton.Visibility = Visibility.Hidden;
//                Globals.InjectorPage.E_BrowseConfigsButton.Visibility = Visibility.Hidden;
//            } else
//            {
//                Globals.InjectorPage.E_FavoriteButton.Visibility = Visibility.Visible;
//                Globals.InjectorPage.E_BrowseConfigsButton.Visibility = Visibility.Visible;
//            }
//        }
//    }
//}
