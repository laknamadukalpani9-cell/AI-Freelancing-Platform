using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AIFreelancingPlatform.Data;
using AIFreelancingPlatform.Models.Database;

namespace AIFreelancingPlatform.Controllers
{
    public class AISkillController : Controller
    {
        private readonly FreelancingDbContext _context;

        public AISkillController(FreelancingDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var skills = await _context.AI_Skills.ToListAsync();
            return View(skills);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AI_Skill skill)
        {
            ModelState.Remove("Project_Requirements");
            ModelState.Remove("Freelancers");

            if (ModelState.IsValid)
            {
                _context.AI_Skills.Add(skill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(skill);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var skill = await _context.AI_Skills.FindAsync(id);

            if (skill == null)
                return NotFound();

            return View(skill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AI_Skill skill)
        {
            if (id != skill.Skill_ID)
                return NotFound();

            ModelState.Remove("Project_Requirements");
            ModelState.Remove("Freelancers");

            if (ModelState.IsValid)
            {
                _context.AI_Skills.Update(skill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(skill);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var skill = await _context.AI_Skills.FindAsync(id);

            if (skill == null)
                return NotFound();

            return View(skill);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skill = await _context.AI_Skills.FindAsync(id);

            if (skill != null)
            {
                _context.AI_Skills.Remove(skill);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}