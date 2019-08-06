using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Assignment1.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CV()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult FeverCheck()
        {
            ViewData["HasTemp"] = false;
            return View();
        }

        [HttpPost]
        public IActionResult FeverCheck(Models.Temperature temp)
        {
            ViewData["Temperature"] = temp.Value;
            ViewData["HasTemp"] = true;
            return View();
        }
    }
}