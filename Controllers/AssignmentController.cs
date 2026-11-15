using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AIFreelancingPlatform.Data;
using AIFreelancingPlatform.Models.Database;

namespace AIFreelancingPlatform.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly FreelancingDbContext _context;

        public AssignmentController(FreelancingDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var assignments = await _context.Assignments
                .Include(a => a.Project)
                .Include(a => a.Freelancer)
                .ToListAsync();

            return View(assignments);
        }

        public IActionResult Create()
        {
            LoadDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Assignment assignment)
        {
            ModelState.Remove("Project");
            ModelState.Remove("Freelancer");
            ModelState.Remove("Payments");
            ModelState.Remove("Reviews");

            if (ModelState.IsValid)
            {
                _context.Assignments.Add(assignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            LoadDropdowns();
            return View(assignment);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var assignment = await _context.Assignments.FindAsync(id);

            if (assignment == null)
                return NotFound();

            LoadDropdowns();
            return View(assignment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Assignment assignment)
        {
            if (id != assignment.Assignment_ID)
                return NotFound();

            ModelState.Remove("Project");
            ModelState.Remove("Freelancer");
            ModelState.Remove("Payments");
            ModelState.Remove("Reviews");

            if (ModelState.IsValid)
            {
                _context.Assignments.Update(assignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            LoadDropdowns();
            return View(assignment);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var assignment = await _context.Assignments
                .Include(a => a.Project)
                .Include(a => a.Freelancer)
                .FirstOrDefaultAsync(a => a.Assignment_ID == id);

            if (assignment == null)
                return NotFound();

            return View(assignment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);

            if (assignment != null)
            {
                _context.Assignments.Remove(assignment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private void LoadDropdowns()
        {
            ViewBag.Project_ID = new SelectList(
                _context.Projects, "Project_ID", "Project_Name");

            ViewBag.Freelancer_ID = new SelectList(
                _context.Freelancers, "Freelancer_ID", "Name");
        }
    }
}