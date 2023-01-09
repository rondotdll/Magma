// Copyright (c) 2009 Daniel Grunwald
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;

namespace Magma
{
    /// <summary>
    /// Allows producing foldings from a document based on braces.
    /// </summary>
    public class BraceFoldingStrategy
    {
        /// <summary>
        /// Gets/Sets the opening brace. The default value is '{'.
        /// </summary>
        public char[] OpeningBrace { get; set; }
        public string[] OpeningString { get; set; }

        /// <summary>
        /// Gets/Sets the closing brace. The default value is '}'.
        /// </summary>
        public char[] ClosingBrace { get; set; }
        public string[] ClosingString { get; set; }

        /// <summary>
        /// Creates a new BraceFoldingStrategy.
        /// </summary>
        public BraceFoldingStrategy()
        {
            this.OpeningBrace = new char[2] { '(', '{' };
            this.ClosingBrace = new char[2] { ')', '}' };

            this.OpeningString = new string[] { "if", "elseif", "else", "do", "function" };
            this.ClosingString = new string[] { "end", "end", "end", "end", "end" };
        }

        public void UpdateFoldings(FoldingManager manager, TextDocument document)
        {
            int firstErrorOffset;
            IEnumerable<NewFolding> newFoldings = CreateNewFoldings(document, out firstErrorOffset);
            manager.UpdateFoldings(newFoldings, firstErrorOffset);
        }

        /// <summary>
        /// Create <see cref="NewFolding"/>s for the specified document.
        /// </summary>
        public IEnumerable<NewFolding> CreateNewFoldings(TextDocument document, out int firstErrorOffset)
        {
            firstErrorOffset = -1;
            return CreateNewFoldings(document);
        }

        /// <summary>
        /// Create <see cref="NewFolding"/>s for the specified document.
        /// </summary>
        public IEnumerable<NewFolding> CreateNewFoldings(ITextSource document)
        {
            List<NewFolding> newFoldings = new List<NewFolding>();

            Stack<int> startOffsets = new Stack<int>();
            int lastNewLineOffset = 0;
            for (int n = 0; n < OpeningString.Length; n++)
            {
                string openingString = OpeningString[n];
                string closingString = ClosingString[n];
                for (int i = 0; i < document.TextLength; i++)
                {
                    char s = document.GetCharAt(i);
                    if ((Extra.CheckStringAt(i, openingString + "\n", document) || Extra.CheckStringAt(i, openingString + " ", document)) && !Extra.IsCommented(i, document))
                    {
                        startOffsets.Push(i);
                    }
                    else if ((Extra.CheckStringAt(i, closingString, document) || Extra.CheckStringAt(i, closingString, document)) && !Extra.IsCommented(i, document) && startOffsets.Count > 0)
                    {
                        int startOffset = startOffsets.Pop();
                        // don't fold if opening and closing brace are on the same line
                        if (startOffset < lastNewLineOffset)
                        {
                            int val = openingString.Length;

                            if (val >= 7)
                                val = 6;

                            newFoldings.Add(new NewFolding(startOffset + val, i + closingString.Length));
                        }
                    }
                    else if (s == '\n' || s == '\r')
                    {
                        lastNewLineOffset = i + 1;
                    }
                }
            }
            
            for (int n = 0; n < OpeningBrace.Length; n++)
            {
                char openingBrace = this.OpeningBrace[n];
                char closingBrace = this.ClosingBrace[n];
                for (int i = 0; i < document.TextLength; i++)
                {
                    char c = document.GetCharAt(i);
                    if (c == openingBrace)
                    {
                        startOffsets.Push(i);
                    }
                    else if (c == closingBrace && startOffsets.Count > 0)
                    {
                        int startOffset = startOffsets.Pop();
                        // don't fold if opening and closing brace are on the same line
                        if (startOffset < lastNewLineOffset)
                        {
                            newFoldings.Add(new NewFolding(startOffset, i + 1));
                        }
                    }
                    else if (c == '\n' || c == '\r')
                    {
                        lastNewLineOffset = i + 1;
                    }
                }
            }

            newFoldings.Sort((a, b) => a.StartOffset.CompareTo(b.StartOffset));
            //Debugger.Break();
            return newFoldings;
        }
    }
}