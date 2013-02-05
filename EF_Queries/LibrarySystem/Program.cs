using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using LibrarySystem.Domain;
using LibrarySystem.Repositories;
using LibrarySystem.Helpers;

namespace LibrarySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // set database initializer
            Database.SetInitializer<LibraryContext>(new LibraryContextInitializer());

            LibraryContext db = new LibraryContext();
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;

            Console.WriteLine("Database contains:");

            var titles = db.Titles.ToList();
            Console.WriteLine(string.Format("{0} Titles", titles.Count()));

            var copies = db.Copies.ToList();
            Console.WriteLine(string.Format("{0} Copies", copies.Count()));

            var members = db.Members.ToList();
            Console.WriteLine(string.Format("{0} Members", members.Count()));

            var loans = db.Loans.ToList();
            Console.WriteLine(string.Format("{0} Loans", loans.Count()));

            var categories = db.Categories.ToList();
            Console.WriteLine(string.Format("{0} Categories", categories.Count()));


            FormatClass<Title>.ForDisplay(titles[0]);




            Console.WriteLine("Press <ENTER> to execute Use Case 1");
            Console.ReadLine();
            Repository rep = new Repository();
            Member member1 = rep.GetMemberDetailsAndLoans(2);
            Console.WriteLine(FormatMember.ForDisplay(member1, FormatAssociationsEnum.Children));


            Console.WriteLine("Press <ENTER> to execute Use Case 2");
            Console.ReadLine();
            rep = new Repository();
            List<Title> titles2 = rep.GetTitlesforCategoryDescription("Thriller");
            foreach (Title t in titles2)
            {
                Console.WriteLine(FormatTitle.ForDisplay(t, FormatAssociationsEnum.Children));
            }


            Console.WriteLine("Press <ENTER> to execute Use Case 3");
            Console.ReadLine();
            rep = new Repository();
            List<Title> titles3 = rep.GetTitlesWithCopiesForCategoryDescription("Thriller", "Glasgow Caledonian University");
            foreach (Title t in titles3)
            {
                Console.WriteLine(FormatTitle.ForDisplay(t, FormatAssociationsEnum.Children));
            }


            Console.WriteLine("Press <ENTER> to execute Use Case 4");
            Console.ReadLine();
            rep = new Repository();
            List<Loan> loans4 = rep.GetMembersWhoHaveTitleOnLoan("Programming Entity Framework");
            foreach (Loan l in loans4)
            {
                Console.WriteLine(FormatLoan.ForDisplay(l, FormatAssociationsEnum.Parents));
            }

            Console.WriteLine("Press <ENTER> to execute Use Case 4A");
            Console.ReadLine();
            rep = new Repository();
            List<Loan> loans4A = rep.GetMembersWhoHaveTitleOnLoan("Programming Entity Framework");
            foreach (Loan l in loans4A)
            {
                Console.WriteLine(FormatLoan.ForDisplay(l, FormatAssociationsEnum.Parents));
            }


            Console.WriteLine("Press <ENTER> to execute Use Case 5");
            Console.ReadLine();
            rep = new Repository();
            Dictionary<Title, Member> TitlesLoaned5 = rep.GetTitlesLoanedByPeriod(new DateTime(2013, 2, 15), new DateTime(2013, 2, 22), "Glasgow Caledonian University", "Jenson");
            foreach (Title t in TitlesLoaned5.Keys)
            {
                Console.WriteLine(FormatTitle.ForDisplay(t, FormatAssociationsEnum.Children));
                Console.WriteLine(FormatMember.ForDisplay(TitlesLoaned5[t], FormatAssociationsEnum.Children));
            }


            Console.WriteLine("Press <ENTER> to execute Use Case 6");
            Console.ReadLine();
            rep = new Repository();
            Dictionary<string, int> TitlesLoaned6 = rep.GetLoansPerTitle();
            foreach (string t in TitlesLoaned6.Keys)
            {
                Console.WriteLine("{0} has {1} copies on loan", t, TitlesLoaned6[t]);
            }

            Console.WriteLine("Press <ENTER> to execute Use Case 7");
            Console.ReadLine();
            rep = new Repository();
            List<Loan> entireGraph = rep.GetEntireObjectGraph();
            foreach (Loan l in entireGraph)
            {
                Console.WriteLine(FormatTitle.ForDisplay(l.Copy.Title, FormatAssociationsEnum.Children));
                Console.WriteLine(FormatMember.ForDisplay(l.Member, FormatAssociationsEnum.Children));
            }


            Console.WriteLine("Press <ENTER> to execute Use Case 8");
            Console.ReadLine();
            rep = new Repository();
            Dictionary<Title, Member> TitlesLoaned8 = rep.GetTitlesLoanedByPeriodWithFilters(new DateTime(2013, 2, 15), new DateTime(2013, 2, 22), "Glasgow Caledonian University", "Jenson");
            foreach (Title t in TitlesLoaned8.Keys)
            {
                Console.WriteLine(FormatTitle.ForDisplay(t, FormatAssociationsEnum.Children));
                Console.WriteLine(FormatMember.ForDisplay(TitlesLoaned8[t], FormatAssociationsEnum.Children));
            }


            


            Console.WriteLine("\r\nPress <ENTER> to terminate");
            Console.ReadLine();
        }
    }
}
