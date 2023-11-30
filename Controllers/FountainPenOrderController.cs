using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PenShop.Data;
using PenShop.Models;

namespace PenShop.Controllers
{
    [Authorize]
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
            var user = GetUser();
            if (user is Administrator)
            {
                return View(await penShopContext.ToListAsync());
            }
            else if (user is Customer)
            {
                return View("ShoppingCart", await penShopContext.Where(x => x.CustomerId == user.Id).ToListAsync());
            }
            else if (user is null)
            {
                return Unauthorized();
            }
            else
            {
                return Problem("Unknown user");
            }
        }

        // GET: FountainPenOrder/ForOrder/5
        public async Task<IActionResult> ForOrder(int? id)
        {
            var penShopContext = _context.FountainPenOrder.Include(p => p.Customer).Include(p => p.Order).Include(g => g.Pen);
            if(id is null)
                return NotFound();

            var order = _context.Order.Find(id);
            if(order is null)
                return NotFound();

            var user = GetUser();
            if(user is null)
                return NotFound();

            if (user is not Administrator && order.CustomerId != user.Id)
            {
                return Unauthorized();
            }
            else
            {
                ViewData["OrderId"] = id;
                return View(await penShopContext.Where(x => x.OrderId == order.Id).ToListAsync());
            }
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

            var user = GetUser();
            if (user is null || fountainPenOrder.CustomerId != user.Id && fountainPenOrder.Order?.CustomerId != user.Id && user is not Administrator)
            {
                return Unauthorized();
            }

            return View(fountainPenOrder);
        }

        // GET: FountainPenOrder/Create/penId
        public IActionResult Create(int penId)
        {
            var pen = _context.FountainPen.Where(x => x.Id == penId).FirstOrDefault();
            if (pen is null)
                return NotFound();

            var user = GetUser();
            var customers = user is Customer
                ? new Customer[] {(Customer)user}
                : _context.Customer.AsEnumerable();

            ViewData["CustomerId"] = new SelectList(customers, nameof(Customer.Id), nameof(Customer.FullName));
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
            var user = GetUser();
            if (user is null || fountainPenOrder.CustomerId != user.Id && user is not Administrator)
            {
                return Unauthorized();
            }

            fountainPenOrder.PenId = penId;

            if (ModelState.IsValid)
            {
                _context.Add(fountainPenOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var pen = _context.FountainPen.Where(x => x.Id == penId).FirstOrDefault();
            if (pen is null)
                return NotFound();

            var customers = user is Customer
                ? new Customer[] {(Customer)user}
                : _context.Customer.AsEnumerable();

            ViewData["CustomerId"] = new SelectList(customers, nameof(Customer.Id), nameof(Customer.FullName), fountainPenOrder.CustomerId);
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

            var user = GetUser();
            if (user is null || fountainPenOrder.CustomerId != user.Id && fountainPenOrder.Order?.CustomerId != user.Id && user is not Administrator)
            {
                return Unauthorized();
            }

            var customers = user is Customer
                ? new Customer[] {(Customer)user}
                : _context.Customer.AsEnumerable();

            var orders = user is Customer
                ? _context.Order.Where(x => x.CustomerId == user.Id)
                : _context.Order.AsEnumerable();

            ViewData["CustomerId"] = new SelectList(customers, nameof(Customer.Id), nameof(Customer.FullName), fountainPenOrder.CustomerId);
            ViewData["OrderId"] = new SelectList(orders, nameof(Order.Id), nameof(Order.Text), fountainPenOrder.OrderId);
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
            var user = GetUser();
            if (user is null)
                return Unauthorized();

            var customers = user is Customer
                ? new Customer[] {(Customer)user}
                : _context.Customer.AsEnumerable();

            var orders = user is Customer
                ? _context.Order.Where(x => x.CustomerId == user.Id)
                : _context.Order.AsEnumerable();

            if (!customers.Any(x => x.Id == fountainPenOrder.CustomerId) && !orders.Any(x => x.Id == fountainPenOrder.OrderId))
            {
                return Unauthorized();
            }

            if (id != fountainPenOrder.Id)
            {
                return NotFound();
            }

            var oldFountainPenOrder = _context.FountainPenOrder
                .Include(f => f.Customer)
                .Include(f => f.Order)
                .Include(f => f.Pen)
                .FirstOrDefault(x => x.Id == id);
            if(oldFountainPenOrder is null)
                return NotFound();

            oldFountainPenOrder.PenId = fountainPenOrder.PenId;
            oldFountainPenOrder.RemoveNib = fountainPenOrder.RemoveNib;
            oldFountainPenOrder.CustomerId = fountainPenOrder.CustomerId;
            oldFountainPenOrder.OrderId = fountainPenOrder.OrderId;
            oldFountainPenOrder.Quantity = fountainPenOrder.Quantity;

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(fountainPenOrder);
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
            ViewData["CustomerId"] = new SelectList(customers, nameof(Customer.Id), nameof(Customer.FullName), fountainPenOrder.CustomerId);
            ViewData["OrderId"] = new SelectList(orders, nameof(Order.Id), nameof(Order.Text), fountainPenOrder.OrderId);
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

            var user = GetUser();
            if (user is null || fountainPenOrder.CustomerId != user.Id && fountainPenOrder.Order?.CustomerId != user.Id && user is not Administrator)
            {
                return Unauthorized();
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
                var user = GetUser();
                if (user is null || fountainPenOrder.CustomerId != user.Id && fountainPenOrder.Order?.CustomerId != user.Id && user is not Administrator)
                {
                    return Unauthorized();
                }

                _context.FountainPenOrder.Remove(fountainPenOrder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FountainPenOrderExists(int id)
        {
            return (_context.FountainPenOrder?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private IdentityUser? GetUser(){
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId is null)
                return null;

            return _context.Users.AsNoTracking().FirstOrDefault(x => x.Id == userId);
        }
    }
}
