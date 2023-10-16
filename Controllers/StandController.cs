using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PenShop.Data;
using PenShop.Models;

namespace PenShop.Controllers
{
    public class StandController : Controller
    {
        private readonly PenShopContext _context;

        public StandController(PenShopContext context)
        {
            _context = context;
        }

        // GET: Stand
        public async Task<IActionResult> Index()
        {
            return View(await _context.Stand.Select(x => x.Id).ToListAsync());
        }

        // GET: Stand/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Stand == null)
            {
                return NotFound();
            }

            var stand = await _context.Stand
                .Include(s => s.Material)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stand == null)
            {
                return NotFound();
            }

            return View(stand);
        }

        // GET: Stand/ProductCard/5
        public async Task<IActionResult> ProductCard(int? id)
        {
            if (id == null || _context.Stand == null)
            {
                return NotFound();
            }

            var stand = await _context.Stand
                .Include(s => s.Material)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stand == null)
            {
                return NotFound();
            }

            return await ProductCard(stand);
        }

        // GET: Stand/ProductCard/5
        public Task<IActionResult> ProductCard(Stand stand)
        {
            return Task.FromResult<IActionResult>(PartialView("/Views/Stand/ProductCard.cshtml", stand));
        }

        // GET: Stand/Create
        public IActionResult Create()
        {
            ViewData["MaterialId"] = new SelectList(_context.Material, nameof(Material.Id), nameof(Material.Name));
            return View();
        }

        // POST: Stand/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaterialId,Id,Price,Name,Description")] Stand stand)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaterialId"] = new SelectList(_context.Material, nameof(Material.Id), nameof(Material.Name), stand.MaterialId);
            return View(stand);
        }

        // GET: Stand/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Stand == null)
            {
                return NotFound();
            }

            var stand = await _context.Stand.FindAsync(id);
            if (stand == null)
            {
                return NotFound();
            }
            ViewData["MaterialId"] = new SelectList(_context.Material, nameof(Material.Id), nameof(Material.Name), stand.MaterialId);
            return View(stand);
        }

        // POST: Stand/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaterialId,Id,Price,Name,Description")] Stand stand)
        {
            if (id != stand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StandExists(stand.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaterialId"] = new SelectList(_context.Material, nameof(Material.Id), nameof(Material.Name), stand.MaterialId);
            return View(stand);
        }

        // GET: Stand/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Stand == null)
            {
                return NotFound();
            }

            var stand = await _context.Stand
                .Include(s => s.Material)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stand == null)
            {
                return NotFound();
            }

            return View(stand);
        }

        // POST: Stand/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Stand == null)
            {
                return Problem("Entity set 'PenShopContext.Stand'  is null.");
            }
            var stand = await _context.Stand.FindAsync(id);
            if (stand != null)
            {
                _context.Stand.Remove(stand);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StandExists(int id)
        {
          return (_context.Stand?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
