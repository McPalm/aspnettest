using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP_Assignment1.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Assignment1.Controllers
{
    public class PeopleController : Controller
    {
        static List<Person> people = PeopleList.List;

        [HttpGet]
        public IActionResult Index()
        {
            return View(people.Take(10).ToList());
        }

        public IActionResult GetPeoplePartial(int page)
        {
            return PartialView("_PeopleList", people.Skip(page * 10).Take(10));
        }

        public IActionResult GetCreatePartial()
        {
            return PartialView("_CreatePartial");
        }

        [HttpPost]
        public IActionResult Index(string filter)
        {
            IEnumerable<Person> filtered = people;
            foreach (var query in filter.Split(' '))
            {
                if(query.Length > 2 && query[1] == ':')
                {
                    switch(query[0])
                    {
                        case 's':
                        case 'o':
                            if (query.Substring(2).ToLower() == "name")
                                filtered = filtered.OrderBy(p => p.Name);
                            else if (query.Substring(2).ToLower() == "age")
                                filtered = filtered.OrderBy(p => p.Age);
                            else if (query.Substring(2).ToLower() == "rage")
                                filtered = filtered.OrderBy(p => -p.Age);
                            else if (query.Substring(2).ToLower() == "iage")
                                filtered = filtered.OrderBy(p => -p.Age);
                            else if (query.Substring(2).ToLower() == "city")
                                filtered = filtered.OrderBy(p => p.City);
                            break;
                    }
                }
                else
                {
                    filtered = filtered.Where(person =>
                    {
                        return person.Name.Contains(query, StringComparison.CurrentCultureIgnoreCase)
                        || (person.City != null && person.City.Contains(query, StringComparison.CurrentCultureIgnoreCase));
                    });
                }
            }

            return View(filtered.Take(10).ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Delete(string name)
        {
            people.RemoveAll(p => p.Name == name);
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                people.Add(person);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}