using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SODV3201_LibMgtSys.Data;
using SODV3201_LibMgtSys.Models;
using SODV3201_LibMgtSys.Models.ViewModels;

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

        [HttpGet]
        public IActionResult Create()
        {

            var accountInfo = new LibAccountData();
            return View(accountInfo);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LibAccountData data)
        {
            // TODO: Need to check that Owner information is unique. 
            if (ModelState.IsValid)
            {
                var newOwner = new Person
                {
                    FirstName = data.Owner.FirstName,
                    LastName = data.Owner.LastName
                };

                var newAccount = new LibAccount
                {
                    // TODO: Change the library account init to accept an owner and set the items checked out to 0
                    Owner = newOwner,
                    ItemsCheckedOut = 0
                };

                try
                {
                    _context.LibAccounts.Add(newAccount);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            // TODO: Make this redirect more informative. 
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Details(Guid? id)
        {
            // TODO: Determine if async is actually required here. 
            if (id != null)
            {
                var libAccountID = id.GetValueOrDefault();
                var account = await _context.LibAccounts.Include(o => o.Owner).SingleAsync(l => l.ID == libAccountID);
                var owner = _context.People.Single(o => o.ID == account.Owner.ID);
                var bookLoans = _context.BookLoans.Include(c => c.BookItem).Where(u => u.LibAccountID == libAccountID).Where(l => l.ReturnedDate == null).ToList();

                var libAccountData = new LibAccountData
                {
                    Owner = owner,
                    BookLoans = bookLoans
                };
                return View(libAccountData);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> CreateBookLoan(Guid? id)
        {
            // TODO: This action needs to be revised to support validation. 
            var testBook = await _context.BookItems.SingleAsync(b => b.ISBN == "978771601184");
            var testID = testBook.ID;
            id = testID;

            if (id != null)
            {
                var libAccounts = await _context.LibAccounts.Include(l => l.Owner).ToListAsync();
                var newBookItem = await _context.BookItems.SingleOrDefaultAsync(b => b.ID == id);

                var newLoan = new BookLoan
                {
                    BookItemID = newBookItem.ID,
                };

                var bookLoanData = new BookLoanData
                {
                    loan = newLoan,
                    libAccounts = libAccounts,
                    bookItem = newBookItem
                };

                return View(bookLoanData);
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBookLoan(BookLoanData data)
        {
            if (data != null)
            {
                var bookLoan = data.loan;
                var libAccount = await _context.LibAccounts.SingleAsync(l => l.ID == bookLoan.LibAccountID);
                // TODO: Change this code to properly increment the number of books checked out. 
                libAccount.ItemsCheckedOut = libAccount.ItemsCheckedOut + 1;
                try
                {
                    _context.BookLoans.Add(bookLoan);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }

            }
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> CloseBookLoan(Guid? id)
        {
            if (id != null)
            {
                var bookLoan = await _context.BookLoans.SingleAsync(b => b.ID == id);
                bookLoan.ReturnedDate = DateTime.Now;

                var libAccounts = await _context.LibAccounts.Include(l => l.Owner).ToListAsync();
                var newBookItem = await _context.BookItems.SingleAsync(b => b.ID == bookLoan.BookItemID);

                var bookLoanData = new BookLoanData
                {
                    loan = bookLoan,
                    bookItem = newBookItem,
                };

                return View(bookLoanData);

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CloseBookLoan(BookLoanData data)
        {

            if (data != null)
            {
                var bookLoan = await _context.BookLoans.SingleAsync(l => l.ID == data.loan.ID);
                var libAccount = await _context.LibAccounts.SingleAsync(l => l.ID == bookLoan.LibAccountID);
                bookLoan.ReturnedDate = data.loan.ReturnedDate;
                // TODO: Change this code to properly decrement the number of books checked out. 
                libAccount.ItemsCheckedOut = libAccount.ItemsCheckedOut - 1;
                var returnID = data.loan.LibAccountID;

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { id = returnID });
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }

            }
            return RedirectToAction(nameof(CloseBookLoan), data.loan.ID);
        }
    }
}