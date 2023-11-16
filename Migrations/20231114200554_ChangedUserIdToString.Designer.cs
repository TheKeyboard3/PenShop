﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PenShop.Data;

#nullable disable

namespace PenShop.Migrations
{
    [DbContext(typeof(PenShopContext))]
    [Migration("20231114200554_ChangedUserIdToString")]
    partial class ChangedUserIdToString
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("PenShop.Models.CartridgeStandard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CartridgeStandard");
                });

            modelBuilder.Entity("PenShop.Models.InkColour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("InkColour");
                });

            modelBuilder.Entity("PenShop.Models.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Material");
                });

            modelBuilder.Entity("PenShop.Models.Nib", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BodyMaterialId")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Price")
                        .HasColumnType("REAL");

                    b.Property<float>("TipDiameter")
                        .HasColumnType("REAL");

                    b.Property<int>("TipMaterialId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BodyMaterialId");

                    b.HasIndex("TipMaterialId");

                    b.ToTable("Nib");
                });

            modelBuilder.Entity("PenShop.Models.NibMaterial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Hardness")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("NibMaterial");
                });

            modelBuilder.Entity("PenShop.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("ShippingAddress")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("PenShop.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<float>("Price")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Product");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Product");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("PenShop.Models.ProductOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CustomerId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("OrderId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderId");

                    b.ToTable("ProductOrder");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ProductOrder");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("PenShop.Models.Administrator", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.HasDiscriminator().HasValue("Administrator");
                });

            modelBuilder.Entity("PenShop.Models.Customer", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("DefaultShippingAddress")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("Customer");
                });

            modelBuilder.Entity("PenShop.Models.Accessory", b =>
                {
                    b.HasBaseType("PenShop.Models.Product");

                    b.HasDiscriminator().HasValue("Accessory");
                });

            modelBuilder.Entity("PenShop.Models.Ink", b =>
                {
                    b.HasBaseType("PenShop.Models.Product");

                    b.Property<float>("Capacity")
                        .HasColumnType("REAL");

                    b.Property<int>("ColourId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("ColourId");

                    b.HasDiscriminator().HasValue("Ink");
                });

            modelBuilder.Entity("PenShop.Models.Pen", b =>
                {
                    b.HasBaseType("PenShop.Models.Product");

                    b.Property<int>("CartridgeStandardId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaterialId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("CartridgeStandardId");

                    b.HasIndex("MaterialId");

                    b.HasDiscriminator().HasValue("Pen");
                });

            modelBuilder.Entity("PenShop.Models.FountainPenOrder", b =>
                {
                    b.HasBaseType("PenShop.Models.ProductOrder");

                    b.Property<int>("PenId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("RemoveNib")
                        .HasColumnType("INTEGER");

                    b.HasIndex("PenId");

                    b.HasDiscriminator().HasValue("FountainPenOrder");
                });

            modelBuilder.Entity("PenShop.Models.GeneralProductOrder", b =>
                {
                    b.HasBaseType("PenShop.Models.ProductOrder");

                    b.Property<int>("GeneralProductId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("GeneralProductId");

                    b.HasDiscriminator().HasValue("GeneralProductOrder");
                });

            modelBuilder.Entity("PenShop.Models.Converter", b =>
                {
                    b.HasBaseType("PenShop.Models.Accessory");

                    b.Property<float>("Capacity")
                        .HasColumnType("REAL");

                    b.Property<int>("CartridgeStandardId")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Height")
                        .HasColumnType("REAL");

                    b.HasIndex("CartridgeStandardId");

                    b.ToTable("Product", t =>
                        {
                            t.Property("Capacity")
                                .HasColumnName("Converter_Capacity");

                            t.Property("CartridgeStandardId")
                                .HasColumnName("Converter_CartridgeStandardId");
                        });

                    b.HasDiscriminator().HasValue("Converter");
                });

            modelBuilder.Entity("PenShop.Models.NibAccessory", b =>
                {
                    b.HasBaseType("PenShop.Models.Accessory");

                    b.Property<int>("NibId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("NibId");

                    b.ToTable("Product", t =>
                        {
                            t.Property("NibId")
                                .HasColumnName("NibAccessory_NibId");
                        });

                    b.HasDiscriminator().HasValue("NibAccessory");
                });

            modelBuilder.Entity("PenShop.Models.Stand", b =>
                {
                    b.HasBaseType("PenShop.Models.Accessory");

                    b.Property<int>("MaterialId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("MaterialId");

                    b.ToTable("Product", t =>
                        {
                            t.Property("MaterialId")
                                .HasColumnName("Stand_MaterialId");
                        });

                    b.HasDiscriminator().HasValue("Stand");
                });

            modelBuilder.Entity("PenShop.Models.InkBottle", b =>
                {
                    b.HasBaseType("PenShop.Models.Ink");

                    b.HasDiscriminator().HasValue("InkBottle");
                });

            modelBuilder.Entity("PenShop.Models.InkCartridge", b =>
                {
                    b.HasBaseType("PenShop.Models.Ink");

                    b.Property<int>("CartridgeStandardId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("CartridgeStandardId");

                    b.ToTable("Product", t =>
                        {
                            t.Property("CartridgeStandardId")
                                .HasColumnName("InkCartridge_CartridgeStandardId");
                        });

                    b.HasDiscriminator().HasValue("InkCartridge");
                });

            modelBuilder.Entity("PenShop.Models.FountainPen", b =>
                {
                    b.HasBaseType("PenShop.Models.Pen");

                    b.Property<int>("NibId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("NibId");

                    b.HasDiscriminator().HasValue("FountainPen");
                });

            modelBuilder.Entity("PenShop.Models.RollerballPen", b =>
                {
                    b.HasBaseType("PenShop.Models.Pen");

                    b.Property<float>("RollerballDiameter")
                        .HasColumnType("REAL");

                    b.HasDiscriminator().HasValue("RollerballPen");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PenShop.Models.Nib", b =>
                {
                    b.HasOne("PenShop.Models.NibMaterial", "BodyMaterial")
                        .WithMany()
                        .HasForeignKey("BodyMaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PenShop.Models.NibMaterial", "TipMaterial")
                        .WithMany()
                        .HasForeignKey("TipMaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BodyMaterial");

                    b.Navigation("TipMaterial");
                });

            modelBuilder.Entity("PenShop.Models.Order", b =>
                {
                    b.HasOne("PenShop.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("PenShop.Models.ProductOrder", b =>
                {
                    b.HasOne("PenShop.Models.Customer", "Customer")
                        .WithMany("ShoppingCart")
                        .HasForeignKey("CustomerId");

                    b.HasOne("PenShop.Models.Order", "Order")
                        .WithMany("ProductOrders")
                        .HasForeignKey("OrderId");

                    b.Navigation("Customer");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("PenShop.Models.Ink", b =>
                {
                    b.HasOne("PenShop.Models.InkColour", "Colour")
                        .WithMany()
                        .HasForeignKey("ColourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Colour");
                });

            modelBuilder.Entity("PenShop.Models.Pen", b =>
                {
                    b.HasOne("PenShop.Models.CartridgeStandard", "CartridgeStandard")
                        .WithMany()
                        .HasForeignKey("CartridgeStandardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PenShop.Models.Material", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CartridgeStandard");

                    b.Navigation("Material");
                });

            modelBuilder.Entity("PenShop.Models.FountainPenOrder", b =>
                {
                    b.HasOne("PenShop.Models.FountainPen", "Pen")
                        .WithMany()
                        .HasForeignKey("PenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pen");
                });

            modelBuilder.Entity("PenShop.Models.GeneralProductOrder", b =>
                {
                    b.HasOne("PenShop.Models.Product", "GeneralProduct")
                        .WithMany()
                        .HasForeignKey("GeneralProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GeneralProduct");
                });

            modelBuilder.Entity("PenShop.Models.Converter", b =>
                {
                    b.HasOne("PenShop.Models.CartridgeStandard", "CartridgeStandard")
                        .WithMany()
                        .HasForeignKey("CartridgeStandardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CartridgeStandard");
                });

            modelBuilder.Entity("PenShop.Models.NibAccessory", b =>
                {
                    b.HasOne("PenShop.Models.Nib", "Nib")
                        .WithMany()
                        .HasForeignKey("NibId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Nib");
                });

            modelBuilder.Entity("PenShop.Models.Stand", b =>
                {
                    b.HasOne("PenShop.Models.Material", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Material");
                });

            modelBuilder.Entity("PenShop.Models.InkCartridge", b =>
                {
                    b.HasOne("PenShop.Models.CartridgeStandard", "CartridgeStandard")
                        .WithMany()
                        .HasForeignKey("CartridgeStandardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CartridgeStandard");
                });

            modelBuilder.Entity("PenShop.Models.FountainPen", b =>
                {
                    b.HasOne("PenShop.Models.Nib", "Nib")
                        .WithMany()
                        .HasForeignKey("NibId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Nib");
                });

            modelBuilder.Entity("PenShop.Models.Order", b =>
                {
                    b.Navigation("ProductOrders");
                });

            modelBuilder.Entity("PenShop.Models.Customer", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("ShoppingCart");
                });
#pragma warning restore 612, 618
        }
    }
}