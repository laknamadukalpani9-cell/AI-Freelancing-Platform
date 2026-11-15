using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AIFreelancingPlatform.Data;
using AIFreelancingPlatform.Models.Database;

namespace AIFreelancingPlatform.Controllers
{
    public class ProjectRequirementController : Controller
    {
        private readonly FreelancingDbContext _context;

        public ProjectRequirementController(FreelancingDbContext context)
        {
            _context = context;
        }

        private void LoadDropdowns()
        {
            ViewBag.Project_ID = new SelectList(
                _context.Projects,
                "Project_ID",
                "Project_Name");

            ViewBag.Skill_ID = new SelectList(
                _context.AI_Skills,
                "Skill_ID",
                "Skill_Name");
        }

        public async Task<IActionResult> Index()
        {
            var requirements = await _context.Project_Requirements
                .Include(r => r.Project)
                .Include(r => r.Skill)
                .ToListAsync();

            return View(requirements);
        }

        public IActionResult Create()
        {
            LoadDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project_Requirement requirement)
        {
            ModelState.Remove("Project");
            ModelState.Remove("Skill");

            if (ModelState.IsValid)
            {
                _context.Project_Requirements.Add(requirement);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            LoadDropdowns();
            return View(requirement);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var requirement = await _context.Project_Requirements.FindAsync(id);

            if (requirement == null)
                return NotFound();

            LoadDropdowns();
            return View(requirement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Project_Requirement requirement)
        {
            if (id != requirement.Requirement_ID)
                return NotFound();

            ModelState.Remove("Project");
            ModelState.Remove("Skill");

            if (ModelState.IsValid)
            {
                _context.Project_Requirements.Update(requirement);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            LoadDropdowns();
            return View(requirement);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var requirement = await _context.Project_Requirements
                .Include(r => r.Project)
                .Include(r => r.Skill)
                .FirstOrDefaultAsync(r => r.Requirement_ID == id);

            if (requirement == null)
                return NotFound();

            return View(requirement);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var requirement = await _context.Project_Requirements.FindAsync(id);

            if (requirement != null)
            {
                _context.Project_Requirements.Remove(requirement);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}