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
    public class NibController : Controller
    {
        private readonly PenShopContext _context;

        public NibController(PenShopContext context)
        {
            _context = context;
        }

        // GET: Nib
        public async Task<IActionResult> Index()
        {
            var penShopContext = _context.Nib.Include(n => n.BodyMaterial).Include(n => n.TipMaterial);
            return View(await penShopContext.ToListAsync());
        }

        // GET: Nib/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Nib == null)
            {
                return NotFound();
            }

            var nib = await _context.Nib
                .Include(n => n.BodyMaterial)
                .Include(n => n.TipMaterial)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nib == null)
            {
                return NotFound();
            }

            return View(nib);
        }

        // GET: Nib/Create
        public IActionResult Create()
        {
            ViewData["BodyMaterialId"] = new SelectList(_context.NibMaterial, nameof(NibMaterial.Id), nameof(NibMaterial.Name));
            ViewData["TipMaterialId"] = new SelectList(_context.NibMaterial, nameof(NibMaterial.Id), nameof(NibMaterial.Name));
            return View();
        }

        // POST: Nib/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BodyMaterialId,TipMaterialId,TipDiameter,Price")] Nib nib)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nib);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BodyMaterialId"] = new SelectList(_context.NibMaterial, nameof(NibMaterial.Id), nameof(NibMaterial.Name), nib.BodyMaterialId);
            ViewData["TipMaterialId"] = new SelectList(_context.NibMaterial, nameof(NibMaterial.Id), nameof(NibMaterial.Name), nib.TipMaterialId);
            return View(nib);
        }

        // GET: Nib/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Nib == null)
            {
                return NotFound();
            }

            var nib = await _context.Nib.FindAsync(id);
            if (nib == null)
            {
                return NotFound();
            }
            ViewData["BodyMaterialId"] = new SelectList(_context.NibMaterial, nameof(NibMaterial.Id), nameof(NibMaterial.Name), nib.BodyMaterialId);
            ViewData["TipMaterialId"] = new SelectList(_context.NibMaterial, nameof(NibMaterial.Id), nameof(NibMaterial.Name), nib.TipMaterialId);
            return View(nib);
        }

        // POST: Nib/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BodyMaterialId,TipMaterialId,TipDiameter,Price")] Nib nib)
        {
            if (id != nib.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nib);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NibExists(nib.Id))
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
            ViewData["BodyMaterialId"] = new SelectList(_context.NibMaterial, nameof(NibMaterial.Id), nameof(NibMaterial.Name), nib.BodyMaterialId);
            ViewData["TipMaterialId"] = new SelectList(_context.NibMaterial, nameof(NibMaterial.Id), nameof(NibMaterial.Name), nib.TipMaterialId);
            return View(nib);
        }

        // GET: Nib/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Nib == null)
            {
                return NotFound();
            }

            var nib = await _context.Nib
                .Include(n => n.BodyMaterial)
                .Include(n => n.TipMaterial)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nib == null)
            {
                return NotFound();
            }

            return View(nib);
        }

        // POST: Nib/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Nib == null)
            {
                return Problem("Entity set 'PenShopContext.Nib'  is null.");
            }
            var nib = await _context.Nib.FindAsync(id);
            if (nib != null)
            {
                _context.Nib.Remove(nib);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NibExists(int id)
        {
          return (_context.Nib?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
