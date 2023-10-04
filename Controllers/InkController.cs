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
    public class InkController : Controller
    {
        private readonly PenShopContext _context;

        public InkController(PenShopContext context)
        {
            _context = context;
        }

        // GET: Ink
        public async Task<IActionResult> Index()
        {
            var penShopContext = _context.Ink.Include(i => i.Colour);
            return View(await penShopContext.ToListAsync());
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

        // GET: Ink/Edit/5
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
