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
    public class PenController : Controller
    {
        private readonly PenShopContext _context;

        public PenController(PenShopContext context)
        {
            _context = context;
        }

        // GET: Pen
        public async Task<IActionResult> Index()
        {
            var penShopContext = _context.Pen.Include(p => p.CartridgeStandard).Include(p => p.Material);
            return View(await penShopContext.ToListAsync());
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

        // GET: Pen/Edit/5
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
