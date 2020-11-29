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
                Title = "Ski Trails in the Canadian Rockies",
                Borrowed = DateTime.Parse("2020-09-01"),
                Due =  DateTime.Parse("2020-09-15")
                },
                new BookItem { ISBN = "978771601185",
                Author = "Chic Scott",
                Title = "Ski Trails in the Canadian Rockies",
                Borrowed = DateTime.Parse("2020-09-01"),
                Due =  DateTime.Parse("2020-09-15")
                }
            };

            foreach (BookItem item in bookItems)
            {
                context.BookItems.Add(item);
            }
            context.SaveChanges();
        }
    }
}