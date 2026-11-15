using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AIFreelancingPlatform.Data;
using AIFreelancingPlatform.Models.Database;

namespace AIFreelancingPlatform.Controllers
{
    public class AIServiceController : Controller
    {
        private readonly FreelancingDbContext _context;

        public AIServiceController(FreelancingDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var services = await _context.AI_Services.ToListAsync();
            return View(services);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AI_Service service)
        {
            ModelState.Remove("Freelancers");

            if (ModelState.IsValid)
            {
                _context.AI_Services.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(service);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var service = await _context.AI_Services.FindAsync(id);

            if (service == null)
                return NotFound();

            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AI_Service service)
        {
            if (id != service.Service_ID)
                return NotFound();

            ModelState.Remove("Freelancers");

            if (ModelState.IsValid)
            {
                _context.AI_Services.Update(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(service);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var service = await _context.AI_Services.FindAsync(id);

            if (service == null)
                return NotFound();

            return View(service);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.AI_Services.FindAsync(id);

            if (service != null)
            {
                _context.AI_Services.Remove(service);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}