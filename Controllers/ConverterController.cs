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
    public class ConverterController : Controller
    {
        private readonly PenShopContext _context;

        public ConverterController(PenShopContext context)
        {
            _context = context;
        }

        // GET: Converter
        public async Task<IActionResult> Index()
        {
            var penShopContext = _context.Converter.Include(c => c.CartridgeStandard);
            return View(await penShopContext.ToListAsync());
        }

        // GET: Converter/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Converter == null)
            {
                return NotFound();
            }

            var converter = await _context.Converter
                .Include(c => c.CartridgeStandard)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (converter == null)
            {
                return NotFound();
            }

            return View(converter);
        }

        // GET: Converter/Create
        public IActionResult Create()
        {
            ViewData["CartridgeStandardId"] = new SelectList(_context.CartridgeStandard, nameof(CartridgeStandard.Id), nameof(CartridgeStandard.Name));
            return View();
        }

        // POST: Converter/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Height,Capacity,CartridgeStandardId,Id,Price,Name,Description")] Converter converter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(converter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartridgeStandardId"] = new SelectList(_context.CartridgeStandard, nameof(CartridgeStandard.Id), nameof(CartridgeStandard.Name), converter.CartridgeStandardId);
            return View(converter);
        }

        // GET: Converter/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Converter == null)
            {
                return NotFound();
            }

            var converter = await _context.Converter.FindAsync(id);
            if (converter == null)
            {
                return NotFound();
            }
            ViewData["CartridgeStandardId"] = new SelectList(_context.CartridgeStandard, nameof(CartridgeStandard.Id), nameof(CartridgeStandard.Name), converter.CartridgeStandardId);
            return View(converter);
        }

        // POST: Converter/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Height,Capacity,CartridgeStandardId,Id,Price,Name,Description")] Converter converter)
        {
            if (id != converter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(converter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConverterExists(converter.Id))
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
            ViewData["CartridgeStandardId"] = new SelectList(_context.CartridgeStandard, nameof(CartridgeStandard.Id), nameof(CartridgeStandard.Name), converter.CartridgeStandardId);
            return View(converter);
        }

        // GET: Converter/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Converter == null)
            {
                return NotFound();
            }

            var converter = await _context.Converter
                .Include(c => c.CartridgeStandard)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (converter == null)
            {
                return NotFound();
            }

            return View(converter);
        }

        // POST: Converter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Converter == null)
            {
                return Problem("Entity set 'PenShopContext.Converter'  is null.");
            }
            var converter = await _context.Converter.FindAsync(id);
            if (converter != null)
            {
                _context.Converter.Remove(converter);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConverterExists(int id)
        {
          return (_context.Converter?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
