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
    public class InkCartridgeController : BaseProductController
    {
        private readonly PenShopContext _context;

        public InkCartridgeController(PenShopContext context, IWebHostEnvironment webHostEnvironment)
            : base (webHostEnvironment)
        {
            _context = context;
        }

        // GET: InkCartridge
        public async Task<IActionResult> Index()
        {
            return View(await _context.InkCartridge.Select(x => x.Id).ToListAsync());
        }

        // GET: InkCartridge/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InkCartridge == null)
            {
                return NotFound();
            }

            var inkCartridge = await _context.InkCartridge
                .Include(i => i.Colour)
                .Include(i => i.CartridgeStandard)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inkCartridge == null)
            {
                return NotFound();
            }

            return View(inkCartridge);
        }

        // GET: InkCartridge/Order/5
        [Authorize(Policy = "Customer")]
        public async Task<IActionResult> Order(int? id)
        {
            if (id == null || _context.InkCartridge == null)
            {
                return NotFound();
            }

            var inkCartridge = await _context.InkCartridge
                .Include(i => i.Colour)
                .Include(i => i.CartridgeStandard)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inkCartridge == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(GeneralProductOrderController.Create), nameof(GeneralProductOrder), new {productId = id});
        }

        // GET: InkCartridge/ProductCard/5
        public async Task<IActionResult> ProductCard(int? id)
        {
            if (id == null || _context.InkCartridge == null)
            {
                return NotFound();
            }

            var inkCartridge = await _context.InkCartridge
                .Include(i => i.Colour)
                .Include(i => i.CartridgeStandard)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inkCartridge == null)
            {
                return NotFound();
            }

            return await ProductCard(inkCartridge);
        }

        // GET: InkCartridge/ProductCard/5
        public Task<IActionResult> ProductCard(InkCartridge inkCartridge)
        {
            return Task.FromResult<IActionResult>(PartialView("/Views/InkCartridge/ProductCard.cshtml", inkCartridge));
        }

        // GET: InkCartridge/Create
        [Authorize(Policy = "Administrator")]
        public IActionResult Create()
        {
            ViewData["ColourId"] = new SelectList(_context.InkColour, nameof(InkColour.Id), nameof(InkColour.Name));
            ViewData["CartridgeStandardId"] = new SelectList(_context.CartridgeStandard, nameof(CartridgeStandard.Id), nameof(CartridgeStandard.Name));
            return View();
        }

        // POST: InkCartridge/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Create([Bind("CartridgeStandardId,Capacity,ColourId,Id,Price,Name,Description,ImageFile")] InkCartridge inkCartridge)
        {
            if (ModelState.IsValid)
            {
                string? uniqueFileName = UploadedFile(inkCartridge.ImageFile);
                inkCartridge.ImageName = uniqueFileName;
                _context.Add(inkCartridge);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ColourId"] = new SelectList(_context.InkColour, nameof(InkColour.Id), nameof(InkColour.Name), inkCartridge.ColourId);
            ViewData["CartridgeStandardId"] = new SelectList(_context.CartridgeStandard, nameof(CartridgeStandard.Id), nameof(CartridgeStandard.Name), inkCartridge.CartridgeStandardId);
            return View(inkCartridge);
        }

        // GET: InkCartridge/Edit/5
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InkCartridge == null)
            {
                return NotFound();
            }

            var inkCartridge = await _context.InkCartridge.FindAsync(id);
            if (inkCartridge == null)
            {
                return NotFound();
            }
            ViewData["ColourId"] = new SelectList(_context.InkColour, nameof(InkColour.Id), nameof(InkColour.Name), inkCartridge.ColourId);
            ViewData["CartridgeStandardId"] = new SelectList(_context.CartridgeStandard, nameof(CartridgeStandard.Id), nameof(CartridgeStandard.Name), inkCartridge.CartridgeStandardId);
            return View(inkCartridge);
        }

        // POST: InkCartridge/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("CartridgeStandardId,Capacity,ColourId,Id,Price,Name,Description,ImageFile")] InkCartridge inkCartridge)
        {
            if (id != inkCartridge.Id)
            {
                return NotFound();
            }

            var oldInkCartridge = _context.InkCartridge.Find(id);
            if(oldInkCartridge is null)
                return NotFound();

            oldInkCartridge.CartridgeStandardId = inkCartridge.CartridgeStandardId;
            oldInkCartridge.Capacity = inkCartridge.Capacity;
            oldInkCartridge.ColourId = inkCartridge.ColourId;
            oldInkCartridge.Price = inkCartridge.Price;
            oldInkCartridge.Name = inkCartridge.Name;
            oldInkCartridge.Description = inkCartridge.Description;

            if (ModelState.IsValid)
            {
                try
                {
                    string? uniqueFileName = UploadedFile(inkCartridge.ImageFile);
                    oldInkCartridge.ImageName = uniqueFileName ?? oldInkCartridge!.ImageName;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InkCartridgeExists(inkCartridge.Id))
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
            ViewData["ColourId"] = new SelectList(_context.InkColour, nameof(InkColour.Id), nameof(InkColour.Name), inkCartridge.ColourId);
            ViewData["CartridgeStandardId"] = new SelectList(_context.CartridgeStandard, nameof(CartridgeStandard.Id), nameof(CartridgeStandard.Name), inkCartridge.CartridgeStandardId);
            return View(inkCartridge);
        }

        // GET: InkCartridge/Delete/5
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InkCartridge == null)
            {
                return NotFound();
            }

            var inkCartridge = await _context.InkCartridge
                .Include(i => i.Colour)
                .Include(i => i.CartridgeStandard)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inkCartridge == null)
            {
                return NotFound();
            }

            return View(inkCartridge);
        }

        // POST: InkCartridge/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InkCartridge == null)
            {
                return Problem("Entity set 'PenShopContext.InkCartridge'  is null.");
            }
            var inkCartridge = await _context.InkCartridge.FindAsync(id);
            if (inkCartridge != null)
            {
                _context.InkCartridge.Remove(inkCartridge);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InkCartridgeExists(int id)
        {
          return (_context.InkCartridge?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
