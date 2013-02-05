using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using LibrarySystem.Domain;


namespace LibrarySystem
{
    //public class LibraryContextInitializer : DropCreateDatabaseAlways<LibraryContext>
    public class LibraryContextInitializer : CreateDatabaseIfNotExists<LibraryContext>
    {
        protected override void Seed(LibraryContext context)
        {
            base.Seed(context);

            Shelf sh1 = new Shelf{
                ShelfCode = "005.7",
                SiteName = "Glasgow Caledonian University",
                Address = "Cowcaddens Road",
                PostCode = "G4 0BA" 
            };

            Shelf sh2 = new Shelf
            {
                ShelfCode = "005.8",
                SiteName = "Some Other University",
                Address = "Cowcaddens Road",
                PostCode = "G4 0BA"
            };

            Title bk1 = new Title{
                TitleName = "Programming Entity Framework",
                Author = "Julie Lerman",
                ISBN = "0596807260",
                Shelf = sh1,
                CategoryId = 2,
                Copies = new List<Copy>{
                    new Copy{
                        IsAvailable = true
                    },
                    new Copy{
                        IsAvailable = true
                    },
                    new Copy{
                        IsAvailable = true
                    },
                    new Copy{
                        IsAvailable = true
                    }
                }
            };

            Title bk2 = new Title
            {
                TitleName = "Programming Microsoft LINQ in Microsoft .NET Framework 4",
                Author = "Paolo Pialorsi",
                ISBN = "0735640572",
                Shelf = sh2,
                CategoryId = 3,
                Copies = new List<Copy>{
                    new Copy{
                        IsAvailable = false
                    },
                    new Copy{
                        IsAvailable = true
                    },
                    new Copy{
                        IsAvailable = true
                    }
                }
            };

            Title bk3 = new Title
            {
                TitleName = "Entity Framework 4.1: Expert's Cookbook",
                Author = "Devlin Iles",
                ISBN = "1849684464",
                Shelf = sh1,
                CategoryId = 2,
                Copies = new List<Copy>{
                    new Copy{
                        IsAvailable = false
                    },
                    new Copy{
                        IsAvailable = false
                    }
                }
            };

            context.Titles.Add(bk1);
            context.Titles.Add(bk2);
            context.Titles.Add(bk3);

            Copy cp1 = bk1.GetCopy();
           
            Member mb1 = new Member
            {
                Name = "Fernando",
                Loans = new List<Loan>()
                    {
                        new Loan
                        {
                            Copy = bk1.GetCopy(),
                            LoanDate = new DateTime(2013,2,1)
                        },
                        new Loan
                        {
                            Copy = bk2.GetCopy(),
                            LoanDate = new DateTime(2013,2,20)
                        }
                    }
            };

            Member mb2 = new Member
            {
                Name = "Felipe",
                Loans = new List<Loan>()
                {
                    new Loan
                    {
                        Copy = bk1.GetCopy(),
                        LoanDate = new DateTime(2013,2,5)
                    },
                    new Loan
                    {
                        Copy = bk2.GetCopy(),
                        LoanDate = new DateTime(2013,2,25)
                    }
                }
            };

            Member mb3 = new Member
            {
                Name = "Jenson",
                Loans = new List<Loan>()
                {
                    new Loan
                    {
                        Copy = bk1.GetCopy(),
                        LoanDate = new DateTime(2013,2,15)
                    }
                }
            };

            Member mb4 = new Member
            {
                Name = "Checo",
                Loans = new List<Loan>()
            };

            context.Members.Add(mb1);
            context.Members.Add(mb2);
            context.Members.Add(mb3);
            context.Members.Add(mb4);

            Category cat1 = new Category
            {
                Description = "Non-Fiction"
            };

            Category cat2 = new Category
            {
                Description = "Thriller"
            };

            Category cat3 = new Category
            {
                Description = "Humour"
            };

            Category cat4 = new Category
            {
                Description = "Sport"
            };

            context.Categories.Add(cat1);
            context.Categories.Add(cat2);
            context.Categories.Add(cat3);
            context.Categories.Add(cat4);

            context.SaveChanges();
        }
    }
}
