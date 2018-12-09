using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using grow.Data;
using grow.Models;

namespace grow.Controllers
{
    public class LightsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LightsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lights
        public async Task<IActionResult> Index()
        {
            return View(await _context.Light.ToListAsync());
        }

        // GET: Lights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var light = await _context.Light
                .FirstOrDefaultAsync(m => m.LightId == id);
            if (light == null)
            {
                return NotFound();
            }

            return View(light);
        }

        // GET: Lights/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LightId,Requirements")] Light light)
        {
            if (ModelState.IsValid)
            {
                _context.Add(light);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(light);
        }

        // GET: Lights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var light = await _context.Light.FindAsync(id);
            if (light == null)
            {
                return NotFound();
            }
            return View(light);
        }

        // POST: Lights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LightId,Requirements")] Light light)
        {
            if (id != light.LightId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(light);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LightExists(light.LightId))
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
            return View(light);
        }

        // GET: Lights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var light = await _context.Light
                .FirstOrDefaultAsync(m => m.LightId == id);
            if (light == null)
            {
                return NotFound();
            }

            return View(light);
        }

        // POST: Lights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var light = await _context.Light.FindAsync(id);
            _context.Light.Remove(light);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LightExists(int id)
        {
            return _context.Light.Any(e => e.LightId == id);
        }
    }
}
