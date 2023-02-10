using System;
using ICSharpCode.AvalonEdit;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Magma.Avalon;
using System.Reflection;
using System.Xml.Linq;

/* The classes in this file exists to help keep Executor.xaml.cs clean.
 * In this file are the classes "Tab" and "TabManager", which are just responsible
 * for handling all of the tab system logic.
 */

namespace Magma
{

    // Represents a Tab and it's contents
    public class Tab
    {
        public Button Button { get; private set; } // this is what actually get's clicked 
        public TextEditor TextEditor { get; private set; } // tab's content

        public int Index { get; private set; }
        public string Name { get; private set; }

        public Tab(string name, PackIconKind icon, int index)
        {
            // This is just a style template (scarier than it looks)
            Button tabButton = new Button()
            {
                BorderThickness = new Thickness(0, 0, 0, 0),
                Margin = new Thickness(10, 10, 10, 0),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Padding = new Thickness(0, 0, 0, 0),
                Background = new SolidColorBrush(Colors.Transparent),
                Content = new Grid()
                {
                    Width = 80,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Children =
                        {
                            new PackIcon()
                            {
                                Foreground = (SolidColorBrush)Application.Current.Resources["TextSuperLightBrush"],
                                Kind = icon,
                                VerticalAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Left,
                            },
                            new Label
                            {
                                FontFamily = (FontFamily)Application.Current.Resources["QuickSand"],
                                Foreground = (SolidColorBrush)Application.Current.Resources["TextSuperLightBrush"],
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Center,
                                Margin = new Thickness(20, 0, 0, 0),
                                Content = name
                            }
                        }
                }
            };

            // Another style template
            TextEditor textEditor = new TextEditor()
            {
                FontSize = 12,
                Margin = new Thickness(5),
                FontFamily = (FontFamily)Application.Current.Resources["JBMono"],
                ShowLineNumbers = true,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                Background = new SolidColorBrush(Colors.Transparent),
                LineNumbersForeground = (SolidColorBrush)Application.Current.Resources["TextSuperDuperLightBrush"],
                Foreground = (SolidColorBrush)Application.Current.Resources["TextSuperLightBrush"],
                Visibility = Visibility.Collapsed,
            };

            // Load the syntax highlighting engine (all the pretty colors :D)
            // -> Link text is seperate from Syntax Highlighting
            textEditor.TextArea.TextView.LinkTextForegroundBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF80CBC4"));
            textEditor.SyntaxHighlighting = AvalonHelpers.LoadEditorTheme("LuaPalenight");
            
            // Key press event handlers (only used for zoom control)
            textEditor.KeyUp += AvalonEvents.TextBox_KeyUp;
            textEditor.KeyDown += AvalonEvents.TextBox_KeyDown;
            
            // These 3 event listeners are for the native autocomplete engine
            textEditor.TextChanged += AvalonEvents.TextBox_TextChanged;

            textEditor.TextArea.TextEntering += AvalonEvents.TextArea_TextEntering;
            textEditor.TextArea.TextEntered += AvalonEvents.TextArea_TextEntered;

            // WPF requires you to set row and column positioning like this, idk why
            Grid.SetColumn(tabButton, index);
            Grid.SetRow(tabButton, 0);
            Grid.SetRow(textEditor, 1);

            // Initialize values
            this.Index = index;
            this.Name = name;
            this.Button = tabButton;
            this.TextEditor = textEditor;
        }

        // Adds the selected style to the tab button
        public Tab Activate()
        {
            ((PackIcon)((Grid)this.Button.Content).Children[0]).Foreground = (SolidColorBrush)Application.Current.Resources["TextLightBrush"];
            ((Label)((Grid)this.Button.Content).Children[1]).Foreground = (SolidColorBrush)Application.Current.Resources["TextLightBrush"];
            this.Button.Background = (SolidColorBrush)Application.Current.Resources["BackgroundSuperContrastBrush"];

            this.TextEditor.Visibility = Visibility.Visible;
            this.TextEditor.Focus();

            return this;
        }

        // Adds the neutral style to the tab button
        public Tab DeActivate()
        {
            ((PackIcon)((Grid)this.Button.Content).Children[0]).Foreground = (SolidColorBrush)Application.Current.Resources["TextSuperLightBrush"];
            ((Label)((Grid)this.Button.Content).Children[1]).Foreground = (SolidColorBrush)Application.Current.Resources["TextSuperLightBrush"];
            this.Button.Background = new SolidColorBrush(Colors.Transparent);

            this.TextEditor.Visibility = Visibility.Collapsed;

            return this;
        }
    }

    // really just a glorified Tab collection
    public class TabManager
    {
        // This is how many tabs we want to create & manage (duh)
        private const int TabCount = 6;

        // The icons displayed on each tab button from L->R
        private readonly List<PackIconKind> TabIcons = new List<PackIconKind>
        {
            PackIconKind.Cheese,
            PackIconKind.Cookie,
            PackIconKind.FruitWatermelon,
            PackIconKind.FriedPotatoes,
            PackIconKind.Rice,
            PackIconKind.IcePop
        };

        // Text displayed on each tab button from L->R
        private readonly List<string> TabLabels = new List<string>
        {
            "Tab A",
            "Tab B",
            "Tab C",
            "Tab D",
            "Tab E",
            "Tab F",
        };

        // All of the tab objects in our container
        public List<Tab> Tabs = new List<Tab>();

        // Currently selected tab (duh)
        public Tab Selected { get; private set; } 
        
        // Represents the Executor page that the TabManager is parented in
        public Executor Context { get; private set; }

        // Class initializer
        public TabManager(Page context)
        {
            if (context != Globals.ExecutorPage) // Verify that the context is for the correct window (or else our code would McShit™️ itself)
            {
                throw new Exception("Invalid Context.");
            }
            this.Context = Globals.ExecutorPage;

            // Wipe everything (in case there are already objects in our tab container)
            this.Context.TabsContainer.ColumnDefinitions.Clear();
            this.Context.TabsContainer.Children.Clear();
            this.Context.AvalonContainer.Children.Clear();

            // Initializes each Tab object
            for (int i = 0; i < TabCount; i++)
            {
                // Adds a grid column for each tab button
                this.Context.TabsContainer.ColumnDefinitions.Add(new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) });

                var thisTab = new Tab(TabLabels[i], TabIcons[i], i);
                
                this.Tabs.Add(thisTab);

                // Adds the event handler for when the tab is switched
                this.Tabs[i].Button.Click += (sender, e) =>
                {
                    string buttonName = (string)((Label)((Grid)((Button)sender).Content).Children[1]).Content;

                    if (this.Selected.Name == buttonName)
                        return;
                    // Changes the selected tab to whatever the clicked tab button text is
                    this.ChangeSelected(buttonName);
                };

                // forgot what this does tbh, just ctrl click it I guess 
                this.Context.TabsContainer.Children.Add(thisTab.Button);
                this.Context.AvalonContainer.Children.Add(thisTab.TextEditor);
            }

            // Sets the current tab to 0
            this.ChangeSelectedIndex(0);
        }

        // (Use your context clues)
        public void ChangeSelected(string name)
        {
            foreach (Tab tab in Tabs)
            {
                tab.DeActivate();

                if (tab.Name == name)
                    this.Selected = tab.Activate();
                
            }
        }

        // Does the same thing as ChangeSelected(), but uses the tab's index instead
        public void ChangeSelectedIndex(int index)
        {
            foreach (Tab tab in Tabs)
            {
                tab.DeActivate();

                if (tab.Index == index)
                    this.Selected = tab.Activate();
            }
        }
    }
}
