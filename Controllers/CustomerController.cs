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
    [Authorize(Policy = "Administrator")]
    public class CustomerController : Controller
    {
        private readonly PenShopContext _context;

        public CustomerController(PenShopContext context)
        {
            _context = context;
        }

        // GET: Customer
        public async Task<IActionResult> Index()
        {
            var user = GetUser();
            if (user is Administrator)
            {
                return _context.Customer != null ?
                    View(await _context.Customer.ToListAsync()) :
                    Problem("Entity set 'PenShopContext.Customer'  is null.");
            }
            else if (user is Customer)
            {
                return View(new Customer[] {(Customer)user});
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

        // GET: Customer/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var user = GetUser();
            if (user is null || user.Id != id && user is not Administrator)
            {
                return Unauthorized();
            }

            if (id == null || _context.Customer == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var user = GetUser();
            if (user is null || user.Id != id && user is not Administrator)
            {
                return Unauthorized();
            }

            if (id == null || _context.Customer == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Surname,DefaultShippingAddress,LockoutEnd,LockoutEnabled")] Customer customer)
        {
            var user = GetUser();
            if (user is null || user.Id != id && user is not Administrator)
            {
                return Unauthorized();
            }

            if (id != customer.Id)
                return NotFound();

            var oldCustomer = _context.Customer.Find(id);

            if(oldCustomer is null)
                return NotFound();

            oldCustomer.ConcurrencyStamp =  ((char)(customer.ConcurrencyStamp![0] + 1))+customer.ConcurrencyStamp!.Substring(1);
            oldCustomer.Name = customer.Name;
            oldCustomer.Surname = customer.Surname;
            oldCustomer.DefaultShippingAddress = customer.DefaultShippingAddress;
            if (user is Administrator){
                oldCustomer.LockoutEnd = customer.LockoutEnd;
                oldCustomer.LockoutEnabled = customer.LockoutEnabled;
            }

            if (ModelState.IsValid)
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var user = GetUser();
            if (user is null || user.Id != id && user is not Administrator)
            {
                return Unauthorized();
            }

            if (id == null || _context.Customer == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = GetUser();
            if (user is null || user.Id != id && user is not Administrator)
            {
                return Unauthorized();
            }

            if (_context.Customer == null)
            {
                return Problem("Entity set 'PenShopContext.Customer'  is null.");
            }
            var customer = await _context.Customer.FindAsync(id);
            if (customer != null)
            {
                _context.Customer.Remove(customer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(string id)
        {
            return (_context.Customer?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private IdentityUser? GetUser(){
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId is null)
                return null;

            return _context.Users.Find(userId);
        }
    }
}
