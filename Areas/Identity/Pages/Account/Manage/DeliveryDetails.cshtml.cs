// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace PenShop.Areas.Identity.Pages.Account.Manage
{
    public class DeliveryDetailsModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;
        private readonly IStringLocalizer _stringLocalizer;

        public DeliveryDetailsModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<ChangePasswordModel> logger,
            IStringLocalizer stringLocalizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _stringLocalizer = stringLocalizer;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [Required]
            [StringLength(50)]
            public string Name{ get; set; }
            [Required]
            [StringLength(50)]
            public string Surname{ get; set; }
            [Display(Name = "DefaultShippingAddress")]
            [StringLength(1000)]
            public string DefaultShippingAddress{ get; set; }
        }

        private void LoadAsync(IdentityUser user){
            var customer = (Models.Customer)user;

            Input = new InputModel() {
                Name = customer.Name,
                Surname = customer.Surname,
                DefaultShippingAddress = customer.DefaultShippingAddress
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if(user is not Models.Customer){
                return BadRequest($"user with ID '{_userManager.GetUserId(User)}' is not a customer.");
            }

            LoadAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if(user is not Models.Customer){
                return BadRequest($"user with ID '{_userManager.GetUserId(User)}' is not a customer.");
            }

            var customer = (Models.Customer)user;

            customer.Name = Input.Name;
            customer.Surname = Input.Surname;
            customer.DefaultShippingAddress = Input.DefaultShippingAddress;

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their delivery information successfully.");
            StatusMessage = _stringLocalizer.GetString("DeliveryInfoChanged");

            return RedirectToPage();
        }
    }
}
