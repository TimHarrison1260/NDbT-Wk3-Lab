using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibrarySystem.Domain;
using LibrarySystem.Queries;

namespace LibrarySystem.Repositories
{
    public class Repository
    {
        /// <summary>
        ///  *************************************************************************************************
        ///  USE CASE 1:    Display details of all Loans for a Member specified by MemberId, 
        ///                 and the details of that Member
        ///
        ///  Object type required:   Member
        ///  Repository to be used:  Members
        ///  Query logic:            MemberId == value
        ///  Fetch strategy:         Include Loans 
        ///  Objects materialised:   Title, Loans 
        ///  *************************************************************************************************
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public Member GetMemberDetailsAndLoans(int memberId)
        {
            //  Use the 'using' construct to ensure the context is disposed.  This is required
            //  because other contexts may use the same entities, which result in SQL DataReaders
            //  being used, unless they are closed once finished, exceptions could be thrown when
            //  referencing another context which would use the same reader.  This ensures the 
            //  data reader is closed.
            using (LibraryContext db = new LibraryContext())
            {
                var member = db.Members
                    .Include("Loans")
                    .Where(m => m.MemberId == memberId)
                    .FirstOrDefault();

                return member;
            }
        }


        /// <summary>
        ///  *************************************************************************************************
        ///  USE CASE 2:    Display details of all Titles in a Category specified by Description
        ///
        ///  Object type required:   Title
        ///  Repository to be used:  Titles, Categories
        ///  Query logic:            Category.Description == value
        ///  Fetch strategy:         Include Category 
        ///  Objects materialised:   Title, Category
        ///
        ///  Note:       No navigation possible from Category to Title or vice versa, therefore 
        ///              a Join is used to provide the association on the CategoryId foreign key.
        ///
        ///  *************************************************************************************************
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public List<Title> GetTitlesforCategoryDescription(string description)
        {
            using (LibraryContext db = new LibraryContext())
            {
                IQueryable<Title> result = db.Titles
                    .Include("Shelf")
                    .Join(db.Categories, t => t.CategoryId, c => c.CategoryId, (t, c) => new { t, c })
                    .Where(c1 => c1.c.Description == description)
                    .Select(t1 => t1.t);

                //  Must force the IQueryable<> to be traversed BEFORE the context is closed.
                return result.ToList<Title>();
            }

        }



        /// <summary>
        ///  *************************************************************************************************
        ///  USE CASE 3:    Display details of all Titles which have copies available in a Category specified
        ///                 by Description at any Shelf with a specified SiteName, and display the details of the copies
        ///                 of those Titles
        ///
        ///  Object type required:   Title
        ///  Repository to be used:  Titles, Copies
        ///  Query logic:            Category.Description == value
        ///                          Shelf.SiteName = value
        ///                          At least 1 Copy is available.
        ///  Fetch strategy:         Include Copies
        ///  Objects materialised:   Title, Copies
        ///
        ///  Note:       No navigation possible from Category to Title or vice versa, therefore 
        ///              a Join is used to provide the association on the CategoryId foreign key.
        ///
        ///  *************************************************************************************************
        /// </summary>
        /// <param name="description"></param>
        /// <param name="siteName"></param>
        /// <returns></returns>
        public List<Title> GetTitlesWithCopiesForCategoryDescription(string description, string siteName)
        {
            using (LibraryContext db = new LibraryContext())
            {
                int id = db.Categories.Where(c => c.Description == description).Select(c => c.CategoryId).Single();

                var titles = db.Titles
                    .Include("Copies")
                    .Where(t => t.Shelf.SiteName == siteName
                        && (t.Copies.Where(c => c.IsAvailable == true && c.Title.CategoryId == id).Count() > 0)
                        && t.CategoryId == id);

                return titles.ToList<Title>();
            }
        }


        /// <summary>
        ///  *************************************************************************************************
        ///  USE CASE 4:    Display details of all Members who have Loans for a specified Title, and display
        ///                 the dates of those Loans
        ///
        ///  Object type required:   Loan
        ///  Repository to be used:  Loans
        ///  Query logic:            Title.TitlaName == value
        ///  Fetch strategy:         Include Member
        ///  Objects materialised:   Loans, Member
        ///  *************************************************************************************************
        /// </summary>
        /// <param name="bookTitle"></param>
        /// <returns></returns>
        public List<Loan> GetMembersWhoHaveTitleOnLoan(string bookTitle)
        {
            using (LibraryContext db = new LibraryContext())
            {
                var loans = db.Loans
                    .Include("Member")
                    .Where(l => l.Copy.Title.TitleName == bookTitle);
                return loans.ToList<Loan>();
            }
        }

        public List<Loan> GetMembersWhoHaveTitleOnLoanWithFiltersAndFetchStrategy(string bookTitle)
        {
            using (LibraryContext db = new LibraryContext())
            {
                var loans = db.Loans
                    .FetchStrategyForMembersWithTitlesOnLoan()
                    //.Include("Member")
                    .FilterByBookTitle(bookTitle);
                    //.Where(l => l.Copy.Title.TitleName == bookTitle);
                return loans.ToList<Loan>();
            }
        }


