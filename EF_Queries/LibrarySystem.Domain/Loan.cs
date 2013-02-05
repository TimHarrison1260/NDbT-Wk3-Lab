using System;

namespace LibrarySystem.Domain
{
    public class Loan
    {
        public virtual int LoanId { get; set; }
        public virtual DateTime LoanDate { get; set; }
        public virtual Copy Copy { get; set; }
        public virtual Member Member { get; set; }

        /// <summary>
        /// return a loan item
        /// sets copy availability to true
        /// </summary>
        public void Return()
        {
            this.Copy.IsAvailable = true;
        }
    }
}
