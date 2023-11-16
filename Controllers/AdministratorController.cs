using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PenShop.Data;
using PenShop.Models;
using PenShop.Areas.Identity.Pages.Account;

namespace PenShop.Controllers
{
    [Authorize(Policy = "Administrator")]
    public class AdministratorController : Controller
    {
        private readonly PenShopContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;

        public AdministratorController(PenShopContext context,
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;
            _emailStore = GetEmailStore();
        }

        // GET: Administrator
        public async Task<IActionResult> Index()
        {
              return _context.Administrator != null ? 
                          View(await _context.Administrator.ToListAsync()) :
                          Problem("Entity set 'PenShopContext.Administrator'  is null.");
        }

        // GET: Administrator/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Administrator == null)
            {
                return NotFound();
            }

            var administrator = await _context.Administrator
                .FirstOrDefaultAsync(m => m.Id == id);
            if (administrator == null)
            {
                return NotFound();
            }

            return View(administrator);
        }

        // GET: Administrator/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Password,ConfirmPassword")] RegisterModel.InputModel input)
        {
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, input.Password);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(input);
        }

        // GET: Administrator/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Administrator == null)
            {
                return NotFound();
            }

            var administrator = await _context.Administrator.FindAsync(id);
            if (administrator == null)
            {
                return NotFound();
            }
            return View(administrator);
        }

        // POST: Administrator/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,LockoutEnd,LockoutEnabled")] Administrator administrator)
        {
            if (id != administrator.Id)
                return NotFound();

            var oldAdministrator = _context.Administrator.Find(id);

            if(oldAdministrator is null)
                return NotFound();

            oldAdministrator.ConcurrencyStamp =  ((char)(administrator.ConcurrencyStamp![0] + 1))+administrator.ConcurrencyStamp!.Substring(1);
            oldAdministrator.LockoutEnd = administrator.LockoutEnd;
            oldAdministrator.LockoutEnabled = administrator.LockoutEnabled;

            if (ModelState.IsValid)
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(administrator);
        }

        // GET: Administrator/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Administrator == null)
            {
                return NotFound();
            }

            var administrator = await _context.Administrator
                .FirstOrDefaultAsync(m => m.Id == id);
            if (administrator == null)
            {
                return NotFound();
            }

            return View(administrator);
        }

        // POST: Administrator/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Administrator == null)
            {
                return Problem("Entity set 'PenShopContext.Administrator'  is null.");
            }
            var administrator = await _context.Administrator.FindAsync(id);
            if (administrator != null)
            {
                _context.Administrator.Remove(administrator);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdministratorExists(string id)
        {
          return (_context.Administrator?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<Administrator>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(Administrator)}'. " +
                    $"Ensure that '{nameof(Administrator)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}
