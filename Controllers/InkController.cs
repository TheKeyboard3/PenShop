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
    public class InkController : Controller
    {
        private readonly PenShopContext _context;
        private readonly InkBottleController _inkBottleController;
        private readonly InkCartridgeController _inkCartridgeController;

        public InkController(PenShopContext context, InkBottleController inkBottleController, InkCartridgeController inkCartridgeController)
        {
            _context = context;
            _inkBottleController = inkBottleController;
            _inkCartridgeController = inkCartridgeController;
        }

        // GET: Ink
        public async Task<IActionResult> Index()
        {
            if(_context.Ink is null)
                return Problem("Entity set 'PenShopContext.Ink' is null.");

            ViewData["Products"] = await _context.Ink.Select(x => x.Id).ToListAsync();
            return View(new InkFilters());
        }

        // POST: Ink
        [HttpPost]
        public IActionResult Index(InkFilters inkFilters)
        {
            if(_context.Ink is null)
                return Problem("Entity set 'PenShopContext.Ink' is null.");

            if (ModelState.IsValid)
                ViewData["Products"] = _context.Ink.AsEnumerable().Where(x => inkFilters.MatchInk(x)).Select(x => x.Id).ToList();
            else
                ViewData["Products"] = new List<int>();

            return View(inkFilters);
        }

        // GET: Ink/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ink == null)
            {
                return NotFound();
            }

            var ink = await _context.Ink
                .Include(i => i.Colour)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ink == null)
            {
                return NotFound();
            }

            if(ink is InkCartridge)
                return RedirectToAction(nameof(InkCartridgeController.Details), nameof(InkCartridge), new {id = id});

            if(ink is InkBottle)
                return RedirectToAction(nameof(InkBottleController.Details), nameof(InkBottle), new {id = id});

            throw new InvalidOperationException();
        }

        // GET: Ink/Order/5
        [Authorize(Policy = "Customer")]
        public async Task<IActionResult> Order(int? id)
        {
            if (id == null || _context.Ink == null)
            {
                return NotFound();
            }

            var ink = await _context.Ink
                .Include(i => i.Colour)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ink == null)
            {
                return NotFound();
            }

            if(ink is InkCartridge)
                return RedirectToAction(nameof(InkCartridgeController.Order), nameof(InkCartridge), new {id = id});

            if(ink is InkBottle)
                return RedirectToAction(nameof(InkBottleController.Order), nameof(InkBottle), new {id = id});

            throw new InvalidOperationException();
        }

        // GET: Ink/Details/5
        public async Task<IActionResult> ProductCard(int? id)
        {
            if (id == null || _context.Ink == null)
            {
                return NotFound();
            }

            var ink = await _context.Ink
                .Include(i => i.Colour)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ink == null)
            {
                return NotFound();
            }

            return await ProductCard(ink);
        }

        // GET: Ink/Details/5
        public async Task<IActionResult> ProductCard(Ink ink)
        {
            if(ink is InkCartridge)
                return await _inkCartridgeController.ProductCard((InkCartridge)ink);

            if(ink is InkBottle)
                return await _inkBottleController.ProductCard((InkBottle)ink);

            throw new InvalidOperationException();
        }

        // GET: Ink/Edit/5
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ink == null)
            {
                return NotFound();
            }

            var ink = await _context.Ink.FindAsync(id);
            if (ink == null)
            {
                return NotFound();
            }

            if(ink is InkCartridge)
                return RedirectToAction(nameof(InkCartridgeController.Edit), nameof(InkCartridge), new {id = id});

            if(ink is InkBottle)
                return RedirectToAction(nameof(InkBottleController.Edit), nameof(InkBottle), new {id = id});

            throw new InvalidOperationException();
        }

        // GET: Ink/Delete/5
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ink == null)
            {
                return NotFound();
            }

            var ink = await _context.Ink
                .Include(i => i.Colour)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ink == null)
            {
                return NotFound();
            }

            if(ink is InkCartridge)
                return RedirectToAction(nameof(InkCartridgeController.Delete), nameof(InkCartridge), new {id = id});

            if(ink is InkBottle)
                return RedirectToAction(nameof(InkBottleController.Delete), nameof(InkBottle), new {id = id});

            throw new InvalidOperationException();
        }
    }
}
