using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AIFreelancingPlatform.Data;
using AIFreelancingPlatform.Models.Database;

namespace AIFreelancingPlatform.Controllers
{
    public class ProjectController : Controller
    {
        private readonly FreelancingDbContext _context;

        public ProjectController(FreelancingDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects
                .Include(p => p.Client)
                .ToListAsync();

            return View(projects);
        }

        public IActionResult Create()
        {
            ViewBag.Client_ID = new SelectList(
                _context.Clients, "Client_ID", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project)
        {
            ModelState.Remove("Client");
            ModelState.Remove("Assignments");
            ModelState.Remove("Project_Requirements");
            ModelState.Remove("Proposals");

            if (ModelState.IsValid)
            {
                _context.Projects.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Client_ID = new SelectList(
                _context.Clients, "Client_ID", "Name",
                project.Client_ID);

            return View(project);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var project = await _context.Projects.FindAsync(id);

            if (project == null)
                return NotFound();

            ViewBag.Client_ID = new SelectList(
                _context.Clients, "Client_ID", "Name",
                project.Client_ID);

            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Project project)
        {
            if (id != project.Project_ID)
                return NotFound();

            ModelState.Remove("Client");
            ModelState.Remove("Assignments");
            ModelState.Remove("Project_Requirements");
            ModelState.Remove("Proposals");

            if (ModelState.IsValid)
            {
                _context.Projects.Update(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Client_ID = new SelectList(
                _context.Clients, "Client_ID", "Name",
                project.Client_ID);

            return View(project);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var project = await _context.Projects
                .Include(p => p.Client)
                .FirstOrDefaultAsync(p => p.Project_ID == id);

            if (project == null)
                return NotFound();

            return View(project);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}