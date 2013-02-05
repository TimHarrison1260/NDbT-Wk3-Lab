using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Domain;

namespace LibrarySystem.Helpers
{
    public static class FormatCopy
    {
        public static string ForDisplay(Copy copy, FormatAssociationsEnum includeAssociations)
        {
            StringBuilder bldr = new StringBuilder("Copy:\r\n");
            bldr.AppendFormat("Id:\t\t{0}\r\n", copy.CopyId.ToString());
            bldr.AppendFormat("IsAvailable:\t{0}\r\n", copy.IsAvailable.ToString());

            if (copy.Title == null)
                bldr.AppendLine("Title:\t\tNot Materialised");
            else
            {
                if (includeAssociations != FormatAssociationsEnum.Children)
                {
                    //  Format Title
                    bldr.AppendLine(FormatTitle.ForDisplay(copy.Title, includeAssociations));
                }
                else
                {
                    bldr.AppendLine("Title:\t\tMaterialised but not displayed");
                }
            }

            return bldr.ToString();
        }
    }
}
