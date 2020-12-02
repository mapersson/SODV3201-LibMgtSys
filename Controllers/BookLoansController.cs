using System.Collections.Generic;
using System.Linq;
using SODV3201_LibMgtSys.Data;
using SODV3201_LibMgtSys.Models;

namespace SODV3201_LibMgtSys.Controllers
{

    public class BookLoansController
    {

        private LibraryContext _context;

        public BookLoansController(LibraryContext context)
        {
            _context = context;
        }

        public IEnumerable<BookItem> LoanedBooks()
        {
            var loanedBooks = _context.BookLoans.Where(l => l.ReturnedDate == null).Select(b => b.BookItem).ToList();
            return (loanedBooks);
        }

    }
}