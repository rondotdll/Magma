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

namespace Magma
{
    /// <summary>
    /// Interaction logic for Executor.xaml
    /// </summary>
    public partial class Executor : Page
    {
        public Executor()
        {
            InitializeComponent();
            
            //TypingTimer = new DispatcherTimer();
            //TypingTimer.Stop();
            //TypingTimer.Interval = new TimeSpan(0, 0, 0, 3);
            //TypingTimer.Tick += new EventHandler(UpdateFoldsOnTick);
        }
        static BraceFoldingStrategy foldingStrategy = new BraceFoldingStrategy();
        public static int LineCount;

        private void Page_Initialized(object sender, EventArgs e)
        {
            // While support for code folding does exist it's kinda clunky and bad, so I've disabled it for now.

            //foldingManager = FoldingManager.Install(ScriptTextBox.TextArea);
            //foldingStrategy.UpdateFoldings(foldingManager, ScriptTextBox.Document);

            ScriptTextBox.TextArea.TextView.LinkTextForegroundBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF80CBC4"));
            ScriptTextBox.SyntaxHighlighting = Avalon.LoadEditorTheme("LuaPalenight");

            LineCount = ScriptTextBox.LineCount;

            ScriptTextBox.TextArea.TextEntering += textEditor_TextArea_TextEntering;
            ScriptTextBox.TextArea.TextEntered += textEditor_TextArea_TextEntered;
        }

        private void ScriptTextBox_TextChanged(object sender, EventArgs e)
        {
            if (LineCount < ScriptTextBox.LineCount)
            {
                int CursorOffset = ScriptTextBox.CaretOffset;
                var ScriptDoc = ScriptTextBox.Document;
                DocumentLine CurrentLine = ScriptDoc.GetLineByOffset(CursorOffset);
                DocumentLine PreviousLine = ScriptDoc.GetLineByOffset(CursorOffset).PreviousLine;

                if (
                    ScriptDoc.GetText(PreviousLine.Offset, PreviousLine.Length).Replace("\r", "").Replace("\n", "").EndsWith("then") || 
                    ScriptDoc.GetText(PreviousLine.Offset, PreviousLine.Length).Replace("\r", "").Replace("\n", "").EndsWith("do") ||
                    ScriptDoc.GetText(PreviousLine.Offset, PreviousLine.Length).Replace("\r", "").Replace("\n", "").EndsWith("[[") ||
                    ScriptDoc.GetText(PreviousLine.Offset, PreviousLine.Length).Replace("\r", "").Replace("\n", "").EndsWith("{") ||
                    ScriptDoc.GetText(PreviousLine.Offset, PreviousLine.Length).Replace("\r", "").Replace("\n", "").EndsWith("("))
                {
                    ScriptTextBox.Document.Text = ScriptTextBox.Document.Text.Insert(CursorOffset, "\t");
                }
                else if (
                    ScriptDoc.GetText(PreviousLine.Offset, PreviousLine.Length).Replace("\r", "").Replace("\n", "").EndsWith("end") ||
                    ScriptDoc.GetText(PreviousLine.Offset, PreviousLine.Length).Replace("\r", "").Replace("\n", "").EndsWith("]]") ||
                    ScriptDoc.GetText(PreviousLine.Offset, PreviousLine.Length).Replace("\r", "").Replace("\n", "").EndsWith("}") ||
                    ScriptDoc.GetText(PreviousLine.Offset, PreviousLine.Length).Replace("\r", "").Replace("\n", "").EndsWith(")"))
                {
                    ScriptTextBox.Document.Text = ScriptTextBox.Document.Text.Remove(CursorOffset - 1);
                }
            }

            LineCount = ScriptTextBox.LineCount;
        }

        CompletionWindow completionWindow;

        public bool IsEqualToChars(char input, char[] list)
        {
            bool output = false;
            
            foreach(char c in list)
            {
                output = (input == c);
                if (output)
                    break;
            }

            return output;
        }

