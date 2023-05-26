using System.ComponentModel.DataAnnotations;
using Adventum.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Adventum.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;

namespace Adventum.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        
        private readonly AdventureContext _context;

        public HomeController(UserManager<User> userManager, AdventureContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
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
        public async Task<IActionResult> Booking()
        {
            var events = await _context.Events
                .Include(s => s.SportActivity)
                .Include(l => l.Location)
                .ToListAsync();
            return View(events);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }


    public class UserReservationModel
    {
        public string UserId { get; set; }
        
        [Display(Name = "Име")]
        [Editable(false)]
        public string FirstName { get; set; }
        
        [Display(Name = "Фамилия")]
        [Editable(false)]
        public string LastName { get; set; }
        
        [Display(Name = "Имейл")]
        [Editable(false)]
        public string Email { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Избери дата")]
        public DateTime Date { get; set; }

        [Range(1, Int32.MaxValue)]
        [Display(Name = "Брой участници")]
        public int ParticipantCount { get; set; }
        
        public int SportActivityId { get; set; }
    }
}