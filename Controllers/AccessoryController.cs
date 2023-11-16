using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PenShop.Data;
using PenShop.Models;

namespace PenShop.Controllers
{
    public class AccessoryController : Controller
    {
        private readonly PenShopContext _context;
        private readonly NibAccessoryController _nibAccessoryController;
        private readonly ConverterController _converterController;
        private readonly StandController _standController;

        public AccessoryController(PenShopContext context, NibAccessoryController nibAccessoryController, ConverterController converterController, StandController standController)
        {
            _context = context;
            _nibAccessoryController = nibAccessoryController;
            _converterController = converterController;
            _standController = standController;
        }

        // GET: Accessory
        public async Task<IActionResult> Index()
        {
              return _context.Accessory != null ? 
                          View(await _context.Accessory.Select(x => x.Id).ToListAsync()) :
                          Problem("Entity set 'PenShopContext.Accessory' is null.");
        }

        // GET: Accessory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Accessory == null)
            {
                return NotFound();
            }

            var accessory = await _context.Accessory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accessory == null)
            {
                return NotFound();
            }

            if(accessory is NibAccessory)
                return RedirectToAction(nameof(NibAccessoryController.Details), nameof(NibAccessory), new {id = id});

            if(accessory is Converter)
                return RedirectToAction(nameof(ConverterController.Details), nameof(Converter), new {id = id});

            if(accessory is Stand)
                return RedirectToAction(nameof(StandController.Details), nameof(Stand), new {id = id});

            throw new InvalidOperationException();
        }

        [Authorize(Policy = "Customer")]
        // GET: Accessory/Order/5
        public async Task<IActionResult> Order(int? id)
        {
            if (id == null || _context.Accessory == null)
            {
                return NotFound();
            }

            var accessory = await _context.Accessory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accessory == null)
            {
                return NotFound();
            }

            if(accessory is NibAccessory)
                return RedirectToAction(nameof(NibAccessoryController.Order), nameof(NibAccessory), new {id = id});

            if(accessory is Converter)
                return RedirectToAction(nameof(ConverterController.Order), nameof(Converter), new {id = id});

            if(accessory is Stand)
                return RedirectToAction(nameof(StandController.Order), nameof(Stand), new {id = id});

            throw new InvalidOperationException();
        }

        // GET: Accessory/Details/5
        public async Task<IActionResult> ProductCard(int? id)
        {
            if (id == null || _context.Accessory == null)
            {
                return NotFound();
            }

            var accessory = await _context.Accessory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accessory == null)
            {
                return NotFound();
            }

            return await ProductCard(accessory);
        }

        // GET: Accessory/Details/5
        public async Task<IActionResult> ProductCard(Accessory accessory)
        {
            if(accessory is NibAccessory)
                return await _nibAccessoryController.ProductCard((NibAccessory)accessory);

            if(accessory is Converter)
                return await _converterController.ProductCard((Converter)accessory);

            if(accessory is Stand)
                return await _standController.ProductCard((Stand)accessory);

            throw new InvalidOperationException();
        }

        [Authorize(Policy = "Administrator")]
        // GET: Accessory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Accessory == null)
            {
                return NotFound();
            }

            var accessory = await _context.Accessory.FindAsync(id);
            if (accessory == null)
            {
                return NotFound();
            }

            if(accessory is NibAccessory)
                return RedirectToAction(nameof(NibAccessoryController.Edit), nameof(NibAccessory), new {id = id});

            if(accessory is Converter)
                return RedirectToAction(nameof(ConverterController.Edit), nameof(Converter), new {id = id});

            if(accessory is Stand)
                return RedirectToAction(nameof(StandController.Edit), nameof(Stand), new {id = id});

            throw new InvalidOperationException();
        }

        [Authorize(Policy = "Administrator")]
        // GET: Accessory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Accessory == null)
            {
                return NotFound();
            }

            var accessory = await _context.Accessory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accessory == null)
            {
                return NotFound();
            }

            if(accessory is NibAccessory)
                return RedirectToAction(nameof(NibAccessoryController.Delete), nameof(NibAccessory), new {id = id});

            if(accessory is Converter)
                return RedirectToAction(nameof(ConverterController.Delete), nameof(Converter), new {id = id});

            if(accessory is Stand)
                return RedirectToAction(nameof(StandController.Delete), nameof(Stand), new {id = id});

            throw new InvalidOperationException();
        }
    }
}
