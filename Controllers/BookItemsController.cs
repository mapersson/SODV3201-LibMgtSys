using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SODV3201_LibMgtSys.Models;
using SODV3201_LibMgtSys.Data;
namespace SODV3201_LibMgtSys.Controllers
{

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
            try
            {
                if (ModelState.IsValid)
                {
                    if (bookItem != null)
                    {
                        bookItem.Borrowed = null;
                        bookItem.Due = null;
                        _context.BookItems.Add(bookItem);
                        await _context.SaveChangesAsync();
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", ex.Message);
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
    }

}