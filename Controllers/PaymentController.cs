using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AIFreelancingPlatform.Data;
using AIFreelancingPlatform.Models.Database;

namespace AIFreelancingPlatform.Controllers
{
    public class PaymentController : Controller
    {
        private readonly FreelancingDbContext _context;

        public PaymentController(FreelancingDbContext context)
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
            var payments = await _context.Payments
                .Include(p => p.Assignment)
                .ToListAsync();

            return View(payments);
        }

        public IActionResult Create()
        {
            LoadDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Payment payment)
        {
            ModelState.Remove("Assignment");

            if (ModelState.IsValid)
            {
                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            LoadDropdowns();
            return View(payment);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
                return NotFound();

            LoadDropdowns();
            return View(payment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Payment payment)
        {
            if (id != payment.Payment_ID)
                return NotFound();

            ModelState.Remove("Assignment");

            if (ModelState.IsValid)
            {
                _context.Payments.Update(payment);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            LoadDropdowns();
            return View(payment);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var payment = await _context.Payments
                .Include(p => p.Assignment)
                .FirstOrDefaultAsync(p => p.Payment_ID == id);

            if (payment == null)
                return NotFound();

            return View(payment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payments.FindAsync(id);

            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}