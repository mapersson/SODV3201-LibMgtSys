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

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
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