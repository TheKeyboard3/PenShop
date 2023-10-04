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
    public class RollerballPenController : Controller
    {
        private readonly PenShopContext _context;

        public RollerballPenController(PenShopContext context)
        {
            _context = context;
        }

        // GET: RollerballPen
        public async Task<IActionResult> Index()
        {
            var penShopContext = _context.RollerballPen.Include(r => r.CartridgeStandard).Include(r => r.Material);
            return View(await penShopContext.ToListAsync());
        }

        // GET: RollerballPen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RollerballPen == null)
            {
                return NotFound();
            }

            var rollerballPen = await _context.RollerballPen
                .Include(r => r.CartridgeStandard)
                .Include(r => r.Material)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rollerballPen == null)
            {
                return NotFound();
            }

            return View(rollerballPen);
        }

        // GET: RollerballPen/Create
        public IActionResult Create()
        {
            ViewData["CartridgeStandardId"] = new SelectList(_context.CartridgeStandard, nameof(CartridgeStandard.Id), nameof(CartridgeStandard.Name));
            ViewData["MaterialId"] = new SelectList(_context.Material, nameof(Material.Id), nameof(Material.Name));
            return View();
        }

        // POST: RollerballPen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RollerballDiameter,CartridgeStandardId,MaterialId,Id,Price,Name,Description")] RollerballPen rollerballPen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rollerballPen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartridgeStandardId"] = new SelectList(_context.CartridgeStandard, nameof(CartridgeStandard.Id), nameof(CartridgeStandard.Name), rollerballPen.CartridgeStandardId);
            ViewData["MaterialId"] = new SelectList(_context.Material, nameof(Material.Id), nameof(Material.Name), rollerballPen.MaterialId);
            return View(rollerballPen);
        }

        // GET: RollerballPen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RollerballPen == null)
            {
                return NotFound();
            }

            var rollerballPen = await _context.RollerballPen.FindAsync(id);
            if (rollerballPen == null)
            {
                return NotFound();
            }
            ViewData["CartridgeStandardId"] = new SelectList(_context.CartridgeStandard, nameof(CartridgeStandard.Id), nameof(CartridgeStandard.Name), rollerballPen.CartridgeStandardId);
            ViewData["MaterialId"] = new SelectList(_context.Material, nameof(Material.Id), nameof(Material.Name), rollerballPen.MaterialId);
            return View(rollerballPen);
        }

        // POST: RollerballPen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RollerballDiameter,CartridgeStandardId,MaterialId,Id,Price,Name,Description")] RollerballPen rollerballPen)
        {
            if (id != rollerballPen.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rollerballPen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RollerballPenExists(rollerballPen.Id))
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
            ViewData["CartridgeStandardId"] = new SelectList(_context.CartridgeStandard, nameof(CartridgeStandard.Id), nameof(CartridgeStandard.Name), rollerballPen.CartridgeStandardId);
            ViewData["MaterialId"] = new SelectList(_context.Material, nameof(Material.Id), nameof(Material.Name), rollerballPen.MaterialId);
            return View(rollerballPen);
        }

        // GET: RollerballPen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RollerballPen == null)
            {
                return NotFound();
            }

            var rollerballPen = await _context.RollerballPen
                .Include(r => r.CartridgeStandard)
                .Include(r => r.Material)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rollerballPen == null)
            {
                return NotFound();
            }

            return View(rollerballPen);
        }

        // POST: RollerballPen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RollerballPen == null)
            {
                return Problem("Entity set 'PenShopContext.RollerballPen'  is null.");
            }
            var rollerballPen = await _context.RollerballPen.FindAsync(id);
            if (rollerballPen != null)
            {
                _context.RollerballPen.Remove(rollerballPen);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RollerballPenExists(int id)
        {
          return (_context.RollerballPen?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
