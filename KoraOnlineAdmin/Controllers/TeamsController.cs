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
    public class TeamsController : Controller
    {
        private readonly KoraOnline _context;

        public TeamsController(KoraOnline context)
        {
            _context = context;
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            var koraOnline = _context.Teams.Include(t => t.Coach).Include(t => t.League);
            return View(await koraOnline.ToListAsync());
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(t => t.Coach)
                .Include(t => t.League)
                .FirstOrDefaultAsync(m => m.TeamId == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            ViewData["CoachId"] = new SelectList(_context.Coaches, "CoachId", "CoachName");
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "LeagueId", "LeagueName");
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamId,TeamName,TeamMatchPlayed,TeamWon,TeamDraw,TeamLost,TeamGoalFor,TeamGoalAg,TeamGoalOn,LeagueId,CoachId,TeamImage")] Team team)
        {
            if (ModelState.IsValid)
            {
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoachId"] = new SelectList(_context.Coaches, "CoachId", "CoachId", team.CoachId);
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "LeagueId", "LeagueId", team.LeagueId);
            return View(team);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            ViewData["CoachId"] = new SelectList(_context.Coaches, "CoachId", "CoachName", team.CoachId);
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "LeagueId", "LeagueName", team.LeagueId);
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeamId,TeamName,TeamMatchPlayed,TeamWon,TeamDraw,TeamLost,TeamGoalFor,TeamGoalAg,TeamGoalOn,LeagueId,CoachId,TeamImage")] Team team)
        {
            if (id != team.TeamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.TeamId))
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
            ViewData["CoachId"] = new SelectList(_context.Coaches, "CoachId", "CoachId", team.CoachId);
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "LeagueId", "LeagueId", team.LeagueId);
            return View(team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(t => t.Coach)
                .Include(t => t.League)
                .FirstOrDefaultAsync(m => m.TeamId == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.TeamId == id);
        }
    }
}
