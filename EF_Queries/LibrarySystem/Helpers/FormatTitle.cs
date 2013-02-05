using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Domain;

namespace LibrarySystem.Helpers
{
    public static class FormatTitle
    {
        public static string ForDisplay(Title title, FormatAssociationsEnum includeAssociations)
        {
            StringBuilder bldr = new StringBuilder("Title:\r\n");
            bldr.AppendFormat("Id:\t\t{0}\r\n", title.TitleId.ToString());
            bldr.AppendFormat("Name:\t\t{0}\r\n", title.TitleName);
            bldr.AppendFormat("Author:\t\t{0}\r\n", title.Author);
            bldr.AppendFormat("ISBN:\t\t{0}\r\n", title.ISBN);

            if (title.Shelf == null)
                bldr.AppendLine("Shelf:\t\tNot Materialised");
            else
            {
                if (includeAssociations != FormatAssociationsEnum.Children)
                {
                    //  Format Shelf
                    bldr.AppendLine(FormatShelf.ForDisplay(title.Shelf, includeAssociations));
                }
                else
                {
                    bldr.AppendLine("Shelf:\t\tMaterialised but not displayed");
                }
            }

            if (title.Copies == null)
            {
                bldr.AppendLine("Copies:\t\tNot Materialised");
            }
            else
            {
                if (includeAssociations != FormatAssociationsEnum.Parents)
                {
                    foreach (Copy c in title.Copies)
                    {
                        //  Format Copy
                        bldr.AppendLine(FormatCopy.ForDisplay(c, includeAssociations));
                    }
                }
                else
                {
                    bldr.AppendLine("Copes:\t\tMaterialised but not displayed");
                }
            }
            
            return bldr.ToString();
        }
    }
}
