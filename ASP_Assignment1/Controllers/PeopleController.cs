using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP_Assignment1.Models;
using ASP_Assignment1.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Assignment1.Controllers
{
    public class PeopleController : Controller
    {
        static PeopleList _db { get; set; } = PeopleList.GetMock();

        [HttpGet]
        public IActionResult Index()
        {
            return View(_db);
        }

        public IActionResult GetPeoplePartial(int page)
        {
            if(page > -1)
                _db.Page = page;
            return PartialView("_PeopleList", _db);
        }

        public IActionResult GetCreatePartial()
        {
            return PartialView("_CreatePartial");
        }

        [HttpPost]
        public IActionResult Index(string filter)
        {
            _db.Filter = filter;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Delete(int key)
        {
            _db.Dictionary.Remove(key);
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                _db.Add(person);
                return RedirectToAction("Index");
            }
            return View(); // were refreshing the page here.
        }
    }
}