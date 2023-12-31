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
    public class ConverterController : BaseProductController
    {
        private readonly PenShopContext _context;

        public ConverterController(PenShopContext context, IWebHostEnvironment webHostEnvironment)
            : base (webHostEnvironment)
        {
            _context = context;
        }

        // GET: Converter
        public async Task<IActionResult> Index()
        {
            if(_context.Converter is null)
                return Problem("Entity set 'PenShopContext.Converter' is null.");

            ViewData["CartridgeStandardId"] = new SelectList(_context.CartridgeStandard, nameof(CartridgeStandard.Id), nameof(CartridgeStandard.Name));
            ViewData["Products"] = await _context.Converter.Select(x => x.Id).ToListAsync();
            return View(new ConverterFilters());
        }

        // POST: Converter
        [HttpPost]
        public IActionResult Index(ConverterFilters converterFilters)
        {
            if(_context.Converter is null)
                return Problem("Entity set 'PenShopContext.Converter' is null.");

            ViewData["CartridgeStandardId"] = new SelectList(_context.CartridgeStandard, nameof(CartridgeStandard.Id), nameof(CartridgeStandard.Name));
            if (ModelState.IsValid)
                ViewData["Products"] = _context.Converter.AsEnumerable().Where(x => converterFilters.MatchConverter(x)).Select(x => x.Id).ToList();
            else
                ViewData["Products"] = new List<int>();

            return View(converterFilters);
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

        // GET: Converter/Order/5
        [Authorize(Policy = "Customer")]
        public async Task<IActionResult> Order(int? id)
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

            return RedirectToAction(nameof(GeneralProductOrderController.Create), nameof(GeneralProductOrder), new {productId = id});
        }

        // GET: Converter/ProductCard/5
        public async Task<IActionResult> ProductCard(int? id)
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

            return await ProductCard(converter);
        }

        // GET: Converter/ProductCard/5
        public Task<IActionResult> ProductCard(Converter converter)
        {
            return Task.FromResult<IActionResult>(PartialView("/Views/Converter/ProductCard.cshtml", converter));
        }

        [Authorize(Policy = "Administrator")]
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
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Create([Bind("Height,Capacity,CartridgeStandardId,Id,Price,Name,Description,ImageFile")] Converter converter)
        {
            if (ModelState.IsValid)
            {
                string? uniqueFileName = UploadedFile(converter.ImageFile);
                converter.ImageName = uniqueFileName;
                _context.Add(converter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartridgeStandardId"] = new SelectList(_context.CartridgeStandard, nameof(CartridgeStandard.Id), nameof(CartridgeStandard.Name), converter.CartridgeStandardId);
            return View(converter);
        }

        // GET: Converter/Edit/5
        [Authorize(Policy = "Administrator")]
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
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("Height,Capacity,CartridgeStandardId,Id,Price,Name,Description,ImageFile")] Converter converter)
        {
            if (id != converter.Id)
            {
                return NotFound();
            }

            var oldConverter = _context.Converter.Find(id);
            if(oldConverter is null)
                return NotFound();

            oldConverter.Height = converter.Height;
            oldConverter.Capacity = converter.Capacity;
            oldConverter.CartridgeStandardId = converter.CartridgeStandardId;
            oldConverter.Price = converter.Price;
            oldConverter.Name = converter.Name;
            oldConverter.Description = converter.Description;

            if (ModelState.IsValid)
            {
                try
                {
                    string? uniqueFileName = UploadedFile(converter.ImageFile);
                    oldConverter.ImageName = uniqueFileName ?? oldConverter.ImageName;
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
        [Authorize(Policy = "Administrator")]
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
        [Authorize(Policy = "Administrator")]
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
