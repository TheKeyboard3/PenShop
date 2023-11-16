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
    public class ProductOrderController : Controller
    {
        private readonly PenShopContext _context;

        public ProductOrderController(PenShopContext context)
        {
            _context = context;
        }

        // GET: ProductOrder
        public async Task<IActionResult> Index()
        {
            var penShopContext = _context.ProductOrder.Include(p => p.Customer).Include(p => p.Order);
            var user = GetUser();
            if (user is Administrator)
            {
                return View(await penShopContext.ToListAsync());
            }
            else if (user is Customer)
            {
                return View(await penShopContext.Where(x => x.CustomerId == user.Id || x.Order != null && x.Order.CustomerId == user.Id).ToListAsync());
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

        // GET: ProductOrder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductOrder == null)
            {
                return NotFound();
            }

            var productOrder = await _context.ProductOrder
                .Include(p => p.Customer)
                .Include(p => p.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productOrder == null)
            {
                return NotFound();
            }

            var user = GetUser();
            if (user is null || productOrder.CustomerId != user.Id && productOrder.Order?.CustomerId != user.Id && user is not Administrator)
            {
                return Unauthorized();
            }

            if(productOrder is FountainPenOrder)
                return RedirectToAction(nameof(FountainPenOrderController.Details), nameof(FountainPenOrder), new {id = id});

            if(productOrder is GeneralProductOrder)
                return RedirectToAction(nameof(GeneralProductOrderController.Details), nameof(GeneralProductOrder), new {id = id});

            throw new InvalidOperationException();
        }

        // GET: ProductOrder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductOrder == null)
            {
                return NotFound();
            }

            var productOrder = await _context.ProductOrder.FindAsync(id);
            if (productOrder == null)
            {
                return NotFound();
            }

            var user = GetUser();
            if (user is null || productOrder.CustomerId != user.Id && productOrder.Order?.CustomerId != user.Id && user is not Administrator)
            {
                return Unauthorized();
            }

            if(productOrder is FountainPenOrder)
                return RedirectToAction(nameof(FountainPenOrderController.Edit), nameof(FountainPenOrder), new {id = id});

            if(productOrder is GeneralProductOrder)
                return RedirectToAction(nameof(GeneralProductOrderController.Edit), nameof(GeneralProductOrder), new {id = id});

            throw new InvalidOperationException();
        }

        // GET: ProductOrder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductOrder == null)
            {
                return NotFound();
            }

            var productOrder = await _context.ProductOrder
                .Include(p => p.Customer)
                .Include(p => p.Order)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (productOrder == null)
            {
                return NotFound();
            }

            var user = GetUser();
            if (user is null || productOrder.CustomerId != user.Id && productOrder.Order?.CustomerId != user.Id && user is not Administrator)
            {
                return Unauthorized();
            }

            if(productOrder is FountainPenOrder)
                return RedirectToAction(nameof(FountainPenOrderController.Delete), nameof(FountainPenOrder), new {id = id});

            if(productOrder is GeneralProductOrder)
                return RedirectToAction(nameof(GeneralProductOrderController.Delete), nameof(GeneralProductOrder), new {id = id});

            throw new InvalidOperationException();
        }

        private IdentityUser? GetUser(){
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId is null)
                return null;

            return _context.Users.Find(userId);
        }
    }
}
