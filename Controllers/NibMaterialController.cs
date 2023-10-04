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
    public class NibMaterialController : Controller
    {
        private readonly PenShopContext _context;

        public NibMaterialController(PenShopContext context)
        {
            _context = context;
        }

        // GET: NibMaterial
        public async Task<IActionResult> Index()
        {
              return _context.NibMaterial != null ? 
                          View(await _context.NibMaterial.ToListAsync()) :
                          Problem("Entity set 'PenShopContext.NibMaterial'  is null.");
        }

        // GET: NibMaterial/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NibMaterial == null)
            {
                return NotFound();
            }

            var nibMaterial = await _context.NibMaterial
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nibMaterial == null)
            {
                return NotFound();
            }

            return View(nibMaterial);
        }

        // GET: NibMaterial/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NibMaterial/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Hardness")] NibMaterial nibMaterial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nibMaterial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nibMaterial);
        }

        // GET: NibMaterial/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NibMaterial == null)
            {
                return NotFound();
            }

            var nibMaterial = await _context.NibMaterial.FindAsync(id);
            if (nibMaterial == null)
            {
                return NotFound();
            }
            return View(nibMaterial);
        }

        // POST: NibMaterial/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Hardness")] NibMaterial nibMaterial)
        {
            if (id != nibMaterial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nibMaterial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NibMaterialExists(nibMaterial.Id))
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
            return View(nibMaterial);
        }

        // GET: NibMaterial/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NibMaterial == null)
            {
                return NotFound();
            }

            var nibMaterial = await _context.NibMaterial
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nibMaterial == null)
            {
                return NotFound();
            }

            return View(nibMaterial);
        }

        // POST: NibMaterial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NibMaterial == null)
            {
                return Problem("Entity set 'PenShopContext.NibMaterial'  is null.");
            }
            var nibMaterial = await _context.NibMaterial.FindAsync(id);
            if (nibMaterial != null)
            {
                _context.NibMaterial.Remove(nibMaterial);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NibMaterialExists(int id)
        {
          return (_context.NibMaterial?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
