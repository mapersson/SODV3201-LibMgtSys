using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SODV3201_LibMgtSys.Models;

namespace SODV3201_LibMgtSys.Data
{
    public static class DbInitializer
    {
        public static void Initialize(LibraryContext context, UserManager<AppUser> usrMgr, RoleManager<IdentityRole> roleMgr)
        {
            context.Database.EnsureCreated();

            if (context.BookItems.Any())
            {
                return;
            }

            InitializeRoles(roleMgr);
            InitializeUsers(usrMgr);

            var bookItems = new BookItem[]
            {
                new BookItem { ISBN = "978771601184",
                Author = "Chic Scott",
                Title = "Ski Trails in the Canadian Rockies"

                },
                new BookItem { ISBN = "978771601185",
                Author = "Chic Scott Jr",
                Title = "Ski Trails in the Canadian Rockies"

                },
                new BookItem { ISBN = "978771601186",
                Author = "Robert Greene",
                Title = "48 Laws of Power"

                },
                new BookItem { ISBN = "978771601187",
                Author = "Be Here Now",
                Title = "Ram Dass"
                },
                new BookItem { ISBN = "978771601188",
                Author = "Jocko Willink",
                Title = "Extreme Ownership"
                },
                new BookItem { ISBN = "978771601189",
                Author = "Thich Nhat Hanh",
                Title = "How to Eat"
                },
                new BookItem { ISBN = "978771601190",
                Author = "Dale Carnegie",
                Title = "How to Win Friends & Influence People"
                },
                new BookItem { ISBN = "978771601191",
                Author = "Christina Wodtke",
                Title = "Radical Focus"
                },
                new BookItem { ISBN = "978771601192",
                Author = "Jason Fried & David Hansson",
                Title = "ReWork"
                },
                new BookItem { ISBN = "978771601193",
                Author = "Yuval Noah Harari",
                Title = "Sapiens"
                },
                new BookItem { ISBN = "978771601194",
                Author = "Gregory David Roberts",
                Title = "Shantaram"
                },
                new BookItem {
                    ISBN = "978771601195",
                    Author = "John Sonmez",
                    Title = "Soft Skills"
                },
                new BookItem { ISBN = "978771601196",
                    Title = "Soft Skills Again",
                    Author = "John Sonmez"
                },
                new BookItem { ISBN = "978771601197",

                Title = "Tao of Wu", Author = "RZA"},

                new BookItem { ISBN ="978771601198",

                Title = "The Alchemist",
                Author = "Paulo Coelho"},

                new BookItem { ISBN = "978771601199",
                Title = "The Art of War",
                Author = "Sun Tzu", },

                new BookItem {ISBN = "978771601200",
                Title = "The Book of Mistakes",
                Author = "Skip Prichard"},
                new BookItem { ISBN=  "978771601201",
                Title = "The Culture Code",
                Author = "Daniel Coyle"},
                new BookItem { ISBN = "978771601202",
                Title = "The Four Agreements",
                Author = "Don Miguel Ruiz"}

            };

            foreach (BookItem item in bookItems)
            {
                context.BookItems.Add(item);
            }
            context.SaveChanges();

            var people = new Person[] {
                new Person {
                    FirstName = "Mike",
                    LastName = "Persson"
                },
                new Person {
                    FirstName = "Heather",
                    LastName = "Chamberlain"
                },
                new Person {
                    FirstName = "Elizabeth",
                    LastName = "Seybold"
                }
            };

            foreach (Person item in people)
            {
                context.People.Add(item);
            }
            context.SaveChanges();

            var libAcccounts = new LibAccount[] {
                new LibAccount {
                    Owner = people.Single(p => p.LastName == "Persson"),
                    ItemsCheckedOut = 1
                },
                new LibAccount {
                    Owner = people.Single(p => p.LastName == "Chamberlain"),
                    ItemsCheckedOut = 1
                },
                new LibAccount {
                    Owner = people.Single(p => p.LastName == "Seybold"),
                    ItemsCheckedOut = 2
                }
            };

            foreach (LibAccount item in libAcccounts)
            {
                context.LibAccounts.Add(item);
            }
            context.SaveChanges();

            var bookLoans = new BookLoan[] {
                new BookLoan
                {
                    LibAccountID = libAcccounts.Single(l => l.Owner.LastName == "Persson").ID,
                    BookItemID = bookItems.Single( b => b.ISBN == "978771601184").ID,
                    BorrowedDate = DateTime.Now,
                    DueDate = DateTime.Parse("2020-09-01")
                },
                new BookLoan
                {
                    LibAccountID = libAcccounts.Single(l => l.Owner.LastName == "Chamberlain").ID,
                    BookItemID = bookItems.Single( b => b.ISBN == "978771601185").ID,
                    BorrowedDate = DateTime.Now,
                    DueDate = DateTime.Parse("2020-09-01")
                },
                new BookLoan
                {
                    LibAccountID = libAcccounts.Single(l => l.Owner.LastName == "Chamberlain").ID,
                    BookItemID = bookItems.Single( b => b.ISBN == "978771601199").ID,
                    BorrowedDate = DateTime.Now,
                    DueDate = DateTime.Parse("2020-09-01"),
                    ReturnedDate = DateTime.Parse("2020-12-4")
                },
                new BookLoan
                {
                    LibAccountID = libAcccounts.Single(l => l.Owner.LastName == "Seybold").ID,
                    BookItemID = bookItems.Single( b => b.ISBN == "978771601201").ID,
                    BorrowedDate = DateTime.Now,
                    DueDate = DateTime.Parse("2020-09-01")
                },
                new BookLoan
                {
                    LibAccountID = libAcccounts.Single(l => l.Owner.LastName == "Seybold").ID,
                    BookItemID = bookItems.Single( b => b.ISBN == "978771601202").ID,
                    BorrowedDate = DateTime.Now,
                    DueDate = DateTime.Parse("2020-09-01")
                },
                new BookLoan
                {
                    LibAccountID = libAcccounts.Single(l => l.Owner.LastName == "Seybold").ID,
                    BookItemID = bookItems.Single( b => b.ISBN == "978771601198").ID,
                    BorrowedDate = DateTime.Now,
                    DueDate = DateTime.Parse("2020-09-01"),
                    ReturnedDate = DateTime.Parse("2020-12-4")
                },
            };

            foreach (BookLoan item in bookLoans)
            {
                context.BookLoans.Add(item);
            }
            context.SaveChanges();
        }

