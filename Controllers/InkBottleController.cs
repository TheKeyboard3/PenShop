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
    public class InkBottleController : BaseProductController
    {
        private readonly PenShopContext _context;

        public InkBottleController(PenShopContext context, IWebHostEnvironment webHostEnvironment)
            : base (webHostEnvironment)
        {
            _context = context;
        }

        // GET: InkBottle
        public async Task<IActionResult> Index()
        {
            return View(await _context.InkBottle.Select(x => x.Id).ToListAsync());
        }

        // GET: InkBottle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InkBottle == null)
            {
                return NotFound();
            }

            var inkBottle = await _context.InkBottle
                .Include(i => i.Colour)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inkBottle == null)
            {
                return NotFound();
            }

            return View(inkBottle);
        }

        // GET: InkBottle/ProductCard/5
        public async Task<IActionResult> ProductCard(int? id)
        {
            if (id == null || _context.InkBottle == null)
            {
                return NotFound();
            }

            var inkBottle = await _context.InkBottle
                .Include(i => i.Colour)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inkBottle == null)
            {
                return NotFound();
            }

            return await ProductCard(inkBottle);
        }

        // GET: InkBottle/ProductCard/5
        public Task<IActionResult> ProductCard(InkBottle inkBottle)
        {
            return Task.FromResult<IActionResult>(PartialView("/Views/InkBottle/ProductCard.cshtml", inkBottle));
        }

        // GET: InkBottle/Create
        public IActionResult Create()
        {
            ViewData["ColourId"] = new SelectList(_context.InkColour, nameof(InkColour.Id), nameof(InkColour.Name));
            return View();
        }

        // POST: InkBottle/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Capacity,ColourId,Id,Price,Name,Description,ImageFile")] InkBottle inkBottle)
        {
            if (ModelState.IsValid)
            {
                string? uniqueFileName = UploadedFile(inkBottle.ImageFile);
                inkBottle.ImageName = uniqueFileName;
                _context.Add(inkBottle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ColourId"] = new SelectList(_context.InkColour, nameof(InkColour.Id), nameof(InkColour.Name), inkBottle.ColourId);
            return View(inkBottle);
        }

        // GET: InkBottle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InkBottle == null)
            {
                return NotFound();
            }

            var inkBottle = await _context.InkBottle.FindAsync(id);
            if (inkBottle == null)
            {
                return NotFound();
            }
            ViewData["ColourId"] = new SelectList(_context.InkColour, nameof(InkColour.Id), nameof(InkColour.Name), inkBottle.ColourId);
            return View(inkBottle);
        }

        // POST: InkBottle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Capacity,ColourId,Id,Price,Name,Description,ImageFile")] InkBottle inkBottle)
        {
            if (id != inkBottle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string? uniqueFileName = UploadedFile(inkBottle.ImageFile);
                    inkBottle.ImageName = uniqueFileName ?? inkBottle.ImageName;
                    _context.Update(inkBottle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InkBottleExists(inkBottle.Id))
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
            ViewData["ColourId"] = new SelectList(_context.InkColour, nameof(InkColour.Id), nameof(InkColour.Name), inkBottle.ColourId);
            return View(inkBottle);
        }

        // GET: InkBottle/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InkBottle == null)
            {
                return NotFound();
            }

            var inkBottle = await _context.InkBottle
                .Include(i => i.Colour)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inkBottle == null)
            {
                return NotFound();
            }

            return View(inkBottle);
        }

        // POST: InkBottle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InkBottle == null)
            {
                return Problem("Entity set 'PenShopContext.InkBottle'  is null.");
            }
            var inkBottle = await _context.InkBottle.FindAsync(id);
            if (inkBottle != null)
            {
                _context.InkBottle.Remove(inkBottle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InkBottleExists(int id)
        {
          return (_context.InkBottle?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