        void textEditor_TextArea_TextEntered(object sender, TextCompositionEventArgs e)
        {
            int initial = Extra.CloneInt(ScriptTextBox.CaretOffset); 

            if (IsEqualToChars(e.Text[0], (Globals.AllowedChars + Globals.AllowedChars.ToUpper()).ToCharArray()) || e.Text == ".")
            {
                // Open code completion after the user has pressed dot:
                completionWindow = new CompletionWindow(ScriptTextBox.TextArea);
                completionWindow.FontSize = 12;
                completionWindow.FontFamily = new FontFamily("Consolas");

                completionWindow.BorderThickness = new Thickness(0, 0, 0, 0);
                completionWindow.WindowStyle = WindowStyle.None;
                completionWindow.Background = new SolidColorBrush(Color.FromArgb(255, 33, 34, 39));
                completionWindow.Foreground = ScriptTextBox.Foreground;
                completionWindow.ResizeMode = ResizeMode.NoResize;
                completionWindow.Width = Math.Round(.3 * Application.Current.MainWindow.Width);

                completionWindow.KeyDown += completionWindow_KeyDown;

                IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;


                Globals.SearchString = "";
                for (int n = 1; n <= ScriptTextBox.CaretOffset && IsEqualToChars(Char.ToLower(ScriptTextBox.Document.GetCharAt(ScriptTextBox.CaretOffset - n)), Globals.AllowedChars.ToCharArray()); n++) {
                    Globals.SearchString = ScriptTextBox.Document.Text[ScriptTextBox.CaretOffset - n] + Globals.SearchString;
                }

                int i = 0;
                foreach (CompletionItem item in Globals.MasterCompletionList.AsParallel().Where(obj => obj.Name.StartsWith(Globals.SearchString)).OrderBy(obj => Globals.TypePriority.IndexOf(obj.Type)).ThenBy(obj => obj.Name).ToList())
                {
                    if (i >= 5)
                        break;
             
                    data.Add(new AutoCompleteObject(item.Name, item.Description, item.Type, item.Usage));
                    i++;
                }
                //data.Add(new AutoCompleteObject("print", "function"));
                //data.Add(new AutoCompleteObject("Item"));
                //data.Add(new AutoCompleteObject("Item"));

                if (data.Count == 0)
                    return;
                    //data.Add(new AutoCompleteObject("EMPTY", "EMPTY"));
                
                completionWindow.Show();
                completionWindow.CompletionList.SelectedItem = data[0];
                completionWindow.Closed += delegate {
                    completionWindow = null;
                };
            } else if (e.Text == "\"")
            {
                bool eval = false;

                try
                {
                    eval = ScriptTextBox.Document.GetCharAt(ScriptTextBox.CaretOffset) == '"';
                }
                catch
                {                
                }

                if (eval)
                {
                    ScriptTextBox.Text = ScriptTextBox.Text.Remove(ScriptTextBox.CaretOffset, 1);
                    ScriptTextBox.CaretOffset = initial;
                }
                else
                {
                    ScriptTextBox.Text = ScriptTextBox.Text.Insert(ScriptTextBox.CaretOffset, "\"");
                    ScriptTextBox.CaretOffset = initial;
                }
            }
        }

        void textEditor_TextArea_TextEntering(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Length > 0 && completionWindow != null)
            {
                if (!char.IsLetterOrDigit(e.Text[0]))
                {
                    // Whenever a non-letter is typed while the completion window is open,
                    // insert the currently selected element.
                    completionWindow.CompletionList.RequestInsertion(e);
                }
            }
            // Do not set e.Handled=true.
            // We still want to insert the character that was typed.
        }

        void completionWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {

            } else if (e.Key == Key.Down)
            {

            } else if (e.Key == Key.Tab || e.Key == Key.Return || e.Key == Key.Enter)
            {
                e.Handled = true;
            }
            else
            {
                completionWindow.Close();
            }
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
