using Microsoft.EntityFrameworkCore;
using SODV3201_LibMgtSys.Models;

namespace SODV3201_LibMgtSys.Data
{
    public class LibraryContext : DbContext
    {
        public DbSet<BookItem> BookItems { get; set; }
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

    }

}
