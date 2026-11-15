using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AIFreelancingPlatform.Data;
using AIFreelancingPlatform.Models.Database;

namespace AIFreelancingPlatform.Controllers
{
    public class ProposalController : Controller
    {
        private readonly FreelancingDbContext _context;

        public ProposalController(FreelancingDbContext context)
        {
            _context = context;
        }

        private void LoadDropdowns()
        {
            ViewBag.Project_ID = new SelectList(
                _context.Projects,
                "Project_ID",
                "Project_Name");

            ViewBag.Freelancer_ID = new SelectList(
                _context.Freelancers,
                "Freelancer_ID",
                "Name");
        }

        public async Task<IActionResult> Index()
        {
            var proposals = await _context.Proposals
                .Include(p => p.Project)
                .Include(p => p.Freelancer)
                .ToListAsync();

            return View(proposals);
        }

        public IActionResult Create()
        {
            LoadDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Proposal proposal)
        {
            ModelState.Remove("Project");
            ModelState.Remove("Freelancer");

            if (ModelState.IsValid)
            {
                _context.Proposals.Add(proposal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            LoadDropdowns();
            return View(proposal);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var proposal = await _context.Proposals.FindAsync(id);

            if (proposal == null)
                return NotFound();

            LoadDropdowns();
            return View(proposal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Proposal proposal)
        {
            if (id != proposal.Proposal_ID)
                return NotFound();

            ModelState.Remove("Project");
            ModelState.Remove("Freelancer");

            if (ModelState.IsValid)
            {
                _context.Proposals.Update(proposal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            LoadDropdowns();
            return View(proposal);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var proposal = await _context.Proposals
                .Include(p => p.Project)
                .Include(p => p.Freelancer)
                .FirstOrDefaultAsync(p => p.Proposal_ID == id);

            if (proposal == null)
                return NotFound();

            return View(proposal);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proposal = await _context.Proposals.FindAsync(id);

            if (proposal != null)
            {
                _context.Proposals.Remove(proposal);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}