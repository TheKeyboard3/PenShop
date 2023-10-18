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
    public class FountainPenOrderController : Controller
    {
        private readonly PenShopContext _context;

        public FountainPenOrderController(PenShopContext context)
        {
            _context = context;
        }

        // GET: FountainPenOrder
        public async Task<IActionResult> Index()
        {
            var penShopContext = _context.FountainPenOrder.Include(f => f.Customer).Include(f => f.Order).Include(f => f.Pen);
            return View(await penShopContext.ToListAsync());
        }

        // GET: FountainPenOrder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FountainPenOrder == null)
            {
                return NotFound();
            }

            var fountainPenOrder = await _context.FountainPenOrder
                .Include(f => f.Customer)
                .Include(f => f.Order)
                .Include(f => f.Pen)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fountainPenOrder == null)
            {
                return NotFound();
            }

            return View(fountainPenOrder);
        }

        // GET: FountainPenOrder/Create/penId
        public IActionResult Create(int penId)
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, nameof(Customer.Id), nameof(Customer.FullName));
            var pen = _context.FountainPen.Where(x => x.Id == penId).FirstOrDefault();
            if (pen is null)
                return NotFound();

            ViewData["PenId"] = pen.Id;
            ViewData["PenName"] = pen.Name;

            return View();
        }

        // POST: FountainPenOrder/Create/penId
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int penId, [Bind("RemoveNib,CustomerId,Quantity")] FountainPenOrder fountainPenOrder)
        {
            fountainPenOrder.PenId = penId;
            if (ModelState.IsValid)
            {
                _context.Add(fountainPenOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, nameof(Customer.Id), nameof(Customer.FullName), fountainPenOrder.CustomerId);
            var pen = _context.FountainPen.Where(x => x.Id == penId).FirstOrDefault();
            if (pen is null)
                return NotFound();

            ViewData["PenId"] = pen.Id;
            ViewData["PenName"] = pen.Name;

            return View(fountainPenOrder);
        }

        // GET: FountainPenOrder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FountainPenOrder == null)
            {
                return NotFound();
            }

            var fountainPenOrder = await _context.FountainPenOrder.FindAsync(id);
            if (fountainPenOrder == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, nameof(Customer.Id), nameof(Customer.FullName), fountainPenOrder.CustomerId);
            ViewData["OrderId"] = new SelectList(_context.Order, nameof(Order.Id), nameof(Order.Text), fountainPenOrder.OrderId);
            ViewData["PenId"] = new SelectList(_context.FountainPen, nameof(FountainPen.Id), nameof(FountainPen.Name), fountainPenOrder.PenId);
            return View(fountainPenOrder);
        }

        // POST: FountainPenOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PenId,RemoveNib,Id,CustomerId,OrderId,Quantity")] FountainPenOrder fountainPenOrder)
        {
            if (id != fountainPenOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fountainPenOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FountainPenOrderExists(fountainPenOrder.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, nameof(Customer.Id), nameof(Customer.FullName), fountainPenOrder.CustomerId);
            ViewData["OrderId"] = new SelectList(_context.Order, nameof(Order.Id), nameof(Order.Text), fountainPenOrder.OrderId);
            ViewData["PenId"] = new SelectList(_context.FountainPen, nameof(FountainPen.Id), nameof(FountainPen.Name), fountainPenOrder.PenId);
            return View(fountainPenOrder);
        }

        // GET: FountainPenOrder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FountainPenOrder == null)
            {
                return NotFound();
            }

            var fountainPenOrder = await _context.FountainPenOrder
                .Include(f => f.Customer)
                .Include(f => f.Order)
                .Include(f => f.Pen)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fountainPenOrder == null)
            {
                return NotFound();
            }

            return View(fountainPenOrder);
        }

        // POST: FountainPenOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FountainPenOrder == null)
            {
                return Problem("Entity set 'PenShopContext.FountainPenOrder'  is null.");
            }
            var fountainPenOrder = await _context.FountainPenOrder.FindAsync(id);
            if (fountainPenOrder != null)
            {
                _context.FountainPenOrder.Remove(fountainPenOrder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FountainPenOrderExists(int id)
        {
          return (_context.FountainPenOrder?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
