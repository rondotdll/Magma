using System;
using System.Collections.Generic;
using System.Windows;
using MaterialDesignThemes.Wpf;
using System.Windows.Controls;
using System.Windows.Media;

namespace Magma
{
    internal class ListBoxManager
    {
        public ListBox ListBox;


        private List<SolidColorBrush> ListBoxItemTextBrushes = new List<SolidColorBrush>
        {
            (SolidColorBrush)Application.Current.Resources["TextSuperLightBrush"], // Deselected
            (SolidColorBrush)Application.Current.Resources["TextLightBrush"] // Selected
        };

        private List<FontWeight> ListBoxItemFontWeights = new List<FontWeight>
        {
            FontWeights.Regular, // Deselected
            FontWeights.SemiBold // Selected
        };

        private List<GridLength> ListBoxItemColumnWidths = new List<GridLength>()
        {
            new GridLength(0, GridUnitType.Pixel), // Deselected
            new GridLength(20, GridUnitType.Pixel), // Selected
            new GridLength(1, GridUnitType.Star)

        };

        public ItemCollection Items {
            get {
                return this.ListBox.Items;
            } 
        }

        public ListBoxManager(ref ListBox listBox)
        {
            this.ListBox = listBox;

            this.ListBox.Items.Clear();

            this.Populate(new List<string>{
                "$ Dark Dex",
                "$ RemoteSpy",
                "1.) InfiniteYield",
                "2.) DarkHub",
                "3.) PrisonBreaker",
                "4.) FE Audio Spam",
                "5.) ReAnimation",
                "6.) NetBypass",
                "Prison Life EarRape",
            });
        }

        public void Populate(List<string> data)
        {
            foreach (var item in data)
            {
                ListBoxItem newItem = new ListBoxItem
                {

                    Height = 38,
                    Background = null,
                    Padding = new Thickness(5, 0, 0, 0),
                    Content = new Grid()
                    {
                        Height = 32,
                        ClipToBounds = true,
                        Children =
                        {
                            new PackIcon()
                            {
                                Kind = PackIconKind.HandPointingRight,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                Width = 16,
                                Height = 16,
                                Foreground = ListBoxItemTextBrushes[0],
                                VerticalAlignment = VerticalAlignment.Center,
                            },
                            new TextBlock()
                            {
                                Margin = new Thickness(7, 0, 0, 0),
                                FontWeight = ListBoxItemFontWeights[0],
                                TextWrapping = TextWrapping.NoWrap,
                                FontSize = 12,
                                Foreground = ListBoxItemTextBrushes[0],
                                FontFamily = (FontFamily)Application.Current.Resources["Poppins"],
                                VerticalAlignment= VerticalAlignment.Center,
                                Padding = new Thickness(0),
                                Text = item
                            }
                        },
                    },
                };

                ((Grid)newItem.Content).ColumnDefinitions.Add(
                    new ColumnDefinition() { 
                        Width = ListBoxItemColumnWidths[0]
                    });
                
                ((Grid)newItem.Content).ColumnDefinitions.Add(
                    new ColumnDefinition() { 
                        Width = ListBoxItemColumnWidths[2]
                    });

                Grid.SetColumn((PackIcon)((Grid)newItem.Content).Children[0], 0);
                Grid.SetColumn((TextBlock)((Grid)newItem.Content).Children[1], 1);


                newItem.Selected += (sender, e) =>
                {
                    ToggleItemHighlight((ListBoxItem)sender, true);
                };

                newItem.Unselected += (sender, e) =>
                {
                    ToggleItemHighlight((ListBoxItem)sender, false);
                };

                this.Items.Add(newItem);
            }
        }

        private void ToggleItemHighlight(ListBoxItem target, bool highlighted)
        {
            int index = Convert.ToInt32(highlighted);

            var grid = (Grid)target.Content;
            var packIcon = (PackIcon)grid.Children[0];
            var textBlock = (TextBlock)grid.Children[1];

            packIcon.Foreground = ListBoxItemTextBrushes[index];

            textBlock.Foreground = ListBoxItemTextBrushes[index];
            textBlock.FontWeight = ListBoxItemFontWeights[index];

            // ik, this is clunky and gross to look at. Weird shit was happening if script names were too long, and this was the only way I could fix it.
            // If you find a better way, feel free to submit a push request

            grid.ColumnDefinitions.Clear();

            grid.ColumnDefinitions.Add(new ColumnDefinition()
            {
                Width = ListBoxItemColumnWidths[index]
            });
            grid.ColumnDefinitions.Add(new ColumnDefinition()
            {
                Width = ListBoxItemColumnWidths[2]
            });

            Grid.SetColumn(packIcon, 0);
            Grid.SetColumn(textBlock, 1);
        }
    }
}
