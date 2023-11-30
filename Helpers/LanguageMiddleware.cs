using Microsoft.AspNetCore.Http;
using System.Web;
using System;
using System.Globalization;

namespace PenShop.Middleware;

public static class LanguageMiddleware{
    public static async Task MiddlewareMethod(HttpContext context, Func<Task> next){
        string cultureName = context.Request.Cookies["lang"] ?? "en";
        List<string> cultures = new List<string>() { "uk", "en" };
        if (!cultures.Contains(cultureName))
            cultureName = "en";

        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
        Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
        await next.Invoke();
    }

    public static IApplicationBuilder UseLanguageMiddleware(this IApplicationBuilder webApp){
        return webApp.Use(MiddlewareMethod);
    }
}
