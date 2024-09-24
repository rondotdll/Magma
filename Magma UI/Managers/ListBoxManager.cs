using System;
using System.Collections.Generic;
using System.Windows;
using MaterialDesignThemes.Wpf;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using System.Linq;

/* This class just abstracts away the logic for my custom listbox styling
 */

namespace Magma
{
    public class ListBoxManager
    {
        // The list box we are managing
        public ListBox ListBox;

        // Because of nesting BS, this just selects the TextBlock inside of the selected ListBoxItem
        public TextBlock SelectedItem;

        // This is a representation of everything that *can* be rendered in the ListBox at a given point
        public List<ListBoxItem> CachedItems = new List<ListBoxItem>();

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

        // idk, makes it slightly easier to get the listbox items
        public ItemCollection Items {
            get {
                return this.ListBox.Items;
            } 
        }

        // Class initializer
        public ListBoxManager(ref ListBox listBox)
        {
            this.ListBox = listBox;
            this.Items.Clear();
        }
        
        // This will render only the items that contain the search string
        public void FilterItems(string search = "")
        {

            this.Items.Clear();
            
            foreach (ListBoxItem item in CachedItems)
            {
                if (((TextBlock)((Grid)item.Content).Children[1]).Text.Contains(search))
                    this.Items.Add(item);
            }

        }

        // Sets the items of a listbox using a list of strings
        public void Populate(List<string> data)
        {
            this.Items.Clear();
            this.CachedItems.Clear();

            foreach (var item in data)
            {
                // this is just styling garbage, don't be alarmed lol
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
                            // More styling garbage
                            new PackIcon()
                            {
                                Kind = PackIconKind.HandPointingRight,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                Width = 16,
                                Height = 16,
                                Foreground = ListBoxItemTextBrushes[0],
                                VerticalAlignment = VerticalAlignment.Center,
                            },
                            // Even more styling garbage
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

                /* There isn't really a good way to describe this, but basically this controls the visibility of the little pointer hand icon
                 * whenever you select X listbox item.
                 */

                // Makes the selected pointer icon initially invisible
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
                    this.SelectedItem = (TextBlock)((Grid)((ListBoxItem)sender).Content).Children[1];
                    ToggleItemHighlight((ListBoxItem)sender, true);
                };

                newItem.Unselected += (sender, e) =>
                {
                    ToggleItemHighlight((ListBoxItem)sender, false);
                };

                // Add our items to the global instance
                this.Items.Add(newItem);
                this.CachedItems.Add(newItem);
            }
        }

        // Use your context clues
        public void PopulateFromDirectory(string directory, string fileEnding = "*.*")
        {
            DirectoryInfo dinfo = new DirectoryInfo(directory);
            FileInfo[] Files = { };

            //// This allows you to use multiple file endings (ex: "*.txt | *.lua")
            //foreach (string ending in fileEnding.Replace(" ", "").Split('|'))
            //{
            //    Files = Files.Concat(dinfo.GetFiles(ending)).ToArray();
            //}

            //List<string> FileNames = new List<string>();

            //foreach (FileInfo file in Files)
            //{
            //    // Adds the current file's name without the ".*" ending
            //    FileNames.Add(string.Join(".", (string[])file.Name.Split('.').Take(file.Name.Split('.').Length - 1).ToArray()));
            //}

            //this.Populate(FileNames);
        }

        // Manages selected vs deselected item styling
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
