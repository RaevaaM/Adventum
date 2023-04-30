using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Adventum.Data;
using Adventum.Models;

namespace Adventum.Controllers
{
    public class SportActivitiesController : Controller
    {
        private readonly AdventureContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private string wwwroot;

        public SportActivitiesController(AdventureContext context, IWebHostEnvironment hostEnvironment, string wwwroot)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            this.wwwroot = $"{this._hostEnvironment.WebRootPath}";
        }

        // GET: SportActivities
        public async Task<IActionResult> Index()
        {
            return View(await _context.SportActivities.ToListAsync());
        }

        // GET: SportActivities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SportActivities == null)
            {
                return NotFound();
            }

            SportActivity sportActivity = await _context.SportActivities
                .Include(sa=>sa.VideoURL)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sportActivity == null)
            {
                return NotFound();
            }
            //var videoPath = Path.Combine(wwwroot, "SportActivity");
            //SportActivityVM modelVM = new SportActivityVM
            //{
            //    Name = modelVM.Name,
            //    Description = modelVM.Description,
            //    VideoURL = modelVM.VideoURL
            //};
            return View(sportActivity);
        }

        // GET: SportActivities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SportActivities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm][Bind("Id,Name,Description,VideoURL")] SportActivity sportActivity)
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

        // GET: SportActivities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SportActivities == null)
            {
                return NotFound();
            }

            var sportActivity = await _context.SportActivities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sportActivity == null)
            {
                return NotFound();
            }

            return View(sportActivity);
        }

        // POST: SportActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SportActivities == null)
            {
                return Problem("Entity set 'AdventureContext.SportActivities'  is null.");
            }
            var sportActivity = await _context.SportActivities.FindAsync(id);
            if (sportActivity != null)
            {
                _context.SportActivities.Remove(sportActivity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SportActivityExists(int id)
        {
          return (_context.SportActivities?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
