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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace grow.Controllers
{
    /*
        Plant class.
        Contains all methods for plant

    */

    public class PlantsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _appEnvironment;

        public PlantsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _appEnvironment = appEnvironment;
        }

        // method to get current logged in user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        // GET: Plants
        public async Task<IActionResult> Index()
        {
            // get current user
            var user = await GetCurrentUserAsync();

            // get user's plants
            var applicationDbContext = _context.Plant
                .Include(p => p.PlantType)
                .Include(p => p.User)
                .Where(u => u.User == user);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Plants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DetailsPlantViewModel viewmodel = new DetailsPlantViewModel(_context);

            // get selected plant
            var plant = await _context.Plant
                .Include(p => p.PlantType)
                .Include(l => l.Light)
                .Include(w => w.Water)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PlantId == id);

            // get plant audits for selected plant
            var plantAudit = _context.PlantAudit
                .Where(pa => pa.PlantId == id)
                .OrderByDescending(o => o.DateCreated)
                .ToList();

            viewmodel.Plant = plant;
            viewmodel.PlantAudit = plantAudit;

            if (plant == null)
            {
                return NotFound();
            }

            return View(viewmodel);
        }


        // GET: Plants/Create
        public IActionResult Create()
        {
            ViewData["LightId"] = new SelectList(_context.Light, "LightId", "Requirements");
            ViewData["PlantTypeId"] = new SelectList(_context.PlantType, "PlantTypeId", "Name");
            ViewData["WaterId"] = new SelectList(_context.Water, "WaterId", "Regularity");
            return View();
        }


        // POST: Plants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlantId,DateCreated,Name,Notes,InitialImage,UserId,PlantTypeId,WaterId,LightId")] Plant plant, IFormFile file)
        {
            // Remove user and userId
            ModelState.Remove("UserId");
            ModelState.Remove("User");
            ModelState.Remove("DateCreated");
            ModelState.Remove("InitialImage");

            // IMAGE UPLOAD
            // make sure file is selected
            if (file == null || file.Length == 0) return Content("file not selected");

            // get path location to store img
            string path_Root = _appEnvironment.WebRootPath;

            // get only file name without file path
            var trimmedFileName = System.Guid.NewGuid().ToString() + System.IO.Path.GetFileName(file.FileName);

            // store file location
            string path_to_Images = path_Root + "\\User_Files\\Images\\" + trimmedFileName;

            if (ModelState.IsValid)
            {
                // Get the current user
                var user = await GetCurrentUserAsync();
                plant.User = user;
                plant.UserId = user.Id;

                // copy file to target
                using (var stream = new FileStream(path_to_Images, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                plant.InitialImage = trimmedFileName;

                _context.Add(plant);
                await _context.SaveChangesAsync();

                ViewData["FilePath"] = path_to_Images;

                return RedirectToAction(nameof(Index));
            }
            return View(plant);
        }


        // GET: Plants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var plant = await _context.Plant.FindAsync(id);

            if (plant == null)
            {
                return NotFound();
            }

            ViewData["PlantTypeId"] = new SelectList(_context.PlantType, "PlantTypeId", "Name", plant.PlantTypeId);
            return View(plant);
        }


        // POST: Plants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlantId,DateCreated,Name,Notes,InitialImage,UserId,PlantTypeId,WaterId,LightId")] Plant plant)
        {
            if (id != plant.PlantId)
            {
                return NotFound();
            }

            ModelState.Remove("UserId");
            ModelState.Remove("User");
            ModelState.Remove("WaterId");
            ModelState.Remove("LightId");

            if (ModelState.IsValid)
            {
                try
                {
                    // Get the current user
                    var user = await GetCurrentUserAsync();

                    // get current plant by id
                    var currentPlant = await _context.Plant.FindAsync(id);

                    // update according to filled out fields
                    // NOTE: user cannot update plant's water or light requirements on this edit - that must be done from the plant audit
                    currentPlant.User = user;
                    currentPlant.Name = plant.Name;
                    currentPlant.Notes = plant.Notes;
                    currentPlant.PlantTypeId = plant.PlantTypeId;
             
                    _context.Update(currentPlant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlantExists(plant.PlantId))
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
            ViewData["PlantTypeId"] = new SelectList(_context.PlantType, "PlantTypeId", "Name", plant.PlantTypeId);

            return View(plant);
        }


        // GET: Plants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plant = await _context.Plant
                .Include(p => p.PlantType)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PlantId == id);
            if (plant == null)
            {
                return NotFound();
            }

            return View(plant);
        }

        // POST: Plants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // get selected plant
            var plant = await _context.Plant
                .FindAsync(id);

            // get all plant audits that have selected plant as a foreign key
            var plantAudits = await _context.PlantAudit
                .Include(p => p.Plant)
                .Where(pa => pa.PlantId == plant.PlantId)
                .ToListAsync();

            // delete all plant audit entries for selected plant
            foreach (var pa in plantAudits)
            {
                _context.PlantAudit.Remove(pa);
            }

            // delete plant
            _context.Plant.Remove(plant);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlantExists(int id)
        {
            return _context.Plant.Any(e => e.PlantId == id);
        }
    }
}
