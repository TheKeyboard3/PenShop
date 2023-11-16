using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PenShop.Data;
using PenShop.Models;

namespace PenShop.Controllers
{
    [Authorize(Policy = "Administrator")]
    public class InkColourController : Controller
    {
        private readonly PenShopContext _context;

        public InkColourController(PenShopContext context)
        {
            _context = context;
        }

        // GET: InkColour
        public async Task<IActionResult> Index()
        {
              return _context.InkColour != null ? 
                          View(await _context.InkColour.ToListAsync()) :
                          Problem("Entity set 'PenShopContext.InkColour'  is null.");
        }

        // GET: InkColour/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InkColour == null)
            {
                return NotFound();
            }

            var inkColour = await _context.InkColour
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inkColour == null)
            {
                return NotFound();
            }

            return View(inkColour);
        }

        // GET: InkColour/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InkColour/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] InkColour inkColour)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inkColour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inkColour);
        }

        // GET: InkColour/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InkColour == null)
            {
                return NotFound();
            }

            var inkColour = await _context.InkColour.FindAsync(id);
            if (inkColour == null)
            {
                return NotFound();
            }
            return View(inkColour);
        }

        // POST: InkColour/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] InkColour inkColour)
        {
            if (id != inkColour.Id)
            {
                return NotFound();
            }

            var oldInkColour = _context.InkColour.Find(id);
            if(oldInkColour is null)
                return NotFound();

            oldInkColour.Name = inkColour.Name;

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InkColourExists(inkColour.Id))
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
            return View(inkColour);
        }

        // GET: InkColour/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InkColour == null)
            {
                return NotFound();
            }

            var inkColour = await _context.InkColour
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inkColour == null)
            {
                return NotFound();
            }

            return View(inkColour);
        }

        // POST: InkColour/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InkColour == null)
            {
                return Problem("Entity set 'PenShopContext.InkColour'  is null.");
            }
            var inkColour = await _context.InkColour.FindAsync(id);
            if (inkColour != null)
            {
                _context.InkColour.Remove(inkColour);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InkColourExists(int id)
        {
          return (_context.InkColour?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
