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
    public class AccessoryController : Controller
    {
        private readonly PenShopContext _context;

        public AccessoryController(PenShopContext context)
        {
            _context = context;
        }

        // GET: Accessory
        public async Task<IActionResult> Index()
        {
              return _context.Accessory != null ? 
                          View(await _context.Accessory.ToListAsync()) :
                          Problem("Entity set 'PenShopContext.Accessory'  is null.");
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
                return RedirectToAction(nameof(NibAccessoryController.Details), nameof(NibAccessory));

            if(accessory is Converter)
                return RedirectToAction(nameof(ConverterController.Details), nameof(Converter));

            if(accessory is Stand)
                return RedirectToAction(nameof(StandController.Details), nameof(Stand));

            throw new InvalidOperationException();
        }

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
                return RedirectToAction(nameof(NibAccessoryController.Edit), nameof(NibAccessory));

            if(accessory is Converter)
                return RedirectToAction(nameof(ConverterController.Edit), nameof(Converter));

            if(accessory is Stand)
                return RedirectToAction(nameof(StandController.Edit), nameof(Stand));

            throw new InvalidOperationException();
        }

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
                return RedirectToAction(nameof(NibAccessoryController.Delete), nameof(NibAccessory));

            if(accessory is Converter)
                return RedirectToAction(nameof(ConverterController.Delete), nameof(Converter));

            if(accessory is Stand)
                return RedirectToAction(nameof(StandController.Delete), nameof(Stand));

            throw new InvalidOperationException();
        }
    }
}
