using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using quoting_dojo.Connectors;

namespace quoting_dojo.Controllers
{
    public class QuoteController : Controller
    {

        [HttpGet]
        [Route("")]
        public IActionResult index()
        {
            ViewBag.Error = TempData["error"];
            return View();
        }

        [HttpPost]
        [Route("addQuote")]
        public IActionResult addQuote(string name, string quote)
        {
            if (name.Length < 3 || quote.Length < 10)
            {
                TempData["error"] = "Name must contain at least 3 characters and quote must contain 10 characters";
                return RedirectToAction("index");
            
            }
            Console.WriteLine(name);
            Console.WriteLine(quote);
            DbConnector.Execute($"INSERT INTO Users (name, quote, created_at, updated_at) VALUES ('{name}', '{quote}', NOW(), NOW())");

            return RedirectToAction("showQuote");
        }

        [HttpGet]
        [Route("showQuote")]
        public IActionResult showQuote()
        {
            List<Dictionary<string, object>> AllUsers = DbConnector.Query("SELECT * FROM users ORDER BY created_at DESC");
            ViewBag.allQuotes = AllUsers;
            return View("showQuote");
        }

        [HttpGet]
        [Route("deleteQuote/{id}")]
        public IActionResult deleteQuote(int id)
        {
            DbConnector.Execute($"DELETE FROM users WHERE id={id}");

            Console.WriteLine("$$$$$$$$$$$$$$$$$$&&&&&&&&&&&^^^^^^^^^^^^^^^^^^^^^^^^^^^^^@@@@@@@@@@@@@%%%%%%%%%");
            return RedirectToAction("showQuote");
        }
    }
}
