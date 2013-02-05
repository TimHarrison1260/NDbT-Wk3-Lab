using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibrarySystem.Domain;

namespace LibrarySystem.Helpers
{
    public static class FormatMember
    {
        public static string ForDisplay(Member member, FormatAssociationsEnum includeAssociations)
        {
            StringBuilder bldr = new StringBuilder("Maember:\r\n");
            bldr.AppendFormat("Id:\t\t{0}\r\n", member.MemberId.ToString());
            bldr.AppendFormat("Name:\t\t{0}\r\n", member.Name);
            if (member.Loans == null)
                bldr.AppendLine("Loans:\t\tNot Materialised");
            else
            {
                if (includeAssociations != FormatAssociationsEnum.Parents)
                {
                    foreach (Loan l in member.Loans)
                    {
                        bldr.AppendLine(FormatLoan.ForDisplay(l, includeAssociations));
                    }
                }
                else
                {
                    bldr.AppendLine("Loans:\t\tMaterialised but not displayed");
                }
            }
            return bldr.ToString();
        }
    }
}
