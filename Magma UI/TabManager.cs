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

namespace Magma
{
    class Tab
    {
        public Button Button { get; private set; }
        public TextEditor TextEditor { get; private set; }

        public int Index { get; private set; }
        public string Name { get; private set; }

        public Tab(string name, PackIconKind icon, int index)
        {

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
                Foreground = (SolidColorBrush)Application.Current.Resources["TextLightBrush"],
                Visibility = Visibility.Collapsed,
            };

            textEditor.TextArea.TextView.LinkTextForegroundBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF80CBC4"));
            textEditor.SyntaxHighlighting = AvalonHelpers.LoadEditorTheme("LuaPalenight");

            textEditor.TextChanged += AvalonEvents.TextBox_TextChanged;
            textEditor.KeyUp += AvalonEvents.TextBox_KeyUp;
            textEditor.KeyDown += AvalonEvents.TextBox_KeyDown;

            textEditor.TextArea.TextEntering += AvalonEvents.TextArea_TextEntering;
            textEditor.TextArea.TextEntered += AvalonEvents.TextArea_TextEntered;

            Grid.SetColumn(tabButton, index);
            Grid.SetRow(tabButton, 0);
            Grid.SetRow(textEditor, 1);

            this.Index = index;
            this.Name = name;
            this.Button = tabButton;
            this.TextEditor = textEditor;
        }

        public Tab Activate()
        {
            ((PackIcon)((Grid)this.Button.Content).Children[0]).Foreground = (SolidColorBrush)Application.Current.Resources["TextBrush"];
            ((Label)((Grid)this.Button.Content).Children[1]).Foreground = (SolidColorBrush)Application.Current.Resources["TextBrush"];
            this.Button.Background = (SolidColorBrush)Application.Current.Resources["BackgroundSuperContrastBrush"];

            this.TextEditor.Visibility = Visibility.Visible;
            this.TextEditor.Focus();

            return this;
        }

        public Tab DeActivate()
        {
            ((PackIcon)((Grid)this.Button.Content).Children[0]).Foreground = (SolidColorBrush)Application.Current.Resources["TextSuperLightBrush"];
            ((Label)((Grid)this.Button.Content).Children[1]).Foreground = (SolidColorBrush)Application.Current.Resources["TextSuperLightBrush"];
            this.Button.Background = new SolidColorBrush(Colors.Transparent);

            this.TextEditor.Visibility = Visibility.Collapsed;

            return this;
        }
    }

    class TabManager
    {

        private const int TabCount = 6;
        private readonly List<PackIconKind> TabIcons = new List<PackIconKind>
        {
            PackIconKind.Cheese,
            PackIconKind.Cookie,
            PackIconKind.FruitWatermelon,
            PackIconKind.FriedPotatoes,
            PackIconKind.Rice,
            PackIconKind.IcePop
        };
        private readonly List<string> TabLabels = new List<string>
        {
            "Tab A",
            "Tab B",
            "Tab C",
            "Tab D",
            "Tab E",
            "Tab F",
        };

        public List<Tab> Tabs = new List<Tab>();
        private Tab Selected; 
        public Executor Context { get; private set; }

        public TabManager(Page context)
        {
            if (context != Globals.ExecutorPage) // Verify that the context is for the correct window (or else our code would McShit™️ itself)
            {
                throw new Exception("Invalid Context.");
            }
            this.Context = Globals.ExecutorPage;

            this.Context.TabsContainer.ColumnDefinitions.Clear();
            this.Context.TabsContainer.Children.Clear();
            this.Context.AvalonContainer.Children.Clear();

            for (int i = 0; i < TabCount; i++)
            {
                this.Context.TabsContainer.ColumnDefinitions.Add(new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) });

                var thisTab = new Tab(TabLabels[i], TabIcons[i], i);
                
                this.Tabs.Add(thisTab);

                this.Tabs[i].Button.Click += (sender, e) =>
                {
                    string buttonName = (string)((Label)((Grid)((Button)sender).Content).Children[1]).Content;

                    if (this.Selected.Name == buttonName)
                        return;
                    // Changes the selected tab to whatever the clicked tab button text is
                    this.ChangeSelected(buttonName);
                };

                this.Context.TabsContainer.Children.Add(thisTab.Button);
                this.Context.AvalonContainer.Children.Add(thisTab.TextEditor);
            }

            this.ChangeSelectedIndex(0);
        }

        public void ChangeSelectedIndex(int index)
        {
            foreach (Tab tab in Tabs)
            {
                tab.DeActivate();

                if (tab.Index == index)
                    this.Selected = tab.Activate();
            }
        }

        public void ChangeSelected(string name)
        {
            foreach (Tab tab in Tabs)
            {
                tab.DeActivate();

                if (tab.Name == name)
                    this.Selected = tab.Activate();
                
            }
        }
    }
}
