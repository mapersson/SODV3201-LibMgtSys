using System;
using Microsoft.AspNetCore.Mvc;
using SODV3201_LibMgtSys.Models;

namespace SODV3201_LibMgtSys.Controllers
{

    public class BookItemsController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
    }

}