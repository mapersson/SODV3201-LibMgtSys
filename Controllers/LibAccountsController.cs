using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SODV3201_LibMgtSys.Data;
using SODV3201_LibMgtSys.Models;

namespace SODV3201_LibMgtSys.Controllers
{
    public class LibAccountsController : Controller
    {
        private LibraryContext _context;
        public LibAccountsController(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viewData = await _context.LibAccounts.Include(l => l.Owner).ToListAsync();
            return View(viewData);
        }

        public async Task<IActionResult> CreateBookLoan()
        {

            var myAccount = await _context.LibAccounts.SingleAsync(l => l.Owner.LastName == "Persson");
            var myBook = await _context.BookItems.SingleAsync(b => b.ISBN == "978771601184");
            var myBookLoan = new BookLoan
            {
                LibAccountID = myAccount.ID,
                BookItemID = myBook.ID,
                BorrowedDate = DateTime.Now,
                DueDate = DateTime.Parse("2020-09-01")
            };

            try
            {
                _context.BookLoans.Add(myBookLoan);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View();
        }
    }
}