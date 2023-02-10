using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System.Web.UI.WebControls;
using ICSharpCode.AvalonEdit;
using System.Windows.Controls;
using System.Diagnostics;
using System.Media;

/* This file contains every AvalonEdit specific event handler, including:
 * - AutoComplete
 * - Zoom Keybinds
 */

namespace Magma.Avalon
{
    internal class AvalonEvents
    {
        private static bool ctrlPressed = false;

        private static bool IsNextCharacter(TextEditor textBox, char query)
        {
            return textBox.Document.GetCharAt(textBox.CaretOffset) == query;
        }

        private static void SkipInput(ref TextEditor textBox, int initialPosition)
        {
            textBox.Text = textBox.Text.Remove(textBox.CaretOffset, 1);
            textBox.CaretOffset = initialPosition;
        }

        public static void TextBox_TextChanged(object sender, EventArgs e)
        {
            //Unused
        }

        private static CompletionWindow completionWindow;

        public static void TextArea_TextEntered(object sender, TextCompositionEventArgs e)
        {
            TextArea textArea = (TextArea)sender;
            
            // TextArea is a child of the TextEditor class, so we need to locate the sender's parent.
            TextEditor textBox = (TextEditor)((ScrollViewer)textArea.Parent).TemplatedParent;

            int initial = Extra.CloneInt(textBox.CaretOffset);

            // If the user created a new line
            if (e.Text.EndsWith("\n"))
            {
                int CursorOffset = textBox.CaretOffset;
                var ScriptDoc = textBox.Document;
                DocumentLine CurrentLine = ScriptDoc.GetLineByOffset(CursorOffset);
                DocumentLine PreviousLine = ScriptDoc.GetLineByOffset(CursorOffset).PreviousLine;

                // If the previous line ended with a nested code identifier
                if (Extra.EndsWithAny(
                        ScriptDoc.GetText(PreviousLine.Offset, PreviousLine.Length).Replace("\r", "").Replace("\n", ""),
                        new List<string>{ "then", "do", "[[", "{", "(" }))
                    // Add an indent character to the new line
                    textBox.Document.Text = textBox.Document.Text.Insert(CursorOffset, "\t");
                return;
            }

            // If the user typed an alphanumeric character
            if (Extra.IsEqualToChars(e.Text[0], Globals.AlphaNumeric.ToCharArray()))
            {
                // Open the code completion window:
                completionWindow = new CompletionWindow(textBox.TextArea)
                {
                    FontSize = 12,
                    FontFamily = (FontFamily)Application.Current.Resources["JBMono"], //new FontFamily("Consolas")

                    BorderThickness = new Thickness(0),
                    WindowStyle = WindowStyle.None,
                    Background = (SolidColorBrush)Application.Current.Resources["BackgroundSuperContrastBrush"], //new SolidColorBrush(Color.FromArgb(255, 33, 34, 39))
                    Foreground = textBox.Foreground,
                    ResizeMode = ResizeMode.NoResize,
                            // Roughly 1/3 of the Main Window's width
                    Width = Math.Round(.3 * Application.Current.MainWindow.Width)
                    
                };

                completionWindow.KeyDown += CompletionWindow_KeyDown;

                IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;


                Globals.SearchString = "";

                int AlreadyScanned = 1;
                int CurrentScanPos = textBox.CaretOffset - AlreadyScanned;

                // Scans backwards from the cursor's current position until it reaches a non-alphanumeric character 
                /* English Translation:
                /*     Grabs the text between the previous non-alphanumeric character and the cursor's current position (yes, ik its messy) */
                while (
                    CurrentScanPos >= 0 &&
                    Extra.IsEqualToChars(char.ToLower(textBox.Document.GetCharAt(CurrentScanPos)), Globals.AlphaNumeric.ToCharArray()))
                {
                    // Set our Search String to said text (previous comment)
                    Globals.SearchString = textBox.Document.Text[CurrentScanPos] + Globals.SearchString;
                    AlreadyScanned++;
                    CurrentScanPos = textBox.CaretOffset - AlreadyScanned;
                }

                // Populates the completion window with any completion objects that match our search string
                foreach (CompletionItem item in Globals.MasterCompletionList.AsParallel().Where(obj => obj.Name.StartsWith(Globals.SearchString)).OrderBy(obj => Globals.TypePriority.IndexOf(obj.Type)).ThenBy(obj => obj.Name).ToList())
                {
                    if (data.Count >= 5)
                        break;

                    data.Add(new AutoCompleteObject(item.Name, item.Description, item.Type, item.Usage));
                }

                if (data.Count == 0)
                    return;

                completionWindow.Show();
                completionWindow.CompletionList.SelectedItem = data[0];

                // When the completion window is closed, destroy it's instance
                completionWindow.Closed += delegate {
                    completionWindow = null;
                };
            }
            else
            {
                try
                {
                    switch (e.Text)
                    {
                        case "\"":
                            if (IsNextCharacter(textBox, '\"'))
                                SkipInput(ref textBox, initial);
                            else
                            {
                                textBox.Text = textBox.Text.Insert(textBox.CaretOffset, "\"");
                                textBox.CaretOffset = initial;
                            }
                            break;

                        case ")":
                            if (IsNextCharacter(textBox, ')'))
                                SkipInput(ref textBox, initial);

                            break;

                        case "(":
                            if (IsNextCharacter(textBox, '('))
                                SkipInput(ref textBox, initial);
                            else
                            {
                                textBox.Text = textBox.Text.Insert(textBox.CaretOffset, ")");
                                textBox.CaretOffset = initial;
                            }
                            break;
                    }
                } catch
                {
                    return;
                    // We will just ignore any exceptions that occur, since they usually have
                    // to do with typing too quickly
                }
            }
        }

        public static void TextArea_TextEntering(object sender, TextCompositionEventArgs e)
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

        public static void CompletionWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Return || e.Key == Key.Enter)
            {
                e.Handled = true;
            }
            else
            {
                completionWindow.Close();
            }
        }

        public static void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextEditor textBox = (TextEditor)sender;

            try
            {
                if (e.Key == Key.LeftCtrl)
                {
                    ctrlPressed = true;
                }
                else if (e.Key == Key.OemPlus && ctrlPressed)
                {
                    textBox.FontSize++;
                    e.Handled = true;
                    return;
                }
                else if (e.Key == Key.OemMinus && ctrlPressed)
                {
                    textBox.FontSize--;
                    e.Handled = true;
                    return;
                }
                else if (e.Key == Key.D0 && ctrlPressed)
                {
                    textBox.FontSize = 12;
                    e.Handled = true;
                    return;
                }
            } catch {
                SystemSounds.Exclamation.Play();
                return; 
            };
        }

        public static void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
            {
                ctrlPressed = false;
            }
        }
    }
}
