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

    public class MyCompletionData : ICompletionData
    {
        public MyCompletionData(string text, string description, string imgsource = "Images.GenericIcon")
        {
            this.Text = text;
            this.Description = description;
        }

        public System.Windows.Media.ImageSource Image
        {
            get { return null; }
        }

        public string Text { get; private set; }
        public object Description { get; private set; }

        // Use this property if you want to show a fancy UIElement in the list.
        public object Content
        {
            get { return this.Text; }
        }


        public double Priority => throw new NotImplementedException();

        public void Complete(TextArea textArea, ISegment completionSegment,
            EventArgs insertionRequestEventArgs)
        {
            textArea.Document.Replace(completionSegment, this.Text);
        }
    }
}
