using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Adventum.Data;
using Microsoft.AspNetCore.Identity;
using Adventum.Models;

namespace Adventum.Controllers
{
    public class EventReservationsController : Controller
    {
        private readonly AdventureContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EventReservationsController(AdventureContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: EventReservations
        public async Task<IActionResult> Index()
        {
            string currentUser = _userManager.GetUserId(User);

            IQueryable<EventReservation> query = User.IsInRole("Admin")
                ? _context.EventReservations.AsQueryable()
                : _context.EventReservations.Where(x => x.UserId == currentUser);

            List<EventReservationModel> result = await query
                .Include(er => er.Event)
                .Include(er => er.User)
                .Select(q => new EventReservationModel
                {
                    EventReservationId = q.Id,
                    UserFullName = $"{q.User.FirstName} {q.User.LastName}",
                    EventName = q.Event.Name,
                    Location = q.Event.Location
                })
                .ToListAsync();

            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var eventReservation = await _context.EventReservations
                .Include(e => e.Event)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            return eventReservation == null ? NotFound() : View(eventReservation);
        }
        
        // // GET: EventReservations/Create
        // [Authorize(Roles = "User,Admin")]
        // public IActionResult Create()
        // {
        //     //ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id");
        //     //return View();
        //
        //     EventReservationsVM model = new EventReservationsVM();
        //     //EventReservation ev = new EventReservation();
        //     model.Events = _context.Events.Select(x => new SelectListItem
        //         {
        //             Text = x.Name,
        //             Value = x.Id.ToString(),
        //             Selected = (x.Id == model.EventId)
        //         }
        //     ).ToList();
        //     return View(model);
        // }
        //
        // // POST: EventReservations/Create
        // // To protect from overposting attacks, enable the specific properties you want to bind to.
        // // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create([Bind("Id,EventId,UserId")] EventReservation eventReservation)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         //_context.Add(eventReservation);
        //
        //         EventReservationsVM model = new EventReservationsVM();
        //         //EventReservation ev = new EventReservation();
        //         model.Events = _context.Events.Select(x => new SelectListItem
        //             {
        //                 Text = x.Name,
        //                 Value = x.Id.ToString(),
        //                 Selected = (x.Id == model.EventId)
        //             }
        //         ).ToList();
        //         return View(model);
        //     }
        //
        //     EventReservation modeltoDB = new EventReservation
        //     {
        //         EventId = eventReservation.EventId
        //         /*UserId=_userManager.GetUserId(User)*/,
        //     };
        //     _context.Add(modeltoDB);
        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        // }
        
        
        // // GET: EventReservations/Create
        // [Authorize(Roles = "User,Admin")]
        // public IActionResult Create()
        // {
        //     //ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id");
        //     //return View();
        //
        //     EventReservationsVM model = new EventReservationsVM();
        //     //EventReservation ev = new EventReservation();
        //     model.Events = _context.Events.Select(x => new SelectListItem
        //     {
        //         Text = x.Name,
        //         Value = x.Id.ToString(),
        //         Selected = (x.Id == model.EventId)
        //     }
        //     ).ToList();
        //     return View(model);
        // }

        // // POST: EventReservations/Create
        // // To protect from overposting attacks, enable the specific properties you want to bind to.
        // // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create([Bind("Id,EventId,UserId")] EventReservation eventReservation)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         //_context.Add(eventReservation);
        //
        //         EventReservationsVM model = new EventReservationsVM();
        //         //EventReservation ev = new EventReservation();
        //         model.Events = _context.Events.Select(x => new SelectListItem
        //         {
        //             Text = x.Name,
        //             Value = x.Id.ToString(),
        //             Selected = (x.Id == model.EventId)
        //         }
        //         ).ToList();
        //         return View(model);
        //     }
        //     EventReservation modeltoDB = new EventReservation
        //     {
        //         EventId = eventReservation.EventId
        //         /*UserId=_userManager.GetUserId(User)*/,
        //     };
        //     _context.Add(modeltoDB);
        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        //
        //     //ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventReservation.EventId);
        //     //return View(eventReservation);
        // }

        // GET: EventReservations/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.EventReservations == null)
            {
                return NotFound();
            }

            var eventReservation = await _context.EventReservations
                .Include(e => e.Event)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventReservation == null)
            {
                return NotFound();
            }

            return View(eventReservation);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventReservation = await _context.EventReservations
                .Include(u => u.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (eventReservation == null)
                return RedirectToAction(nameof(Index));
            
            _context.EventReservations.Remove(eventReservation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}