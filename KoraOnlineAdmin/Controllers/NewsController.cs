using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoraOnlineAdmin.Models;

namespace KoraOnlineAdmin.Controllers
{
    public class NewsController : Controller
    {
        private readonly KoraOnline _context;

        public NewsController(KoraOnline context)
        {
            _context = context;
        }

        // GET: News
        public async Task<IActionResult> Index()
        {
            var koraOnline = _context.News.Include(n => n.League).Include(n => n.Match).Include(n => n.NewsCategory).Include(n => n.Player).Include(n => n.Stad).Include(n => n.Team);
            return View(await koraOnline.ToListAsync());
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.League)
                .Include(n => n.Match)
                .Include(n => n.NewsCategory)
                .Include(n => n.Player)
                .Include(n => n.Stad)
                .Include(n => n.Team)
                .FirstOrDefaultAsync(m => m.NewsId == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: News/Create
        public IActionResult Create()
        {
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "LeagueId", "LeagueId");
            ViewData["MatchId"] = new SelectList(_context.Matches, "MatchId", "MatchId");
            ViewData["NewsCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "PlayerId");
            ViewData["StadId"] = new SelectList(_context.Stadia, "StadiumId", "StadiumId");
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "TeamId");
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NewsId,NewsTitle,NewsDate,NewsTime,NewsCategoryId,NewsImage,NewsWriter,NewsDescription,CoachId,LeagueId,TeamId,StadId,MatchId,PlayerId")] News news)
        {
            if (ModelState.IsValid)
            {
                _context.Add(news);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "LeagueId", "LeagueId", news.LeagueId);
            ViewData["MatchId"] = new SelectList(_context.Matches, "MatchId", "MatchId", news.MatchId);
            ViewData["NewsCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", news.NewsCategoryId);
            ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "PlayerId", news.PlayerId);
            ViewData["StadId"] = new SelectList(_context.Stadia, "StadiumId", "StadiumId", news.StadId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "TeamId", news.TeamId);
            return View(news);
        }

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "LeagueId", "LeagueId", news.LeagueId);
            ViewData["MatchId"] = new SelectList(_context.Matches, "MatchId", "MatchId", news.MatchId);
            ViewData["NewsCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", news.NewsCategoryId);
            ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "PlayerId", news.PlayerId);
            ViewData["StadId"] = new SelectList(_context.Stadia, "StadiumId", "StadiumId", news.StadId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "TeamId", news.TeamId);
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NewsId,NewsTitle,NewsDate,NewsTime,NewsCategoryId,NewsImage,NewsWriter,NewsDescription,CoachId,LeagueId,TeamId,StadId,MatchId,PlayerId")] News news)
        {
            if (id != news.NewsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(news);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.NewsId))
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
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "LeagueId", "LeagueId", news.LeagueId);
            ViewData["MatchId"] = new SelectList(_context.Matches, "MatchId", "MatchId", news.MatchId);
            ViewData["NewsCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", news.NewsCategoryId);
            ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "PlayerId", news.PlayerId);
            ViewData["StadId"] = new SelectList(_context.Stadia, "StadiumId", "StadiumId", news.StadId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "TeamId", news.TeamId);
            return View(news);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.League)
                .Include(n => n.Match)
                .Include(n => n.NewsCategory)
                .Include(n => n.Player)
                .Include(n => n.Stad)
                .Include(n => n.Team)
                .FirstOrDefaultAsync(m => m.NewsId == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _context.News.FindAsync(id);
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(e => e.NewsId == id);
        }
    }
}
