using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using grow.Data;
using grow.Models;
using grow.Models.ViewModels;

namespace grow.Controllers
{
    public class WatersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WatersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Waters
        public async Task<IActionResult> Index()
        {
            return View(await _context.Water.ToListAsync());
        }

        // GET: Waters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var water = await _context.Water
                .Include(pa => pa.PlantAudits)
                    .ThenInclude(p => p.Plant)
                .FirstOrDefaultAsync(m => m.WaterId == id);

            if (water == null)
            {
                return NotFound();
            }

            return View(water);
        }

        // GET: Waters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Waters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WaterId,Regularity")] Water water)
        {
            if (ModelState.IsValid)
            {
                _context.Add(water);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(water);
        }

        // GET: Waters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var water = await _context.Water.FindAsync(id);
            if (water == null)
            {
                return NotFound();
            }
            return View(water);
        }

        // POST: Waters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WaterId,Regularity")] Water water)
        {
            if (id != water.WaterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(water);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WaterExists(water.WaterId))
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
            return View(water);
        }

        // GET: Waters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var water = await _context.Water
                .FirstOrDefaultAsync(m => m.WaterId == id);
            if (water == null)
            {
                return NotFound();
            }

            return View(water);
        }

        // POST: Waters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var water = await _context.Water.FindAsync(id);
            _context.Water.Remove(water);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WaterExists(int id)
        {
            return _context.Water.Any(e => e.WaterId == id);
        }
    }
}
