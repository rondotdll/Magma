using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Highlighting;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Linq;
using System.Text;
using System.IO;
using ICSharpCode.AvalonEdit.Folding;
using System.Timers;
using System.Windows.Threading;
using ICSharpCode.AvalonEdit.Rendering;
using ICSharpCode.AvalonEdit.Document;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Security.Cryptography.X509Certificates;
using Magma.Avalon;

namespace Magma
{
    /// <summary>
    /// Interaction logic for Executor.xaml
    /// </summary>
    public partial class Executor : Page
    {
        static BraceFoldingStrategy foldingStrategy = new BraceFoldingStrategy();
        static TabManager tabManager;
        
        public Executor()
        {
            InitializeComponent();
            
            //TypingTimer = new DispatcherTimer();
            //TypingTimer.Stop();
            //TypingTimer.Interval = new TimeSpan(0, 0, 0, 3);
            //TypingTimer.Tick += new EventHandler(UpdateFoldsOnTick);
        }


        private void ExecutorPage_Initialized(object sender, EventArgs e)
        {
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            tabManager = new TabManager(this);
        }

        // While support for code folding does exist it's kinda clunky and bad, so I've disabled it for now.

        /*
        static FoldingManager foldingManager;
        private static DispatcherTimer TypingTimer;
        
        private void ScriptTextBox_TextChanged(object sender, EventArgs e)
        {
            TypingTimer.Start();
        }

        private void ScriptTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            TypingTimer.Stop();

        }

        private void UpdateFoldsOnTick(object sender, EventArgs e)
        {
            foldingStrategy.UpdateFoldings(foldingManager, ScriptTextBox.Document);
            TypingTimer.Stop();
        }

        */
    }

    
}
