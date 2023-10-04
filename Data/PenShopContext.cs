using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PenShop.Models;

namespace PenShop.Data;

public partial class PenShopContext : DbContext
{
    public PenShopContext()
    {
    }

    public PenShopContext(DbContextOptions<PenShopContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=PenShop.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<PenShop.Models.NibAccessory> NibAccessory { get; set; } = default!;

    public DbSet<PenShop.Models.Nib> Nib { get; set; } = default!;

    public DbSet<PenShop.Models.Administrator> Administrator { get; set; } = default!;

    public DbSet<PenShop.Models.CartridgeStandard> CartridgeStandard { get; set; } = default!;

    public DbSet<PenShop.Models.Accessory> Accessory { get; set; } = default!;

    public DbSet<PenShop.Models.Converter> Converter { get; set; } = default!;

    public DbSet<PenShop.Models.GeneralProductOrder> GeneralProductOrder { get; set; } = default!;

    public DbSet<PenShop.Models.FountainPenOrder> FountainPenOrder { get; set; } = default!;

    public DbSet<PenShop.Models.Customer> Customer { get; set; } = default!;

    public DbSet<PenShop.Models.FountainPen> FountainPen { get; set; } = default!;

    public DbSet<PenShop.Models.InkCartridge> InkCartridge { get; set; } = default!;

    public DbSet<PenShop.Models.InkBottle> InkBottle { get; set; } = default!;

    public DbSet<PenShop.Models.Ink> Ink { get; set; } = default!;

    public DbSet<PenShop.Models.InkColour> InkColour { get; set; } = default!;

    public DbSet<PenShop.Models.Material> Material { get; set; } = default!;

    public DbSet<PenShop.Models.NibMaterial> NibMaterial { get; set; } = default!;

    public DbSet<PenShop.Models.Order> Order { get; set; } = default!;

    public DbSet<PenShop.Models.Pen> Pen { get; set; } = default!;

    public DbSet<PenShop.Models.Product> Product { get; set; } = default!;

    public DbSet<PenShop.Models.ProductOrder> ProductOrder { get; set; } = default!;

    public DbSet<PenShop.Models.RollerballPen> RollerballPen { get; set; } = default!;

    public DbSet<PenShop.Models.Stand> Stand { get; set; } = default!;
}
