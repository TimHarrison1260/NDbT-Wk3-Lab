using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Domain;

namespace LibrarySystem.Helpers
{
    public static class FormatShelf
    {
        public static string ForDisplay(Shelf shelf, FormatAssociationsEnum includeAssociations)
        {
            StringBuilder bldr = new StringBuilder("Shelf:\r\n");
            bldr.AppendFormat("Code:\t\t{0}\r\n", shelf.ShelfCode);
            bldr.AppendFormat("Name:\t\t{0}\r\n", shelf.SiteName);
            bldr.AppendFormat("Address:\t\t{0}\r\n", shelf.Address );
            bldr.AppendFormat("PostCode:\t\t{0}\r\n", shelf.PostCode );

            return bldr.ToString();
        }
    }
}
 