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
    public class NibAccessoryController : Controller
    {
        private readonly PenShopContext _context;

        public NibAccessoryController(PenShopContext context)
        {
            _context = context;
        }

        // GET: NibAccessory
        public async Task<IActionResult> Index()
        {
            var penShopContext = _context.NibAccessory.Include(n => n.Nib);
            return View(await penShopContext.ToListAsync());
        }

        // GET: NibAccessory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NibAccessory == null)
            {
                return NotFound();
            }

            var nibAccessory = await _context.NibAccessory
                .Include(n => n.Nib)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nibAccessory == null)
            {
                return NotFound();
            }

            return View(nibAccessory);
        }

        // GET: NibAccessory/Create
        public IActionResult Create()
        {
            ViewData["NibId"] = new SelectList(_context.Nib, nameof(Nib.Id), nameof(Nib.Name));
            return View();
        }

        // POST: NibAccessory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NibId,Price,Id,Name,Description")] NibAccessory nibAccessory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nibAccessory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NibId"] = new SelectList(_context.Nib, nameof(Nib.Id), nameof(Nib.Name), nibAccessory.NibId);
            return View(nibAccessory);
        }

        // GET: NibAccessory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NibAccessory == null)
            {
                return NotFound();
            }

            var nibAccessory = await _context.NibAccessory.FindAsync(id);
            if (nibAccessory == null)
            {
                return NotFound();
            }
            ViewData["NibId"] = new SelectList(_context.Nib, nameof(Nib.Id), nameof(Nib.Name), nibAccessory.NibId);
            return View(nibAccessory);
        }

        // POST: NibAccessory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NibId,Price,Id,Name,Description")] NibAccessory nibAccessory)
        {
            if (id != nibAccessory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nibAccessory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NibAccessoryExists(nibAccessory.Id))
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
            ViewData["NibId"] = new SelectList(_context.Nib, nameof(Nib.Id), nameof(Nib.Name), nibAccessory.NibId);
            return View(nibAccessory);
        }

        // GET: NibAccessory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NibAccessory == null)
            {
                return NotFound();
            }

            var nibAccessory = await _context.NibAccessory
                .Include(n => n.Nib)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nibAccessory == null)
            {
                return NotFound();
            }

            return View(nibAccessory);
        }

        // POST: NibAccessory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NibAccessory == null)
            {
                return Problem("Entity set 'PenShopContext.NibAccessory'  is null.");
            }
            var nibAccessory = await _context.NibAccessory.FindAsync(id);
            if (nibAccessory != null)
            {
                _context.NibAccessory.Remove(nibAccessory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NibAccessoryExists(int id)
        {
          return (_context.NibAccessory?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
