using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoraOnlineAdmin.Models;
using Microsoft.AspNetCore.Authorization;

namespace KoraOnlineAdmin.Controllers
{
    [Authorize(Roles = "Main_admin,Other_admin")]
    public class StadiaController : Controller
    {
        private readonly KoraOnline _context;

        public StadiaController(KoraOnline context)
        {
            _context = context;
        }

        // GET: Stadia
        public async Task<IActionResult> Index()
        {
            var koraOnline = _context.Stadia.Include(s => s.City);
            return View(await koraOnline.ToListAsync());
        }

        // GET: Stadia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stadium = await _context.Stadia
                .Include(s => s.City)
                .FirstOrDefaultAsync(m => m.StadiumId == id);
            if (stadium == null)
            {
                return NotFound();
            }

            return View(stadium);
        }

        // GET: Stadia/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityName");
            return View();
        }

        // POST: Stadia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StadiumId,StadiumName,SatdiumCapacity,CityId")] Stadium stadium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stadium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityId", stadium.CityId);
            return View(stadium);
        }

        // GET: Stadia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stadium = await _context.Stadia.FindAsync(id);
            if (stadium == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityName", stadium.CityId);
            return View(stadium);
        }

        // POST: Stadia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StadiumId,StadiumName,SatdiumCapacity,CityId")] Stadium stadium)
        {
            if (id != stadium.StadiumId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stadium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StadiumExists(stadium.StadiumId))
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
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityId", stadium.CityId);
            return View(stadium);
        }

        // GET: Stadia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stadium = await _context.Stadia
                .Include(s => s.City)
                .FirstOrDefaultAsync(m => m.StadiumId == id);
            if (stadium == null)
            {
                return NotFound();
            }

            return View(stadium);
        }

        // POST: Stadia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stadium = await _context.Stadia.FindAsync(id);
            _context.Stadia.Remove(stadium);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StadiumExists(int id)
        {
            return _context.Stadia.Any(e => e.StadiumId == id);
        }
    }
}
