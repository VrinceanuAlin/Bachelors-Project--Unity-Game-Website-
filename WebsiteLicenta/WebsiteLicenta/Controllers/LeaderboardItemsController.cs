using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebsiteLicenta.Data;
using WebsiteLicenta.Models;

namespace WebsiteLicenta.Controllers
{
    public class LeaderboardItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaderboardItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeaderboardItems
        public async Task<IActionResult> Index()
        {
              return _context.BoardItem != null ?
                          View(await _context.BoardItem.OrderByDescending(item => item.score).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.BoardItem'  is null.");
        }

        // GET: LeaderboardItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BoardItem == null)
            {
                return NotFound();
            }

            var leaderboardItem = await _context.BoardItem
                .FirstOrDefaultAsync(m => m.id == id);
            if (leaderboardItem == null)
            {
                return NotFound();
            }

            return View(leaderboardItem);
        }

        // GET: LeaderboardItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaderboardItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,username,score")] LeaderboardItem leaderboardItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leaderboardItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(leaderboardItem);
        }

        // GET: LeaderboardItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BoardItem == null)
            {
                return NotFound();
            }

            var leaderboardItem = await _context.BoardItem.FindAsync(id);
            if (leaderboardItem == null)
            {
                return NotFound();
            }
            return View(leaderboardItem);
        }

        // POST: LeaderboardItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,username,score")] LeaderboardItem leaderboardItem)
        {
            if (id != leaderboardItem.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaderboardItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaderboardItemExists(leaderboardItem.id))
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
            return View(leaderboardItem);
        }

        // GET: LeaderboardItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BoardItem == null)
            {
                return NotFound();
            }

            var leaderboardItem = await _context.BoardItem
                .FirstOrDefaultAsync(m => m.id == id);
            if (leaderboardItem == null)
            {
                return NotFound();
            }

            return View(leaderboardItem);
        }

        // POST: LeaderboardItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BoardItem == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BoardItem'  is null.");
            }
            var leaderboardItem = await _context.BoardItem.FindAsync(id);
            if (leaderboardItem != null)
            {
                _context.BoardItem.Remove(leaderboardItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaderboardItemExists(int id)
        {
          return (_context.BoardItem?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