        /// <summary>
        ///  *************************************************************************************************
        ///  USE CASE 5:    Display details of Titles of which Copies are loaned between 12/1/13 and
        ///                 22/1/13 of Titles located at a Shelf with a specified SiteName to a specified Member, and
        ///                 display details of the Member
        ///
        ///  Object type required:   Title
        ///  Repository to be used:  Loans
        ///  Query logic:            Loan.LoanDate within period
        ///  Fetch strategy:         Include Member
        ///  Objects materialised:   Title, Member
        ///  *************************************************************************************************
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="siteName"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public Dictionary<Title, Member> GetTitlesLoanedByPeriod(DateTime fromDate, DateTime toDate, string siteName, string memberName)
        {
            using (LibraryContext db = new LibraryContext())
            {
                Dictionary<Title, Member> loanedTitles = new Dictionary<Title, Member>();
                var results = db.Loans
                    .Include("Member")
                    .Include("Copy.Title")
                    .Where(l => l.LoanDate >= fromDate && l.LoanDate <= toDate)
                    .Where(l => l.Copy.Title.Shelf.SiteName == siteName)
                    .Where(l => l.Member.Name == memberName)
                    .Select(l => new { Title = l.Copy.Title, Member = l.Member });

                foreach (var a in results)
                {
                    loanedTitles.Add(a.Title, a.Member);
                }
                return loanedTitles;
            }
        }


        /// <summary>
        ///  *************************************************************************************************
        ///  USE CASE 6:    Display the TitleName of and number of Loans for each Title - it is not
        ///                 necessary here to retrieve domain objects, just the data values specified
        ///
        ///  Object type required:   Title and count of Loans
        ///  Repository to be used:  Loans
        ///  Query logic:            Group the Loans by Title
        ///  Fetch strategy:         Project the title and count, no other objects need to be specifically fetched
        ///  Objects materialised:   Loan
        ///  *************************************************************************************************
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetLoansPerTitle()
        {
            using (LibraryContext db = new LibraryContext())
            {
                Dictionary<string, int> LoansPerTitle = new Dictionary<string, int>();
                var results = db.Loans
                    .GroupBy(l => l.Copy.Title.TitleName)
                    .Select(g => new { Title = g.Key, Loans = g.Count() });
                foreach (var a in results)
                {
                    LoansPerTitle.Add(a.Title, a.Loans);
                }
                return LoansPerTitle;
            }
        }



        /// <summary>
        ///  *************************************************************************************************
        ///  USE CASE 7:    Display details of all Loans for a Member specified by MemberId, 
        ///                 and the details of that Member
        ///
        ///  Object type required:   Entire Object Graph
        ///  Repository to be used:  Loans
        ///  Query logic:            select everything 
        ///  Fetch strategy:         Include Member, Copy, Title, Shelf
        ///  Objects materialised:   Loans , Member, Copy, Title, Shelf
        ///  *************************************************************************************************
        /// </summary>
        /// <returns></returns>
        public List<Loan> GetEntireObjectGraph()
        {
            using (LibraryContext db = new LibraryContext())
            {
                var results = db.Loans
                    .Include("Member")
                    .Include("Copy")
                    .Include("Copy.Title")
                    .Include("Copy.Title.Shelf");
                return results.ToList<Loan>();
            }
        }


        /// <summary>
        ///  *************************************************************************************************
        ///  USE CASE 8:    Display details of Titles of which Copies are loaned between 12/1/13 and
        ///                 22/1/13 of Titles located at a Shelf with a specified SiteName to a specified Member, and
        ///                 display details of the Member
        ///
        ///  Object type required:   Title
        ///  Repository to be used:  Loans
        ///  Query logic:            use Filter extension methods on IQueryable(Loan)
        ///  Fetch strategy:         Include Member
        ///  Objects materialised:   Title, Member
        ///  *************************************************************************************************
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="siteName"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public Dictionary<Title, Member> GetTitlesLoanedByPeriodWithFilters(DateTime fromDate, DateTime toDate, string siteName, string memberName)
        {
            using (LibraryContext db = new LibraryContext())
            {
                Dictionary<Title, Member> loanedTitles = new Dictionary<Title, Member>();

                var results = db.Loans
                    .FetchStrategy()
                    //.Include("Member")
                    //.Include("Copy.Title")
                    .FilterByAll(fromDate, toDate, siteName, memberName)
                    //.FilterbyLoanDates(fromDate, toDate)
                    //.FilterBySiteName(siteName)
                    //.filterByMemberName(memberName)
                    .Select(l => new { Title = l.Copy.Title, Member = l.Member });

                foreach (var a in results)
                {
                    loanedTitles.Add(a.Title, a.Member);
                }
                return loanedTitles;
            }
        }

    }
}
