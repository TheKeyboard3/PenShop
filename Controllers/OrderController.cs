using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
    public class OrderController : Controller
    {
        private readonly PenShopContext _context;

        public OrderController(PenShopContext context)
        {
            _context = context;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            var penShopContext = _context.Order.Include(o => o.Customer);
            var user = GetUser();
            if (user is Administrator)
            {
                return View(await penShopContext.ToListAsync());
            }
            else if (user is Customer)
            {
                return View(await penShopContext.Where(x => x.CustomerId == user.Id).ToListAsync());
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

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            var user = GetUser();
            if (user is null || order.CustomerId != user.Id && user is not Administrator)
            {
                return Unauthorized();
            }

            return View(order);
        }

        // GET: Order/Create
        public IActionResult Create()
        {
            var user = GetUser();
            var customers = user is Customer
                ? new Customer[] {(Customer)user}
                : _context.Customer.AsEnumerable();

            ViewData["CustomerId"] = new SelectList(customers, nameof(Customer.Id), nameof(Customer.FullName));
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,Date,ShippingAddress")] Order order)
        {
            var user = GetUser();
            if (user is null || order.CustomerId != user.Id && user is not Administrator)
            {
                return Unauthorized();
            }

            Customer? orderCustomer = _context.Customer.Where(x => x.Id == order.CustomerId).FirstOrDefault();
            if(orderCustomer is null)
                return NotFound();

            if (ModelState.IsValid)
            {
                order.ProductOrders = new List<ProductOrder>(orderCustomer.ShoppingCart!);
                orderCustomer.ShoppingCart!.Clear();
                if(string.IsNullOrEmpty(order.ShippingAddress))
                    order.ShippingAddress = orderCustomer.DefaultShippingAddress;

                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var customers = user is Customer
                ? new Customer[] {(Customer)user}
                : _context.Customer.AsEnumerable();

            ViewData["CustomerId"] = new SelectList(customers, nameof(Customer.Id), nameof(Customer.FullName));
            return View(order);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            var user = GetUser();
            if (user is null || order.CustomerId != user.Id && user is not Administrator)
            {
                return Unauthorized();
            }

            var customers = user is Customer
                ? new Customer[] {(Customer)user}
                : _context.Customer.AsEnumerable();

            ViewData["CustomerId"] = new SelectList(customers, nameof(Customer.Id), nameof(Customer.FullName), order.CustomerId);
            return View(order);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,Date,ShippingAddress")] Order order)
        {
            var user = GetUser();
            if (user is null)
                return Unauthorized();

            var customers = user is Customer
                ? new Customer[] {(Customer)user}
                : _context.Customer.AsEnumerable();

            if (!customers.Any(x => x.Id == order.CustomerId))
            {
                return Unauthorized();
            }

            if (id != order.Id)
            {
                return NotFound();
            }

            var oldOrder = _context.Order.Find(id);
            if(oldOrder is null)
                return NotFound();

            oldOrder.CustomerId = order.CustomerId;
            oldOrder.Date = order.Date;
            oldOrder.ShippingAddress = order.ShippingAddress;

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["CustomerId"] = new SelectList(customers, nameof(Customer.Id), nameof(Customer.FullName), order.CustomerId);
            return View(order);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            var user = GetUser();
            if (user is null || order.CustomerId != user.Id && user is not Administrator)
            {
                return Unauthorized();
            }

            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Order == null)
            {
                return Problem("Entity set 'PenShopContext.Order'  is null.");
            }
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                var user = GetUser();
                if (user is null || order.CustomerId != user.Id && user is not Administrator)
                {
                    return Unauthorized();
                }

                _context.Order.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Order?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private IdentityUser? GetUser(){
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId is null)
                return null;

            return _context.Users.Find(userId);
        }
    }
}
