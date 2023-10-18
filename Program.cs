using PenShop.Data;
using PenShop.Controllers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PenShopContext>(b => b.UseLazyLoadingProxies());

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


app.Run();
