using ICSharpCode.AvalonEdit.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;
using System.Linq.Expressions;

/* This is where we dumped all the misc functions that are used throughout
 */

namespace Magma
{
    public class Extra
    {
        public static bool IsEqualToChars(char input, char[] list)
        {
            bool output = false;

            foreach (char c in list)
            {
                output = (input == c);
                if (output)
                    break;
            }

            return output;
        }

        public static bool EndsWithAny(string input, List<string> suffixes)
        {
            foreach (string suffix in suffixes)
            {
                if (input.EndsWith(suffix))
                    return true;
            }

            return false;
        }
        public static int CloneInt(int value)
        {
            return value;
        }

        /// <summary>
        /// Checks if string exists at the specified position in document.
        /// </summary>
        /// <param name="position">Position of string in the document</param>
        /// <param name="search">String to match</param>
        /// <param name="document">Document to search</param>
        /// <returns></returns>

        public static bool CheckStringAt(int position, string search, ITextSource document)
        {
            bool output = true;

            if (search.Length == 0)
                return false;

            for (int i = 0; i < search.Length; i++)
            {
                try
                {
                    if (document.GetCharAt(position + i) == search[i])
                        continue;
                    else
                        output = false;
                }
                catch
                {
                    output = false;
                }
            }

            return output;
        }

        /// <summary>
        /// Determines if the line of the specified character position is commented out
        /// </summary>
        /// <param name="position"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public static bool IsCommented(int position, ITextSource document)
        {
            bool flag = false;
            int i = 0;

            try
            {
                while (true)
                {

                    if (document.GetCharAt(position - i) == '\n')
                    {
                        return false;
                    }
                    else if (document.GetCharAt(position - i) == '-' && !flag)
                    {
                        flag = true;
                    }
                    else if (document.GetCharAt(position - i) == '-' && flag)
                    {
                        return true;
                    }
                    i--;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
