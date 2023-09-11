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

    public static class AvalonHelpers
    {
        public static IHighlightingDefinition LoadEditorTheme(string themeName)
        {
            var type = typeof(AvalonHelpers);
            var fullName = type.Namespace + ".EditorThemes." + themeName + ".xshd";
            using (var stream = type.Assembly.GetManifestResourceStream(fullName))
            using (var reader = new XmlTextReader(stream))
                return HighlightingLoader.Load(reader, HighlightingManager.Instance);
        }

        // This was used for code folding (I think)
        public static XmlReader LoadSchemaSet(string schemaName)
        {
            var type = typeof(AvalonHelpers);
            var fullname = type.Namespace + ".EditorSchemas." + schemaName + ".xml";
            using (var stream = type.Assembly.GetManifestResourceStream(fullname))
            {
                return XmlReader.Create(stream);
            }
        }
    }
}
