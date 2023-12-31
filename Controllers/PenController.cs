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
    public class PenController : Controller
    {
        private readonly PenShopContext _context;
        private readonly FountainPenController _fountainPenController;
        private readonly RollerballPenController _rollerballPenController;

        public PenController(PenShopContext context, FountainPenController fountainPenController, RollerballPenController rollerballPenController)
        {
            _context = context;
            _fountainPenController = fountainPenController;
            _rollerballPenController = rollerballPenController;
        }

        // GET: Pen
        public async Task<IActionResult> Index()
        {
            if(_context.Pen is null)
                return Problem("Entity set 'PenShopContext.Pen' is null.");

            ViewData["Products"] = await _context.Pen.Select(x => x.Id).ToListAsync();
            ViewData["CartridgeStandardId"] = new SelectList(_context.CartridgeStandard, nameof(CartridgeStandard.Id), nameof(CartridgeStandard.Name));
            return View(new PenFilters());
        }

        // POST: Pen
        [HttpPost]
        public IActionResult Index(PenFilters penFilters)
        {
            if(_context.Pen is null)
                return Problem("Entity set 'PenShopContext.Pen' is null.");

            ViewData["CartridgeStandardId"] = new SelectList(_context.CartridgeStandard, nameof(CartridgeStandard.Id), nameof(CartridgeStandard.Name));
            if (ModelState.IsValid)
                ViewData["Products"] = _context.Pen.AsEnumerable().Where(x => penFilters.MatchPen(x)).Select(x => x.Id).ToList();
            else
                ViewData["Products"] = new List<int>();

            return View(penFilters);
        }

        // GET: Pen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pen == null)
            {
                return NotFound();
            }

            var pen = await _context.Pen
                .Include(p => p.CartridgeStandard)
                .Include(p => p.Material)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pen == null)
            {
                return NotFound();
            }

            if(pen is FountainPen)
                return RedirectToAction(nameof(FountainPenController.Details), nameof(FountainPen), new {id = id});

            if(pen is RollerballPen)
                return RedirectToAction(nameof(RollerballPenController.Details), nameof(RollerballPen), new {id = id});

            throw new InvalidOperationException();
        }

        // GET: Pen/Order/5
        [Authorize(Policy = "Customer")]
        public async Task<IActionResult> Order(int? id)
        {
            if (id == null || _context.Pen == null)
            {
                return NotFound();
            }

            var pen = await _context.Pen
                .Include(p => p.CartridgeStandard)
                .Include(p => p.Material)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pen == null)
            {
                return NotFound();
            }

            if(pen is FountainPen)
                return RedirectToAction(nameof(FountainPenController.Order), nameof(FountainPen), new {id = id});

            if(pen is RollerballPen)
                return RedirectToAction(nameof(RollerballPenController.Order), nameof(RollerballPen), new {id = id});

            throw new InvalidOperationException();
        }

        // GET: Pen/Details/5
        public async Task<IActionResult> ProductCard(int? id)
        {
            if (id == null || _context.Pen == null)
            {
                return NotFound();
            }

            var pen = await _context.Pen
                .Include(p => p.CartridgeStandard)
                .Include(p => p.Material)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pen == null)
            {
                return NotFound();
            }

            return await ProductCard(pen);
        }

        // GET: Pen/Details/5
        public async Task<IActionResult> ProductCard(Pen pen)
        {
            if(pen is FountainPen)
                return await _fountainPenController.ProductCard((FountainPen)pen);

            if(pen is RollerballPen)
                return await _rollerballPenController.ProductCard((RollerballPen)pen);

            throw new InvalidOperationException();
        }

        // GET: Pen/Edit/5
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pen == null)
            {
                return NotFound();
            }

            var pen = await _context.Pen.FindAsync(id);
            if (pen == null)
            {
                return NotFound();
            }

            if(pen is FountainPen)
                return RedirectToAction(nameof(FountainPenController.Edit), nameof(FountainPen), new {id = id});

            if(pen is RollerballPen)
                return RedirectToAction(nameof(RollerballPenController.Edit), nameof(RollerballPen), new {id = id});

            throw new InvalidOperationException();
        }

        // GET: Pen/Delete/5
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pen == null)
            {
                return NotFound();
            }

            var pen = await _context.Pen
                .Include(p => p.CartridgeStandard)
                .Include(p => p.Material)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pen == null)
            {
                return NotFound();
            }

            if(pen is FountainPen)
                return RedirectToAction(nameof(FountainPenController.Delete), nameof(FountainPen), new {id = id});

            if(pen is RollerballPen)
                return RedirectToAction(nameof(RollerballPenController.Delete), nameof(RollerballPen), new {id = id});

            throw new InvalidOperationException();
        }
    }
}
