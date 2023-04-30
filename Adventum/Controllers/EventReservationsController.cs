using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Adventum.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Adventum.Models;

namespace Adventum.Controllers
{
    public class EventReservationsController : Controller
    {
        private readonly AdventureContext _context;
        private readonly UserManager<User> _userManager;

        public EventReservationsController(AdventureContext context,
            UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: EventReservations
        [Authorize]
        public async Task<IActionResult> Index()
        {
            //var adventureContext = _context.EventReservations.Include(e => e.Event);
            //return View(await adventureContext.ToListAsync());


            if (User.IsInRole("Admin"))
            {
                var adventureContext = _context.EventReservations
                .Include(er => er.Event)
                .Include(er => er.User);
                return View(await adventureContext.ToListAsync());
            }
            else
            {
                var currentUser = _userManager.GetUserId(User);
                var myReservations = _context.EventReservations
                               .Include(er => er.Event)
                               .Include(u => u.User)
                               .Where(x => x.UserId == int.Parse(currentUser))
                               .ToListAsync();

                return View(await myReservations);
            }


        }

        // GET: EventReservations/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: EventReservations/Create
        [Authorize(Roles = "User,Admin")]
        public IActionResult Create()
        {
            //ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id");
            //return View();

            EventReservationsVM model = new EventReservationsVM();
            //EventReservation ev = new EventReservation();
            model.Events = _context.Events.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = (x.Id == model.EventId)
            }
            ).ToList();
            return View(model);
        }

        // POST: EventReservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EventId,UserId")] EventReservation eventReservation)
        {
            if (!ModelState.IsValid)
            {
                //_context.Add(eventReservation);

                EventReservationsVM model = new EventReservationsVM();
                //EventReservation ev = new EventReservation();
                model.Events = _context.Events.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = (x.Id == model.EventId)
                }
                ).ToList();
                return View(model);
            }
            EventReservation modeltoDB = new EventReservation
            {
                EventId = eventReservation.EventId
                /*UserId=_userManager.GetUserId(User)*/,
            };
            _context.Add(modeltoDB);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            //ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventReservation.EventId);
            //return View(eventReservation);
        }

        // GET: EventReservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EventReservations == null)
            {
                return NotFound();
            }

            var eventReservation = await _context.EventReservations.FindAsync(id);
            if (eventReservation == null)
            {
                return NotFound();
            }
            EventReservationsVM model = new EventReservationsVM();
            model.Events = _context.Events.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = (x.Id == model.EventId)
            }).ToList();
            return View(model);
            //ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventReservation.EventId);
            //return View(eventReservation);
        }

        // POST: EventReservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EventId,UserId")] EventReservationsVM evVM)
        {
            if (id != evVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //try
                //{
                //    _context.Update(eventReservation);
                //    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!EventReservationExists(eventReservation.Id))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                //return RedirectToAction(nameof(Index));
                return View(evVM);
            }

            EventReservation er = new EventReservation
            {
                //Id = id,
                //UserId = _userManager.GetUserId(User),
                //EventId = er.EventId
            };
            try
            {
                _context.Update(er);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventReservationExists(er.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Details", new { id = id });
            //ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventReservation.EventId);
            //return View(eventReservation);
        }

        // GET: EventReservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: EventReservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //if (_context.EventReservations == null)
            //{
            //    return Problem("Entity set 'AdventureContext.EventReservations'  is null.");
            //}
            var eventReservation = await _context.EventReservations
                .Include(u => u.User)
                .FirstOrDefaultAsync(x => x.Id == id);
                //FindAsync(id);

            _context.EventReservations.Remove(eventReservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventReservationExists(int id)
        {
            return (_context.EventReservations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
