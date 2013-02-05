using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Domain;
using System.Data.Entity;

namespace LibrarySystem.Queries
{
    public static class LoanQueries
    {
        public static IQueryable<Loan> FilterbyLoanDates(this IQueryable<Loan> loan, DateTime startDate, DateTime endDate)
        {
            return loan.Where(l => l.LoanDate >= startDate && l.LoanDate <= endDate);
        }

        public static IQueryable<Loan> FilterBySiteName(this IQueryable<Loan> loan, string siteName)
        {
            return loan.Where(l => l.Copy.Title.Shelf.SiteName == siteName);
        }

        public static IQueryable<Loan> filterByMemberName(this IQueryable<Loan> loan, string memberName)
        {
            return loan.Where(l => l.Member.Name == memberName);
        }

        public static IQueryable<Loan> FilterByAll(this IQueryable<Loan> loan, DateTime startDate, DateTime endDate, string siteName, string memberName)
        {
            return loan.FilterbyLoanDates(startDate, endDate).FilterBySiteName(siteName).filterByMemberName(memberName);
            }

        public static IQueryable<Loan> FetchStrategy(this IQueryable<Loan> loan)
        {
            return  loan.Include("Member").Include("Copy.Title2");
        }

        public static IQueryable<Loan> FilterByBookTitle(this IQueryable<Loan> query, string Title)
        {
            return query.Where(l => l.Copy.Title.TitleName == Title);
        }

        public static IQueryable<Loan> FetchStrategyForMembersWithTitlesOnLoan(this IQueryable<Loan> query)
        {
            return query.Include("Member");
        }

    }
}
