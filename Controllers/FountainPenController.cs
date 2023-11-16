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
    public class FountainPenController : BaseProductController
    {
        private readonly PenShopContext _context;

        public FountainPenController(PenShopContext context, IWebHostEnvironment webHostEnvironment)
            : base (webHostEnvironment)
        {
            _context = context;
        }

        // GET: FountainPen
        public async Task<IActionResult> Index()
        {
            return View(await _context.FountainPen.Select(x => x.Id).ToListAsync());
        }

        // GET: FountainPen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FountainPen == null)
            {
                return NotFound();
            }

            var fountainPen = await _context.FountainPen
                .Include(f => f.CartridgeStandard)
                .Include(f => f.Material)
                .Include(f => f.Nib)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fountainPen == null)
            {
                return NotFound();
            }

            return View(fountainPen);
        }

        // GET: FountainPen/Order/5
        [Authorize(Policy = "Customer")]
        public async Task<IActionResult> Order(int? id)
        {
            if (id == null || _context.FountainPen == null)
            {
                return NotFound();
            }

            var fountainPen = await _context.FountainPen
                .Include(f => f.CartridgeStandard)
                .Include(f => f.Material)
                .Include(f => f.Nib)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fountainPen == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(FountainPenOrderController.Create), nameof(FountainPenOrder), new {penId = id});
        }

        // GET: FountainPen/ProductCard/5
        public async Task<IActionResult> ProductCard(int? id)
        {
            if (id == null || _context.FountainPen == null)
            {
                return NotFound();
            }

            var fountainPen = await _context.FountainPen
                .Include(f => f.CartridgeStandard)
                .Include(f => f.Material)
                .Include(f => f.Nib)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fountainPen == null)
            {
                return NotFound();
            }

            return await ProductCard(fountainPen);
        }

        // GET: FountainPen/ProductCard/5
        public Task<IActionResult> ProductCard(FountainPen fountainPen)
        {
            return Task.FromResult<IActionResult>(PartialView("/Views/FountainPen/ProductCard.cshtml", fountainPen));
        }

        // GET: FountainPen/Create
        [Authorize(Policy = "Administrator")]
        public IActionResult Create()
        {
            ViewData["CartridgeStandardId"] = new SelectList(_context.CartridgeStandard, nameof(CartridgeStandard.Id), nameof(CartridgeStandard.Name));
            ViewData["MaterialId"] = new SelectList(_context.Material, nameof(Material.Id), nameof(Material.Name));
            ViewData["NibId"] = new SelectList(_context.Nib, nameof(Nib.Id), nameof(Nib.Name));
            return View();
        }

        // POST: FountainPen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Create([Bind("NibId,CartridgeStandardId,MaterialId,Id,Price,Name,Description,ImageFile")] FountainPen fountainPen)
        {
            if (ModelState.IsValid)
            {
                string? uniqueFileName = UploadedFile(fountainPen.ImageFile);
                fountainPen.ImageName = uniqueFileName;
                _context.Add(fountainPen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartridgeStandardId"] = new SelectList(_context.CartridgeStandard, nameof(CartridgeStandard.Id), nameof(CartridgeStandard.Name), fountainPen.CartridgeStandardId);
            ViewData["MaterialId"] = new SelectList(_context.Material, nameof(Material.Id), nameof(Material.Name), fountainPen.MaterialId);
            ViewData["NibId"] = new SelectList(_context.Nib, nameof(Nib.Id), nameof(Nib.Name), fountainPen.NibId);
            return View(fountainPen);
        }

        // GET: FountainPen/Edit/5
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FountainPen == null)
            {
                return NotFound();
            }

            var fountainPen = await _context.FountainPen.FindAsync(id);
            if (fountainPen == null)
            {
                return NotFound();
            }
            ViewData["CartridgeStandardId"] = new SelectList(_context.CartridgeStandard, nameof(CartridgeStandard.Id), nameof(CartridgeStandard.Name), fountainPen.CartridgeStandardId);
            ViewData["MaterialId"] = new SelectList(_context.Material, nameof(Material.Id), nameof(Material.Name), fountainPen.MaterialId);
            ViewData["NibId"] = new SelectList(_context.Nib, nameof(Nib.Id), nameof(Nib.Name), fountainPen.NibId);
            return View(fountainPen);
        }

        // POST: FountainPen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("NibId,CartridgeStandardId,MaterialId,Id,Price,Name,Description,ImageFile")] FountainPen fountainPen)
        {
            if (id != fountainPen.Id)
            {
                return NotFound();
            }

            var oldFountainPen = _context.FountainPen.Find(id);
            if(oldFountainPen is null)
                return NotFound();

            oldFountainPen.NibId = fountainPen.NibId;
            oldFountainPen.CartridgeStandardId = fountainPen.CartridgeStandardId;
            oldFountainPen.MaterialId = fountainPen.MaterialId;
            oldFountainPen.Price = fountainPen.Price;
            oldFountainPen.Name = fountainPen.Name;
            oldFountainPen.Description = fountainPen.Description;

            if (ModelState.IsValid)
            {
                try
                {
                    string? uniqueFileName = UploadedFile(fountainPen.ImageFile);
                    oldFountainPen.ImageName = uniqueFileName ?? oldFountainPen!.ImageName;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FountainPenExists(fountainPen.Id))
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
            ViewData["CartridgeStandardId"] = new SelectList(_context.CartridgeStandard, nameof(CartridgeStandard.Id), nameof(CartridgeStandard.Name), fountainPen.CartridgeStandardId);
            ViewData["MaterialId"] = new SelectList(_context.Material, nameof(Material.Id), nameof(Material.Name), fountainPen.MaterialId);
            ViewData["NibId"] = new SelectList(_context.Nib, nameof(Nib.Id), nameof(Nib.Name), fountainPen.NibId);
            return View(fountainPen);
        }

        // GET: FountainPen/Delete/5
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FountainPen == null)
            {
                return NotFound();
            }

            var fountainPen = await _context.FountainPen
                .Include(f => f.CartridgeStandard)
                .Include(f => f.Material)
                .Include(f => f.Nib)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fountainPen == null)
            {
                return NotFound();
            }

            return View(fountainPen);
        }

        // POST: FountainPen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FountainPen == null)
            {
                return Problem("Entity set 'PenShopContext.FountainPen'  is null.");
            }
            var fountainPen = await _context.FountainPen.FindAsync(id);
            if (fountainPen != null)
            {
                _context.FountainPen.Remove(fountainPen);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FountainPenExists(int id)
        {
          return (_context.FountainPen?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
