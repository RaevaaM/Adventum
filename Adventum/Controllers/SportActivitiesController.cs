using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Adventum.Data;


namespace Adventum.Controllers
{
    public class SportActivitiesController : Controller
    {
        private const string youTubeSplitString = "watch?v=";
        
        private readonly AdventureContext _context;

        public SportActivitiesController(AdventureContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var activities = await _context.SportActivities.ToListAsync();
            
            activities.ForEach(a => a.VideoURL = a.VideoURL.Split(youTubeSplitString)[1]);
            
            return View(activities);
        }


        // public async Task<IActionResult> Details(int? id)
        // {
        //     if (id == null || _context.SportActivities == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     SportActivity sportActivity = await _context.SportActivities
        //         .Include(sa=>sa.VideoURL)
        //         .FirstOrDefaultAsync(m => m.Id == id);
        //     if (sportActivity == null)
        //     {
        //         return NotFound();
        //     }
        //     //var videoPath = Path.Combine(wwwroot, "SportActivity");
        //     //SportActivityVM modelVM = new SportActivityVM
        //     //{
        //     //    Name = modelVM.Name,
        //     //    Description = modelVM.Description,
        //     //    VideoURL = modelVM.VideoURL
        //     //};
        //     return View(sportActivity);
        // }

        
        public IActionResult Create() => View();


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,VideoURL")] SportActivity sportActivity)
        {
            if (!ModelState.IsValid)
            {
                return View(sportActivity);
            }

            _context.Add(sportActivity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: SportActivities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SportActivities == null)
            {
                return NotFound();
            }

            var sportActivity = await _context.SportActivities.FindAsync(id);
            if (sportActivity == null)
            {
                return NotFound();
            }
            return View(sportActivity);
        }

        // POST: SportActivities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,VideoURL")] SportActivity sportActivity)
        {
            if (id != sportActivity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sportActivity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SportActivityExists(sportActivity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sportActivity);
        }
        
        
        public async Task<IActionResult> Delete(int id)
        {
            var sportActivity = await _context.SportActivities.FirstOrDefaultAsync(m => m.Id == id);

            return sportActivity == null ? NotFound() : View(sportActivity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var sportActivity = await _context.SportActivities.FindAsync(id);

            if (sportActivity == null)
                return RedirectToAction(nameof(Index));

            _context.SportActivities.Remove(sportActivity);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool SportActivityExists(int id)
        {
          return (_context.SportActivities?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
