
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AIFreelancingPlatform.Data;
using AIFreelancingPlatform.Models.Database;

namespace AIFreelancingPlatform.Controllers
{
    public class ClientController : Controller
    {
        private readonly FreelancingDbContext _context;

        public ClientController(FreelancingDbContext context)
        {
            _context = context;
        }

        // Display all clients
        public async Task<IActionResult> Index()
        {
            var clients = await _context.Clients.ToListAsync();
            return View(clients);
        }

        // Display Create Form
        public IActionResult Create()
        {
            return View();
        }

        // Save New Client
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(client);
        }

        // Display Edit Form
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var client = await _context.Clients.FindAsync(id);

            if (client == null)
                return NotFound();

            return View(client);
        }

        // Update Client
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Client client)
        {
            if (id != client.Client_ID)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Clients.Update(client);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(client);
        }

        // Display Delete Confirmation
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var client = await _context.Clients.FindAsync(id);

            if (client == null)
                return NotFound();

            return View(client);
        }

        // Delete Client
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}