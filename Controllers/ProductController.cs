using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
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
        private readonly PenController _penController;
        private readonly InkController _inkController;
        private readonly AccessoryController _accessoryController;

        public ProductController(PenShopContext context, PenController penController, InkController inkController, AccessoryController accessoryController)
        {
            _context = context;
            _penController = penController;
            _inkController = inkController;
            _accessoryController = accessoryController;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
              return _context.Product != null ? 
                          View(await _context.Product.Select(x => x.Id).ToListAsync()) :
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

        // GET: Product/Order/5
        public async Task<IActionResult> Order(int? id)
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
                return RedirectToAction(nameof(PenController.Order), nameof(Pen), new {id = id});

            if(product is Ink)
                return RedirectToAction(nameof(InkController.Order), nameof(Ink), new {id = id});

            if(product is Accessory)
                return RedirectToAction(nameof(AccessoryController.Order), nameof(Accessory), new {id = id});

            throw new InvalidOperationException();
        }

        // GET: Product/Details/5
        public async Task<IActionResult> ProductCard(int? id)
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

            return await ProductCard(product);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> ProductCard(Product product)
        {
            if(product is Pen)
                return await _penController.ProductCard((Pen)product);

            if(product is Ink)
                return await _inkController.ProductCard((Ink)product);

            if(product is Accessory)
                return await _accessoryController.ProductCard((Accessory)product);

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
