using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AIFreelancingPlatform.Data;
using AIFreelancingPlatform.Models.Database;

namespace AIFreelancingPlatform.Controllers
{
    public class ReviewController : Controller
    {
        private readonly FreelancingDbContext _context;

        public ReviewController(FreelancingDbContext context)
        {
            _context = context;
        }

        private void LoadDropdowns()
        {
            ViewBag.Assignment_ID = new SelectList(
                _context.Assignments,
                "Assignment_ID",
                "Assignment_ID");
        }

        public async Task<IActionResult> Index()
        {
            var reviews = await _context.Reviews
                .Include(r => r.Assignment)
                .ToListAsync();

            return View(reviews);
        }

        public IActionResult Create()
        {
            LoadDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Review review)
        {
            ModelState.Remove("Assignment");

            if (ModelState.IsValid)
            {
                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            LoadDropdowns();
            return View(review);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
                return NotFound();

            LoadDropdowns();
            return View(review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Review review)
        {
            if (id != review.Review_ID)
                return NotFound();

            ModelState.Remove("Assignment");

            if (ModelState.IsValid)
            {
                _context.Reviews.Update(review);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            LoadDropdowns();
            return View(review);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var review = await _context.Reviews
                .Include(r => r.Assignment)
                .FirstOrDefaultAsync(r => r.Review_ID == id);

            if (review == null)
                return NotFound();

            return View(review);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}