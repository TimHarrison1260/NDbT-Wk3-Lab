using System.Collections.Generic;

namespace LibrarySystem.Domain
{
    public class Member
    {
        public virtual int MemberId { get; set; }
        public virtual string Name { get; set; }
        public virtual List<Loan> Loans { get; set; }
    }
}
