using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PenShop.Data;
using PenShop.Models;

namespace PenShop.Controllers
{
    public class LanguageController : Controller
    {
        public IActionResult ChangeLanguage(string lang)
        {
            string returnUrl = Request.Headers.Referer.ToString();
            List<string> cultures = new List<string>() { "uk", "en" };
            if (!cultures.Contains(lang))
                lang = "en";

            Response.Cookies.Append("lang", lang, new CookieOptions()
                {
                    HttpOnly = false,
                    Expires = DateTime.Now.AddYears(1)
                });

            return Redirect(returnUrl);
        }
    }
}
