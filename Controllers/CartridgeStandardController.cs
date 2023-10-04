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
    public class CartridgeStandardController : Controller
    {
        private readonly PenShopContext _context;

        public CartridgeStandardController(PenShopContext context)
        {
            _context = context;
        }

        // GET: CartridgeStandard
        public async Task<IActionResult> Index()
        {
              return _context.CartridgeStandard != null ? 
                          View(await _context.CartridgeStandard.ToListAsync()) :
                          Problem("Entity set 'PenShopContext.CartridgeStandard'  is null.");
        }

        // GET: CartridgeStandard/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CartridgeStandard == null)
            {
                return NotFound();
            }

            var cartridgeStandard = await _context.CartridgeStandard
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartridgeStandard == null)
            {
                return NotFound();
            }

            return View(cartridgeStandard);
        }

        // GET: CartridgeStandard/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CartridgeStandard/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CartridgeStandard cartridgeStandard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartridgeStandard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cartridgeStandard);
        }

        // GET: CartridgeStandard/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CartridgeStandard == null)
            {
                return NotFound();
            }

            var cartridgeStandard = await _context.CartridgeStandard.FindAsync(id);
            if (cartridgeStandard == null)
            {
                return NotFound();
            }
            return View(cartridgeStandard);
        }

        // POST: CartridgeStandard/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CartridgeStandard cartridgeStandard)
        {
            if (id != cartridgeStandard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartridgeStandard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartridgeStandardExists(cartridgeStandard.Id))
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
            return View(cartridgeStandard);
        }

        // GET: CartridgeStandard/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CartridgeStandard == null)
            {
                return NotFound();
            }

            var cartridgeStandard = await _context.CartridgeStandard
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartridgeStandard == null)
            {
                return NotFound();
            }

            return View(cartridgeStandard);
        }

        // POST: CartridgeStandard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CartridgeStandard == null)
            {
                return Problem("Entity set 'PenShopContext.CartridgeStandard'  is null.");
            }
            var cartridgeStandard = await _context.CartridgeStandard.FindAsync(id);
            if (cartridgeStandard != null)
            {
                _context.CartridgeStandard.Remove(cartridgeStandard);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartridgeStandardExists(int id)
        {
          return (_context.CartridgeStandard?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
