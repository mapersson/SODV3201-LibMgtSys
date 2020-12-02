using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SODV3201_LibMgtSys.Models;

namespace SODV3201_LibMgtSys.Data
{
    public static class DbInitializer
    {
        public static void Initialize(LibraryContext context)
        {
            context.Database.EnsureCreated();

            if (context.BookItems.Any())
            {
                return;
            }

            var bookItems = new BookItem[]
            {
                new BookItem { ISBN = "978771601184",
                Author = "Chic Scott",
                Title = "Ski Trails in the Canadian Rockies"

                },
                new BookItem { ISBN = "978771601185",
                Author = "Chic Scott Jr",
                Title = "Ski Trails in the Canadian Rockies"

                }
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
                    ItemsCheckedOut = 0
                },
                new LibAccount {
                    Owner = people.Single(p => p.LastName == "Chamberlain"),
                    ItemsCheckedOut = 0
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
                }
            };

            foreach (BookLoan item in bookLoans)
            {
                context.BookLoans.Add(item);
            }
            context.SaveChanges();
        }
    }
}