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

        public IActionResult GetPeoplePartial() => PartialView("_PeopleList", _db);

        public IActionResult SetFilter(string filter)
        {
            _db.Filter = filter;
            Console.WriteLine("I Am Here!");
            return GetPeoplePartial();
        }

        public IActionResult GetPage(int page)
        {
            _db.Page = page;
            return GetPeoplePartial();
        }

        public IActionResult GetCreatePartial()
        {
            return PartialView("_CreatePartial");
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