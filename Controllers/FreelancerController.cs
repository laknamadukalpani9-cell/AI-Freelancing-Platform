
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AIFreelancingPlatform.Data;
using AIFreelancingPlatform.Models.Database;

namespace AIFreelancingPlatform.Controllers
{
    public class FreelancerController : Controller
    {
        private readonly FreelancingDbContext _context;

        public FreelancerController(FreelancingDbContext context)
        {
            _context = context;
        }

        // Display all freelancers
        public async Task<IActionResult> Index()
        {
            var freelancers = await _context.Freelancers.ToListAsync();
            return View(freelancers);
        }

        // Display Create Form
        public IActionResult Create()
        {
            return View();
        }

        // Save New Freelancer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Freelancer freelancer)
        {
            if (ModelState.IsValid)
            {
                _context.Freelancers.Add(freelancer);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(freelancer);
        }
        // Display Edit Form
public async Task<IActionResult> Edit(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var freelancer = await _context.Freelancers.FindAsync(id);

    if (freelancer == null)
    {
        return NotFound();
    }

    return View(freelancer);
}

// Update Freelancer
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, Freelancer freelancer)
{
    if (id != freelancer.Freelancer_ID)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        _context.Freelancers.Update(freelancer);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    return View(freelancer);
}
// Display Delete Confirmation
public async Task<IActionResult> Delete(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var freelancer = await _context.Freelancers.FindAsync(id);

    if (freelancer == null)
    {
        return NotFound();
    }

    return View(freelancer);
}

// Delete Freelancer
[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(int id)
{
    var freelancer = await _context.Freelancers.FindAsync(id);

    if (freelancer != null)
    {
        _context.Freelancers.Remove(freelancer);
        await _context.SaveChangesAsync();
    }

    return RedirectToAction(nameof(Index));
}
    }
}
