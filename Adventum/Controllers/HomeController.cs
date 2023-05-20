using Adventum.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Adventum.Controllers
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Gallery()
        {
            return View();
        }
        public IActionResult Video()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Contacts()
        {
            return View();
        }
        public IActionResult ATVpage()
        {
            return View();
        }
        public IActionResult BalloonPage()
        {
            return View();
        }
        public IActionResult BungeePage()
        {
            return View();
        }
        public IActionResult ClimbPage()
        {
            return View();
        }
        public IActionResult ParaPage()
        {
            return View();
        }
        public IActionResult RaftingPage()
        {
            return View();
        }
        public IActionResult SkydivePage()
        {
            return View();
        }
        public IActionResult SnowPage()
        {
            return View();
        }
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}