        private static void InitializeUsers(UserManager<AppUser> usrMgr)
        {
            if (usrMgr.Users.Count() != 0)
            {
                return;
            }
            else
            {
                AppUser genUser = new AppUser()
                {
                    UserName = "UserAccount",
                    Email = "UserAccount@me.com",
                };

                IdentityResult result = usrMgr.CreateAsync(genUser, "FrostyCOVID2019!").Result;
                if (result.Succeeded)
                {
                    usrMgr.AddToRoleAsync(genUser, "General").Wait();
                }

                AppUser adminUser = new AppUser()
                {
                    UserName = "AdminAccount",
                    Email = "AdminAccount@me.com",
                };

                result = usrMgr.CreateAsync(adminUser, "FrostyCOVID2019!").Result;
                if (result.Succeeded)
                {
                    usrMgr.AddToRoleAsync(adminUser, "Administrator").Wait();
                }

                AppUser librarianUser = new AppUser()
                {
                    UserName = "LibrarianAccount",
                    Email = "LibrarianAccount@me.com",
                };

                result = usrMgr.CreateAsync(librarianUser, "FrostyCOVID2019!").Result;
                if (result.Succeeded)
                {
                    usrMgr.AddToRoleAsync(librarianUser, "Librarian").Wait();
                }

            }
        }

        private static void InitializeRoles(RoleManager<IdentityRole> roleMgr)
        {
            if (roleMgr.Roles.Count() != 0)
            {
                return;
            }
            else
            {
                IdentityRole genRole = new IdentityRole()
                {
                    Name = "General"
                };

                IdentityResult result = roleMgr.CreateAsync(genRole).Result;

                IdentityRole adminRole = new IdentityRole()
                {
                    Name = "Administrator"
                };

                result = roleMgr.CreateAsync(adminRole).Result;

                IdentityRole librarianRole = new IdentityRole()
                {
                    Name = "Librarian"
                };

                result = roleMgr.CreateAsync(librarianRole).Result;
            }
        }
    }
}