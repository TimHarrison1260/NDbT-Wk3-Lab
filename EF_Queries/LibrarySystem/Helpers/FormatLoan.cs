using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Domain;


namespace LibrarySystem.Helpers
{
    public static class FormatLoan
    {
        public static string ForDisplay(Loan loan, FormatAssociationsEnum includeAssociations)
        {
            StringBuilder bldr = new StringBuilder("Loan:\r\n");
            bldr.AppendFormat("Id:\t\t{0}\r\n", loan.LoanId.ToString());
            bldr.AppendFormat("Date: \t\t{0}\r\n", loan.LoanDate.ToShortDateString());
            //  Associations: Member is Parent
            if (loan.Member == null)
            {
                bldr.AppendLine("Member:\t\tNot Materialised");
            }
            else
            {
                if (includeAssociations != FormatAssociationsEnum.Children)
                {
                    bldr.AppendLine(FormatMember.ForDisplay(loan.Member, includeAssociations));
                }
                else
                {
                    bldr.AppendLine("Member:\t\tMaterialised but not displayed");
                }
            }
            //  Copy is Parent
            if (loan.Copy == null)
            {
                bldr.AppendLine("Copy:\t\tNot Materialised");
            }
            else
            {
                if (includeAssociations != FormatAssociationsEnum.Children)
                {
                    //  Format Copy
                    bldr.AppendLine("Copy not formatted yet!");
                    //bldr.AppendLine(FormatMember.ForDisplay(loan.Member, includeAssociations);
                }
                else
                {
                    bldr.AppendLine("Copy:\t\tMaterialised but not displayed");
                }
            }

            return bldr.ToString();
        }
    }
}
