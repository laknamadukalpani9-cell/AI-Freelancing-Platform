using AIFreelancingPlatform.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AIFreelancingPlatform.Controllers
{
    public class FreelancerSkillController : Controller
    {
        private readonly FreelancingDbContext _context;

        public FreelancerSkillController(FreelancingDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            await LoadDropdowns();

            var freelancers = await _context.Freelancers
                .Include(f => f.Skills)
                .ToListAsync();

            ViewBag.Links = freelancers
                .SelectMany(f => f.Skills.Select(s => new
                {
                    Freelancer_ID = f.Freelancer_ID,
                    Skill_ID = s.Skill_ID,
                    FreelancerName = f.Name,
                    SkillName = s.Skill_Name
                }))
                .ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(int Freelancer_ID, int Skill_ID)
        {
            var freelancer = await _context.Freelancers
                .Include(f => f.Skills)
                .FirstOrDefaultAsync(f => f.Freelancer_ID == Freelancer_ID);

            var skill = await _context.AI_Skills
                .FirstOrDefaultAsync(s => s.Skill_ID == Skill_ID);

            if (freelancer != null && skill != null &&
                !freelancer.Skills.Any(s => s.Skill_ID == Skill_ID))
            {
                freelancer.Skills.Add(skill);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int Freelancer_ID, int Skill_ID)
        {
            var freelancer = await _context.Freelancers
                .Include(f => f.Skills)
                .FirstOrDefaultAsync(f => f.Freelancer_ID == Freelancer_ID);

            if (freelancer != null)
            {
                var skill = freelancer.Skills
                    .FirstOrDefault(s => s.Skill_ID == Skill_ID);

                if (skill != null)
                {
                    freelancer.Skills.Remove(skill);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadDropdowns()
        {
            ViewBag.Freelancer_ID = new SelectList(
                await _context.Freelancers.ToListAsync(),
                "Freelancer_ID",
                "Name");

            ViewBag.Skill_ID = new SelectList(
                await _context.AI_Skills.ToListAsync(),
                "Skill_ID",
                "Skill_Name");
        }
    }
}