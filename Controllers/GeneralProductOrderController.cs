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

        // GET: GeneralProductOrder/ForOrder/5
        public async Task<IActionResult> ForOrder(int? id)
        {
            var penShopContext = _context.GeneralProductOrder.Include(p => p.Customer).Include(p => p.Order).Include(g => g.GeneralProduct);
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

            var user = GetUser();
            if (user is null || generalProductOrder.CustomerId != user.Id && generalProductOrder.Order?.CustomerId != user.Id && user is not Administrator)
            {
                return Unauthorized();
            }

            return View(generalProductOrder);
        }

        // GET: GeneralProductOrder/Create/productId
        public IActionResult Create(int productId)
        {
            var product = _context.Product.Where(x => x.Id == productId).FirstOrDefault();
            if (product is null || product is FountainPen)
                return NotFound();

            var user = GetUser();
            var customers = user is Customer
                ? new Customer[] {(Customer)user}
                : _context.Customer.AsEnumerable();

            ViewData["CustomerId"] = new SelectList(customers, nameof(Customer.Id), nameof(Customer.FullName));
            ViewData["ProductId"] = product.Id;
            ViewData["ProductName"] = product.Name;

            return View();
        }

        // POST: GeneralProductOrder/Create/productId
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int productId, [Bind("Id,CustomerId,Quantity")] GeneralProductOrder generalProductOrder)
        {
            var user = GetUser();
            if (user is null || generalProductOrder.CustomerId != user.Id && user is not Administrator)
            {
                return Unauthorized();
            }

            generalProductOrder.GeneralProductId = productId;
            if (ModelState.IsValid)
            {
                _context.Add(generalProductOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var product = _context.Product.Where(x => x.Id == productId).FirstOrDefault();
            if (product is null || product is FountainPen)
                return NotFound();

            var customers = user is Customer
                ? new Customer[] {(Customer)user}
                : _context.Customer.AsEnumerable();

            ViewData["CustomerId"] = new SelectList(customers, nameof(Customer.Id), nameof(Customer.FullName), generalProductOrder.CustomerId);
            ViewData["ProductId"] = product.Id;
            ViewData["ProductName"] = product.Name;

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

            var user = GetUser();
            if (user is null || user.Id != generalProductOrder.CustomerId && generalProductOrder.Order?.CustomerId != user.Id && user is not Administrator)
            {
                return Unauthorized();
            }

            var customers = user is Customer
                ? new Customer[] {(Customer)user}
                : _context.Customer.AsEnumerable();

            var orders = user is Customer
                ? _context.Order.Where(x => x.CustomerId == user.Id)
                : _context.Order.AsEnumerable();

            ViewData["CustomerId"] = new SelectList(customers, nameof(Customer.Id), nameof(Customer.FullName), generalProductOrder.CustomerId);
            ViewData["OrderId"] = new SelectList(orders, nameof(Order.Id), nameof(Order.Text), generalProductOrder.OrderId);
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
            var user = GetUser();
            if (user is null)
                return Unauthorized();

            var customers = user is Customer
                ? new Customer[] {(Customer)user}
                : _context.Customer.AsEnumerable();

            var orders = user is Customer
                ? _context.Order.Where(x => x.CustomerId == user.Id)
                : _context.Order.AsEnumerable();

            if (!customers.Any(x => x.Id == generalProductOrder.CustomerId) && !orders.Any(x => x.Id == generalProductOrder.OrderId))
            {
                return Unauthorized();
            }

            if (id != generalProductOrder.Id)
            {
                return NotFound();
            }

            var oldGeneralProductOrder = _context.GeneralProductOrder
                .Include(g => g.Customer)
                .Include(g => g.Order)
                .Include(g => g.GeneralProduct)
                .FirstOrDefault(x => x.Id == id);

            if(oldGeneralProductOrder is null)
                return NotFound();

            oldGeneralProductOrder.GeneralProductId = generalProductOrder.GeneralProductId;
            oldGeneralProductOrder.CustomerId = generalProductOrder.CustomerId;
            oldGeneralProductOrder.OrderId = generalProductOrder.OrderId;
            oldGeneralProductOrder.Quantity = generalProductOrder.Quantity;

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(generalProductOrder);
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
            ViewData["CustomerId"] = new SelectList(customers, nameof(Customer.Id), nameof(Customer.FullName), generalProductOrder.CustomerId);
            ViewData["OrderId"] = new SelectList(orders, nameof(Order.Id), nameof(Order.Text), generalProductOrder.OrderId);
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

            var user = GetUser();
            if (user is null || generalProductOrder.CustomerId != user.Id && generalProductOrder.Order?.CustomerId != user.Id && user is not Administrator)
            {
                return Unauthorized();
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
                var user = GetUser();
                if (user is null || generalProductOrder.CustomerId != user.Id && generalProductOrder.Order?.CustomerId != user.Id && user is not Administrator)
                {
                    return Unauthorized();
                }

                _context.GeneralProductOrder.Remove(generalProductOrder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GeneralProductOrderExists(int id)
        {
          return (_context.GeneralProductOrder?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private IdentityUser? GetUser(){
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId is null)
                return null;

            return _context.Users.AsNoTracking().FirstOrDefault(x => x.Id == userId);
        }
    }
}
