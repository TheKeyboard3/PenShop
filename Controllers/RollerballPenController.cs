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
    public class RollerballPenController : BaseProductController
    {
        private readonly PenShopContext _context;

        public RollerballPenController(PenShopContext context, IWebHostEnvironment webHostEnvironment)
            : base (webHostEnvironment)
        {
            _context = context;
        }

        // GET: RollerballPen
        public async Task<IActionResult> Index()
        {
            if(_context.RollerballPen is null)
                return Problem("Entity set 'PenShopContext.RollerballPen' is null.");

            ViewData["Products"] = await _context.RollerballPen.Select(x => x.Id).ToListAsync();
            ViewData["CartridgeStandardId"] = new SelectList(_context.CartridgeStandard, nameof(CartridgeStandard.Id), nameof(CartridgeStandard.Name));
            return View(new PenFilters());
        }

        // POST: RollerballPen
        [HttpPost]
        public IActionResult Index(PenFilters penFilters)
        {
            if(_context.RollerballPen is null)
                return Problem("Entity set 'PenShopContext.RollerballPen' is null.");

            ViewData["CartridgeStandardId"] = new SelectList(_context.CartridgeStandard, nameof(CartridgeStandard.Id), nameof(CartridgeStandard.Name));
            if (ModelState.IsValid)
                ViewData["Products"] = _context.RollerballPen.AsEnumerable().Where(x => penFilters.MatchPen(x)).Select(x => x.Id).ToList();
            else
                ViewData["Products"] = new List<int>();

            return View(penFilters);
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

        // GET: RollerballPen/Order/5
        [Authorize(Policy = "Customer")]
        public async Task<IActionResult> Order(int? id)
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

            return RedirectToAction(nameof(GeneralProductOrderController.Create), nameof(GeneralProductOrder), new {productId = id});
        }

        // GET: RollerballPen/ProductCard/5
        public async Task<IActionResult> ProductCard(int? id)
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

            return await ProductCard(rollerballPen);
        }

        // GET: RollerballPen/ProductCard/5
        public Task<IActionResult> ProductCard(RollerballPen rollerballPen)
        {
            return Task.FromResult<IActionResult>(PartialView("/Views/RollerballPen/ProductCard.cshtml", rollerballPen));
        }

        // GET: RollerballPen/Create
        [Authorize(Policy = "Administrator")]
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
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Create([Bind("RollerballDiameter,CartridgeStandardId,MaterialId,Id,Price,Name,Description,ImageFile")] RollerballPen rollerballPen)
        {
            if (ModelState.IsValid)
            {
                string? uniqueFileName = UploadedFile(rollerballPen.ImageFile);
                rollerballPen.ImageName = uniqueFileName;
                _context.Add(rollerballPen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartridgeStandardId"] = new SelectList(_context.CartridgeStandard, nameof(CartridgeStandard.Id), nameof(CartridgeStandard.Name), rollerballPen.CartridgeStandardId);
            ViewData["MaterialId"] = new SelectList(_context.Material, nameof(Material.Id), nameof(Material.Name), rollerballPen.MaterialId);
            return View(rollerballPen);
        }

        // GET: RollerballPen/Edit/5
        [Authorize(Policy = "Administrator")]
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
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("RollerballDiameter,CartridgeStandardId,MaterialId,Id,Price,Name,Description,ImageFile")] RollerballPen rollerballPen)
        {
            if (id != rollerballPen.Id)
            {
                return NotFound();
            }

            var oldRollerballPen = _context.RollerballPen.Find(id);
            if(oldRollerballPen is null)
                return NotFound();

            oldRollerballPen.RollerballDiameter = rollerballPen.RollerballDiameter;
            oldRollerballPen.CartridgeStandardId = rollerballPen.CartridgeStandardId;
            oldRollerballPen.MaterialId = rollerballPen.MaterialId;
            oldRollerballPen.Price = rollerballPen.Price;
            oldRollerballPen.Name = rollerballPen.Name;
            oldRollerballPen.Description = rollerballPen.Description;

            if (ModelState.IsValid)
            {
                try
                {
                    string? uniqueFileName = UploadedFile(rollerballPen.ImageFile);
                    oldRollerballPen.ImageName = uniqueFileName ?? oldRollerballPen!.ImageName;
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
        [Authorize(Policy = "Administrator")]
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
        [Authorize(Policy = "Administrator")]
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
