using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Adventum.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Adventum.Controllers;

[Authorize]
public class EventReservationsController : Controller
{
    private readonly AdventureContext _context;
    private readonly UserManager<User> _userManager;

    public EventReservationsController(AdventureContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        string currentUser = _userManager.GetUserId(User);

        IQueryable<EventReservation> query = User.IsInRole(Constants.AdministratorRole)
            ? _context.EventReservations.AsQueryable()
            : _context.EventReservations.Where(x => x.UserId == currentUser);

        List<EventReservation> result = await query
            .Include(er => er.User)
            .Include(er => er.Event)
            .ThenInclude(e => e.SportActivity)
            .ToListAsync();

        return View(result);
    }


    [HttpGet]
    public async Task<IActionResult> Create(int sportActivityId)
    {
        var user = await _userManager.GetUserAsync(User);

        var tomorrow = DateTime.Now.AddDays(1);

        var model = new UserReservationModel
        {
            UserId = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Date = tomorrow,
            SportActivityId = sportActivityId
        };

        return View(model);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserReservationModel model)
    {
        var evt = await _context.Events.FirstOrDefaultAsync(e => e.SportActivityId == model.SportActivityId);

        _context.EventReservations.Add(new EventReservation
        {
            UserId = model.UserId,
            EventId = evt.Id,
            Date = model.Date,
            ParticipantsCount = model.ParticipantCount
        });


        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }


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