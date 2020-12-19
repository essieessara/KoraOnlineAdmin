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
    public class MatchesController : Controller
    {
        private readonly KoraOnline _context;

        public MatchesController(KoraOnline context)
        {
            _context = context;
        }

        // GET: Matches
        public async Task<IActionResult> Index()
        {
            var koraOnline = _context.Matches.Include(m => m.League).Include(m => m.Staduim);
            return View(await koraOnline.ToListAsync());
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(m => m.League)
                .Include(m => m.Staduim)
                .FirstOrDefaultAsync(m => m.MatchId == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // GET: Matches/Create
        public IActionResult Create()
        {
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "LeagueId", "LeagueName");
            ViewData["StaduimId"] = new SelectList(_context.Stadia, "StadiumId", "StadiumName");
            return View();
        }

        // POST: Matches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MatchId,MatchDate,MatchTime,MatchStatus,StaduimId,LeagueId,MatchTeamName1,MatchTeamName2,MatchWeek,MatchTeamResult1,MatchTeamResult2")] Match match)
        {
            if (ModelState.IsValid)
            {
                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "LeagueId", "LeagueId", match.LeagueId);
            ViewData["StaduimId"] = new SelectList(_context.Stadia, "StadiumId", "StadiumId", match.StaduimId);
            return View(match);
        }

        // GET: Matches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "LeagueId", "LeagueName", match.LeagueId);
            ViewData["StaduimId"] = new SelectList(_context.Stadia, "StadiumId", "StadiumName", match.StaduimId);
            return View(match);
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MatchId,MatchDate,MatchTime,MatchStatus,StaduimId,LeagueId,MatchTeamName1,MatchTeamName2,MatchWeek,MatchTeamResult1,MatchTeamResult2")] Match match)
        {
            if (id != match.MatchId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(match);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchExists(match.MatchId))
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
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "LeagueId", "LeagueId", match.LeagueId);
            ViewData["StaduimId"] = new SelectList(_context.Stadia, "StadiumId", "StadiumId", match.StaduimId);
            return View(match);
        }

        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(m => m.League)
                .Include(m => m.Staduim)
                .FirstOrDefaultAsync(m => m.MatchId == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchExists(int id)
        {
            return _context.Matches.Any(e => e.MatchId == id);
        }
    }
}
