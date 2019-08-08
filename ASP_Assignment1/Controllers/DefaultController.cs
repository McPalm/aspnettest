using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

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

        [HttpGet]
        public IActionResult GuessingGame()
        {
            var number = new Random().Next(100) + 1;
            HttpContext.Session.SetInt32("Number", number);
            ViewData["Secret"] = number;
            HttpContext.Session.SetInt32("Guesses", 0);
            ViewData["HighScores"] = GetHighScores();
            return View();
        }

        [HttpPost]
        public IActionResult GuessingGame(int guess)
        {
            var maybe = HttpContext.Session.GetInt32("Number");
            var number = maybe.HasValue ?
                maybe.Value :
                new Random().Next(100) + 1;
            HttpContext.Session.SetInt32("Number", number);

            var guesses = HttpContext.Session.GetInt32("Guesses").GetValueOrDefault();
            guesses++;
            HttpContext.Session.SetInt32("Guesses", guesses);

            ViewData["Guesses"] = guesses;
            ViewData["Guess"] = guess;
            ViewData["Correct"] = guess == number;
            ViewData["GuessTooLow"] = guess < number;
            if (guess == number)
                ViewData["HighScores"] = AddHighScore(guesses);
            return View();
        }

        int[] GetHighScores()
        {
            try
            {
                if (Request.Cookies.TryGetValue("HighScores", out var scores))
                {
                    return scores.Split(' ')
                        .Select(s => int.Parse(s))
                        .OrderBy((a) => a)
                        .ToArray();
                }
                else
                    return new int[] { };
            }
            catch
            {
                return new int[] { };
            }
        }

        int[] AddHighScore(int score)
        {
            var old = GetHighScores();
            var neu = new int[old.Length + 1];
            Array.Copy(old, neu, old.Length);
            neu[old.Length] = score;
            neu = neu.OrderBy(i => i).ToArray();
            Response.Cookies.Append("HighScores", String.Join(' ', neu));
            return neu;
        }
    }
}