using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Highlighting;
using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using ICSharpCode.AvalonEdit;
using System.Diagnostics;

namespace Magma
{

    public static class Avalon
    {
        public static IHighlightingDefinition LoadEditorTheme(string themeName)
        {
            var type = typeof(Avalon);
            var fullName = type.Namespace + ".EditorThemes." + themeName + ".xshd";
            using (var stream = type.Assembly.GetManifestResourceStream(fullName))
            using (var reader = new XmlTextReader(stream))
                return HighlightingLoader.Load(reader, HighlightingManager.Instance);
        }

        public static XmlReader LoadSchemaSet(string schemaName)
        {
            var type = typeof(Avalon);
            var fullname = type.Namespace + ".EditorSchemas." + schemaName + ".xml";
            using (var stream = type.Assembly.GetManifestResourceStream(fullname))
            {
                return XmlReader.Create(stream);
            }
        }
    }

    public class AutoCompleteObject : ICompletionData
    {
        public AutoCompleteObject(string text, string description, string type = "string", string usage = "")
        {
            BitmapImage BMImage = new BitmapImage(new Uri($"pack://application:,,,/Images/{type.Replace("special", "object")}.png"));
            
            this.Text = text + usage;
            this.Image = BMImage;
            this.Type = type;
            this.Usage = usage;
            
            if (this.Type != "keyword")
            {
                this.Description = $"[{this.Type}]\n{description}";
            }
            else
            {
                this.Description = $"[{this.Type}]";
            }
        }

        public System.Windows.Media.ImageSource Image
        {
            get; private set;
        }

        public double Priority => throw new NotImplementedException();

        public string Text { get; private set; }

        public string Usage { get; private set; }

        private string Type { get; set; }

        // Use this property if you want to show a fancy UIElement in the list.
        public object Content
        {
            get {

                return this.Text;
            }
        }

        public object Description
        {
            get; private set;
        }

        public void Complete(TextArea textArea, ISegment completionSegment,
            EventArgs insertionRequestEventArgs)
        {
            int initial = Extra.CloneInt(textArea.Caret.Offset);

            textArea.Document.Text = textArea.Document.Text.Remove(textArea.Caret.Offset - (Globals.SearchString.Length), Globals.SearchString.Length);
            textArea.Caret.Offset = textArea.Caret.Offset - (Globals.SearchString.Length - (initial - textArea.Caret.Offset));

            if (this.Type == "function")
            {
                textArea.Document.Insert(textArea.Caret.Offset, this.Text.Split('(')[0].Trim(' ') + "()");
                textArea.Caret.Location = new TextLocation(textArea.Caret.Location.Line, textArea.Caret.Column - 1);
            }
            else if (this.Type == "string")
            {
                textArea.Document.Insert(textArea.Caret.Offset, this.Text.Split('(')[0].Trim(' ') + " ");
            } 
            else
            {
                textArea.Document.Insert(textArea.Caret.Offset, this.Text.Split('(')[0].Trim(' '));
            }
        }
    }
}
