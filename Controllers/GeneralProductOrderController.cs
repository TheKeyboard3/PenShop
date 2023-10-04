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
    public class GeneralProductOrderController : Controller
    {
        private readonly PenShopContext _context;

        public GeneralProductOrderController(PenShopContext context)
        {
            _context = context;
        }

        // GET: GeneralProductOrder
        public async Task<IActionResult> Index()
        {
            var penShopContext = _context.GeneralProductOrder.Include(g => g.Customer).Include(g => g.Order).Include(g => g.GeneralProduct);
            return View(await penShopContext.ToListAsync());
        }

        // GET: GeneralProductOrder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GeneralProductOrder == null)
            {
                return NotFound();
            }

            var generalProductOrder = await _context.GeneralProductOrder
                .Include(g => g.Customer)
                .Include(g => g.Order)
                .Include(g => g.GeneralProduct)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (generalProductOrder == null)
            {
                return NotFound();
            }

            return View(generalProductOrder);
        }

        // GET: GeneralProductOrder/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, nameof(Customer.Id), nameof(Customer.FullName));
            ViewData["OrderId"] = new SelectList(_context.Order, nameof(Order.Id), nameof(Order.Text));
            ViewData["GeneralProductId"] = new SelectList(_context.Product.AsEnumerable().Select(x => x is FountainPen ? null : x).Where(x => x != null), nameof(Product.Id), nameof(Product.Name));
            return View();
        }

        // POST: GeneralProductOrder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GeneralProductId,Id,CustomerId,OrderId,Quantity")] GeneralProductOrder generalProductOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(generalProductOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, nameof(Customer.Id), nameof(Customer.FullName), generalProductOrder.CustomerId);
            ViewData["OrderId"] = new SelectList(_context.Order, nameof(Order.Id), nameof(Order.Text), generalProductOrder.OrderId);
            ViewData["GeneralProductId"] = new SelectList(_context.Product.AsEnumerable().Select(x => x is FountainPen ? null : x).Where(x => x != null), nameof(Product.Id), nameof(Product.Name), generalProductOrder.GeneralProductId);
            return View(generalProductOrder);
        }

        // GET: GeneralProductOrder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GeneralProductOrder == null)
            {
                return NotFound();
            }

            var generalProductOrder = await _context.GeneralProductOrder.FindAsync(id);
            if (generalProductOrder == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, nameof(Customer.Id), nameof(Customer.FullName), generalProductOrder.CustomerId);
            ViewData["OrderId"] = new SelectList(_context.Order, nameof(Order.Id), nameof(Order.Text), generalProductOrder.OrderId);
            ViewData["GeneralProductId"] = new SelectList(_context.Product.AsEnumerable().Select(x => x is FountainPen ? null : x).Where(x => x != null), nameof(Product.Id), nameof(Product.Name), generalProductOrder.GeneralProductId);
            return View(generalProductOrder);
        }

        // POST: GeneralProductOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GeneralProductId,Id,CustomerId,OrderId,Quantity")] GeneralProductOrder generalProductOrder)
        {
            if (id != generalProductOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(generalProductOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GeneralProductOrderExists(generalProductOrder.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, nameof(Customer.Id), nameof(Customer.FullName), generalProductOrder.CustomerId);
            ViewData["OrderId"] = new SelectList(_context.Order, nameof(Order.Id), nameof(Order.Text), generalProductOrder.OrderId);
            ViewData["GeneralProductId"] = new SelectList(_context.Product.AsEnumerable().Select(x => x is FountainPen ? null : x).Where(x => x != null), nameof(Product.Id), nameof(Product.Name), generalProductOrder.GeneralProductId);
            return View(generalProductOrder);
        }

        // GET: GeneralProductOrder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GeneralProductOrder == null)
            {
                return NotFound();
            }

            var generalProductOrder = await _context.GeneralProductOrder
                .Include(g => g.Customer)
                .Include(g => g.Order)
                .Include(g => g.GeneralProduct)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (generalProductOrder == null)
            {
                return NotFound();
            }

            return View(generalProductOrder);
        }

        // POST: GeneralProductOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GeneralProductOrder == null)
            {
                return Problem("Entity set 'PenShopContext.GeneralProductOrder'  is null.");
            }
            var generalProductOrder = await _context.GeneralProductOrder.FindAsync(id);
            if (generalProductOrder != null)
            {
                _context.GeneralProductOrder.Remove(generalProductOrder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GeneralProductOrderExists(int id)
        {
          return (_context.GeneralProductOrder?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
