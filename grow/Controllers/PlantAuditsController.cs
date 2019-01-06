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
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace grow.Controllers
{
    public class PlantAuditsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _appEnvironment;

        public PlantAuditsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _appEnvironment = appEnvironment;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: PlantAudits
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PlantAudit.Include(p => p.Light).Include(p => p.Plant).Include(p => p.Water);
            return View(await applicationDbContext.ToListAsync());
        }



        // GET: PlantAudits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantAudit = await _context.PlantAudit
                .Include(p => p.Light)
                .Include(p => p.Plant)
                .Include(p => p.Water)
                .FirstOrDefaultAsync(m => m.PlantAuditId == id);
            if (plantAudit == null)
            {
                return NotFound();
            }

            return View(plantAudit);
        }

        // GET: PlantAudits/Create/4
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // get plant based on id passed in
            var plant = await _context.Plant.FindAsync(id);

            CreatePlantAuditViewModel viewModel = new CreatePlantAuditViewModel(_context);

            viewModel.Plant = plant;

            return View(viewModel);
        }

        // POST: PlantAudits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePlantAuditViewModel model, int id, IFormFile file)
        {
            ModelState.Remove("User");
            ModelState.Remove("UserId");
            ModelState.Remove("PlantAudit.PlantId");
            ModelState.Remove("PlantAudit.UpdatedImage");

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

                // get current plant by id
                var plant = await _context.Plant.FindAsync(id);

                // match plant audit with current plant
                model.PlantAudit.PlantId = plant.PlantId;

                // assign plant water id to selected water id
                plant.WaterId = model.PlantAudit.WaterId;

                model.Plant = plant;

                // copy file to target
                using (var stream = new FileStream(path_to_Images, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                model.PlantAudit.UpdatedImage = trimmedFileName;

                _context.Add(model.PlantAudit);
                await _context.SaveChangesAsync();

                ViewData["FilePath"] = path_to_Images;

                return RedirectToAction("Details", "Plants", new { @id = id});
            }
 
            return View(model);
        }

        // GET: PlantAudits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantAudit = await _context.PlantAudit.FindAsync(id);
            if (plantAudit == null)
            {
                return NotFound();
            }
            ViewData["LightId"] = new SelectList(_context.Light, "LightId", "Requirements", plantAudit.LightId);
            ViewData["PlantId"] = new SelectList(_context.Plant, "PlantId", "Name", plantAudit.PlantId);
            ViewData["WaterId"] = new SelectList(_context.Water, "WaterId", "Regularity", plantAudit.WaterId);
            return View(plantAudit);
        }

        // POST: PlantAudits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlantAuditId,DateCreated,PlantId,WaterId,LightId,RequirementsChanged,InfestationIssue,Notes,UpdatedImage")] PlantAudit plantAudit)
        {
            if (id != plantAudit.PlantAuditId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plantAudit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlantAuditExists(plantAudit.PlantAuditId))
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
            ViewData["LightId"] = new SelectList(_context.Light, "LightId", "Requirements", plantAudit.LightId);
            ViewData["PlantId"] = new SelectList(_context.Plant, "PlantId", "Name", plantAudit.PlantId);
            ViewData["WaterId"] = new SelectList(_context.Water, "WaterId", "Regularity", plantAudit.WaterId);
            return View(plantAudit);
        }

        // GET: PlantAudits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantAudit = await _context.PlantAudit
                .Include(p => p.Light)
                .Include(p => p.Plant)
                .Include(p => p.Water)
                .FirstOrDefaultAsync(m => m.PlantAuditId == id);
            if (plantAudit == null)
            {
                return NotFound();
            }

            return View(plantAudit);
        }

        // POST: PlantAudits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plantAudit = await _context.PlantAudit.FindAsync(id);
            _context.PlantAudit.Remove(plantAudit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlantAuditExists(int id)
        {
            return _context.PlantAudit.Any(e => e.PlantAuditId == id);
        }
    }
}
