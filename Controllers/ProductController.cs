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
    public class ProductController : Controller
    {
        private readonly PenShopContext _context;

        public ProductController(PenShopContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
              return _context.Product != null ? 
                          View(await _context.Product.ToListAsync()) :
                          Problem("Entity set 'PenShopContext.Product'  is null.");
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            if(product is Pen)
                return RedirectToAction(nameof(PenController.Details), nameof(Pen), new {id = id});

            if(product is Ink)
                return RedirectToAction(nameof(InkController.Details), nameof(Ink), new {id = id});

            if(product is Accessory)
                return RedirectToAction(nameof(AccessoryController.Details), nameof(Accessory), new {id = id});

            throw new InvalidOperationException();
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            if(product is Pen)
                return RedirectToAction(nameof(PenController.Edit), nameof(Pen), new {id = id});

            if(product is Ink)
                return RedirectToAction(nameof(InkController.Edit), nameof(Ink), new {id = id});

            if(product is Accessory)
                return RedirectToAction(nameof(AccessoryController.Edit), nameof(Accessory), new {id = id});

            throw new InvalidOperationException();
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            if(product is Pen)
                return RedirectToAction(nameof(PenController.Delete), nameof(Pen), new {id = id});

            if(product is Ink)
                return RedirectToAction(nameof(InkController.Delete), nameof(Ink), new {id = id});

            if(product is Accessory)
                return RedirectToAction(nameof(AccessoryController.Delete), nameof(Accessory), new {id = id});

            throw new InvalidOperationException();
        }
    }
}
