using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using SODV3201_LibMgtSys.Models;
using SODV3201_LibMgtSys.Data;
namespace SODV3201_LibMgtSys.Controllers
{
    [Authorize]
    public class BookItemsController : Controller
    {
        private LibraryContext _context;

        public BookItemsController(LibraryContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.BookItems.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookItem bookItem)
        {
            if (ModelState.IsValid)
            {
                //    TODO: This check may not be required because bookItem is not nullable. 
                if (bookItem != null)
                {
                    try
                    {
                        _context.BookItems.Add(bookItem);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateException ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                    }
                }
            }
            return View(bookItem);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(await _context.BookItems.FirstOrDefaultAsync(m => m.ID == id));
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookItemToUpdate = await _context.BookItems.FirstOrDefaultAsync(b => b.ID == id);
            if (await TryUpdateModelAsync<BookItem>(bookItemToUpdate,
            "",
            b => b.Title, b => b.Author))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {

                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(bookItemToUpdate);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookItem = await _context.BookItems.FirstOrDefaultAsync(m => m.ID == id);

            return View(bookItem);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id.HasValue)
            {
                var bookLoansCount = _context.BookLoans.Where(l => l.BookItemID == id).Where(r => r.ReturnedDate == null).ToList().Count();
                if (bookLoansCount == 0)
                {
                    var book = _context.BookItems.Single(b => b.ID == id);

                    try
                    {
                        _context.BookItems.Remove(book);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateException ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                    }
                }
                else
                {
                    // TODO: Create a view that informs the user that the book can't be deleted. 
                    return RedirectToAction(nameof(Details), new { id = id });
                }
            }

            return NotFound();
        }
    }

}