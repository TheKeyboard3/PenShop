using PenShop.Authentication;
using PenShop.Data;
using PenShop.Controllers;
using PenShop.Models;
using PenShop.Middleware;
using Askmethat.Aspnet.JsonLocalizer.Extensions;
using Askmethat.Aspnet.JsonLocalizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Text;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("PenShopContextConnection") ?? throw new InvalidOperationException("Connection string 'PenShopContextConnection' not found.");

// Add services to the container.
builder.Services.AddJsonLocalization(options => {
        options.CacheDuration = TimeSpan.FromMinutes(15);
        options.ResourcesPath = "Resources";
        options.FileEncoding = Encoding.GetEncoding("UTF-8");
        options.SupportedCultureInfos = new HashSet<CultureInfo>()
        {
          new CultureInfo("en"),
          new CultureInfo("uk")
        };
    });
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "en", "uk" };
    options.SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
});
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<PenShopContext>(b => b.UseLazyLoadingProxies(), optionsLifetime: ServiceLifetime.Singleton);
builder.Services.AddSingleton<PenShopContext>();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(120);
    options.Lockout.MaxFailedAccessAttempts = 5;
})
.AddEntityFrameworkStores<PenShopContext>();

builder.Services.AddSingleton<IAuthorizationHandler, IsRequirementHandler>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrator", policy => policy.Requirements.Add(new IsRequirement<Administrator>()));
    options.AddPolicy("Customer", policy => policy.Requirements.Add(new IsRequirement<Customer>()));
});

builder.Services.AddScoped<ConverterController>();
builder.Services.AddScoped<NibAccessoryController>();
builder.Services.AddScoped<StandController>();
builder.Services.AddScoped<FountainPenController>();
builder.Services.AddScoped<RollerballPenController>();
builder.Services.AddScoped<InkBottleController>();
builder.Services.AddScoped<InkCartridgeController>();

builder.Services.AddScoped<AccessoryController>();
builder.Services.AddScoped<PenController>();
builder.Services.AddScoped<InkController>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseLanguageMiddleware();

app.MapControllerRoute(
    name: "generalProductOrderCreate",
    pattern: "GeneralProductOrder/Create/{productId}",
    defaults: new {controller="GeneralProductOrder", action="Create"});

app.MapControllerRoute(
    name: "fountainPenOrderCreate",
    pattern: "FountainPenOrder/Create/{penId}",
    defaults: new {controller="FountainPenOrder", action="Create"});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.MapRazorPages();


app.Run();
