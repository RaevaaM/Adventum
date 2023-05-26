using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Adventum.Data;

namespace Adventum.Controllers
{
    public class LocationsController : Controller
    {
        private readonly AdventureContext _context;

        public LocationsController(AdventureContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _context.Locations.Include(l => l.Events).ToListAsync();
            
            if(!result.Any()) 
                Problem("Entity set 'AdventureContext.Locations'  is null.");
            
            return View(result);
        }
        
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Latitude,Longitude")] Location location)
        {
            if (!ModelState.IsValid)
                return View(location);
            
            _context.Add(location);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            var location = await _context.Locations.FindAsync(id);
            return View(location);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Latitude,Longitude")] Location location)
        {
            if (!ModelState.IsValid)
                return View(location);

            _context.Update(location);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            var location = await _context.Locations.FirstAsync(m => m.Id == id);
            return View(location);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var location = await _context.Locations.FirstAsync(l => l.Id == id);
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
