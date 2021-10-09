using System.IO;
using System.Windows.Controls;

namespace Magma2._1
{
    class LocalFunctions
    {
        /* Create ShortHand To Fill ListBox With Items In Folder */

        public static void PopulateListBox(ListBox lsb, string Folder, string FileType)
        {
            DirectoryInfo dinfo = new DirectoryInfo(Folder);
            FileInfo[] Files = dinfo.GetFiles(FileType);

            int i = 0;

            foreach (FileInfo file in Files)
            {
                string fName = file.Name;

                ListBoxItem tmpItem = new ListBoxItem();

                tmpItem.Content = fName;

                tmpItem.Padding = new System.Windows.Thickness(10, 12, 10, 12);

                tmpItem.Margin = new System.Windows.Thickness(0, 0, 0, 0);

                tmpItem.ToolTip = new ToolTip { Content = fName };

                lsb.Items.Add(tmpItem);

                i++;
            }
        }
    }
}
