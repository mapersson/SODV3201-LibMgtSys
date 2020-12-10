using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SODV3201_LibMgtSys.Models;

namespace SODV3201_LibMgtSys.Data
{
    public class LibraryContext : IdentityDbContext<AppUser>
    {
        public DbSet<BookItem> BookItems { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<LibAccount> LibAccounts { get; set; }
        public DbSet<BookLoan> BookLoans { get; set; }
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

    }

}
