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
    public class PunishmentsController : Controller
    {
        private readonly KoraOnline _context;

        public PunishmentsController(KoraOnline context)
        {
            _context = context;
        }

        // GET: Punishments
        public async Task<IActionResult> Index()
        {
            var koraOnline = _context.Punishments.Include(p => p.Player);
            return View(await koraOnline.ToListAsync());
        }

        // GET: Punishments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var punishment = await _context.Punishments
                .Include(p => p.Player)
                .FirstOrDefaultAsync(m => m.PunishId == id);
            if (punishment == null)
            {
                return NotFound();
            }

            return View(punishment);
        }

        // GET: Punishments/Create
        public IActionResult Create()
        {
            ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "PlayerName");
            return View();
        }

        // POST: Punishments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PunishId,PunishType,PlayerId")] Punishment punishment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(punishment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "PlayerId", punishment.PlayerId);
            return View(punishment);
        }

        // GET: Punishments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var punishment = await _context.Punishments.FindAsync(id);
            if (punishment == null)
            {
                return NotFound();
            }
            ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "PlayerName", punishment.PlayerId);
            return View(punishment);
        }

        // POST: Punishments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PunishId,PunishType,PlayerId")] Punishment punishment)
        {
            if (id != punishment.PunishId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(punishment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PunishmentExists(punishment.PunishId))
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
            ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "PlayerId", punishment.PlayerId);
            return View(punishment);
        }

        // GET: Punishments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var punishment = await _context.Punishments
                .Include(p => p.Player)
                .FirstOrDefaultAsync(m => m.PunishId == id);
            if (punishment == null)
            {
                return NotFound();
            }

            return View(punishment);
        }

        // POST: Punishments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var punishment = await _context.Punishments.FindAsync(id);
            _context.Punishments.Remove(punishment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PunishmentExists(int id)
        {
            return _context.Punishments.Any(e => e.PunishId == id);
        }
    }
}
