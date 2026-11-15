using AIFreelancingPlatform.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AIFreelancingPlatform.Controllers
{
    public class FreelancerServiceController : Controller
    {
        private readonly FreelancingDbContext _context;

        public FreelancerServiceController(FreelancingDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            await LoadDropdowns();

            var freelancers = await _context.Freelancers
                .Include(f => f.Services)
                .ToListAsync();

            ViewBag.Links = freelancers
                .SelectMany(f => f.Services.Select(s => new
                {
                    Freelancer_ID = f.Freelancer_ID,
                    Service_ID = s.Service_ID,
                    FreelancerName = f.Name,
                    ServiceName = s.Service_Name
                }))
                .ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(int Freelancer_ID, int Service_ID)
        {
            var freelancer = await _context.Freelancers
                .Include(f => f.Services)
                .FirstOrDefaultAsync(f => f.Freelancer_ID == Freelancer_ID);

            var service = await _context.AI_Services
                .FirstOrDefaultAsync(s => s.Service_ID == Service_ID);

            if (freelancer != null && service != null &&
                !freelancer.Services.Any(s => s.Service_ID == Service_ID))
            {
                freelancer.Services.Add(service);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int Freelancer_ID, int Service_ID)
        {
            var freelancer = await _context.Freelancers
                .Include(f => f.Services)
                .FirstOrDefaultAsync(f => f.Freelancer_ID == Freelancer_ID);

            if (freelancer != null)
            {
                var service = freelancer.Services
                    .FirstOrDefault(s => s.Service_ID == Service_ID);

                if (service != null)
                {
                    freelancer.Services.Remove(service);
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

            ViewBag.Service_ID = new SelectList(
                await _context.AI_Services.ToListAsync(),
                "Service_ID",
                "Service_Name");
        }
    }
}