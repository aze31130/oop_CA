using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using oop_CA.Models;
using System;
using System.Diagnostics;

namespace oop_CA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (getUserId() > 0)
            {
                ViewData["Logged"] = "OK";
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //-----
        //Returns the id of the currently logged account
        //Returns -1 if no one is logged
        //-----
        private int getUserId()
        {
            try
            {
                return int.Parse(User.Identity.Name);
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
