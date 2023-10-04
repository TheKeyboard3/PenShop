using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PenShop.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrator",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartridgeStandard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartridgeStandard", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Surname = table.Column<string>(type: "TEXT", nullable: true),
                    DefaultShippingAddress = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InkColour",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InkColour", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NibMaterial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Hardness = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NibMaterial", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ShippingAddress = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Nib",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BodyMaterialId = table.Column<int>(type: "INTEGER", nullable: true),
                    TipMaterialId = table.Column<int>(type: "INTEGER", nullable: true),
                    TipDiameter = table.Column<float>(type: "REAL", nullable: false),
                    Price = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nib", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nib_NibMaterial_BodyMaterialId",
                        column: x => x.BodyMaterialId,
                        principalTable: "NibMaterial",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Nib_NibMaterial_TipMaterialId",
                        column: x => x.TipMaterialId,
                        principalTable: "NibMaterial",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Price = table.Column<float>(type: "REAL", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    Height = table.Column<float>(type: "REAL", nullable: true),
                    Capacity = table.Column<float>(type: "REAL", nullable: true),
                    Converter_CartridgeStandardId = table.Column<int>(type: "INTEGER", nullable: true),
                    NibId = table.Column<int>(type: "INTEGER", nullable: true),
                    Stand_MaterialId = table.Column<int>(type: "INTEGER", nullable: true),
                    InkBottle_Capacity = table.Column<float>(type: "REAL", nullable: true),
                    ColourId = table.Column<int>(type: "INTEGER", nullable: true),
                    InkCartridge_Capacity = table.Column<float>(type: "REAL", nullable: true),
                    InkCartridge_CartridgeStandardId = table.Column<int>(type: "INTEGER", nullable: true),
                    InkCartridge_ColourId = table.Column<int>(type: "INTEGER", nullable: true),
                    CartridgeStandardId = table.Column<int>(type: "INTEGER", nullable: true),
                    MaterialId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_CartridgeStandard_CartridgeStandardId",
                        column: x => x.CartridgeStandardId,
                        principalTable: "CartridgeStandard",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_CartridgeStandard_Converter_CartridgeStandardId",
                        column: x => x.Converter_CartridgeStandardId,
                        principalTable: "CartridgeStandard",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_CartridgeStandard_InkCartridge_CartridgeStandardId",
                        column: x => x.InkCartridge_CartridgeStandardId,
                        principalTable: "CartridgeStandard",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_InkColour_ColourId",
                        column: x => x.ColourId,
                        principalTable: "InkColour",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_InkColour_InkCartridge_ColourId",
                        column: x => x.InkCartridge_ColourId,
                        principalTable: "InkColour",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_Material_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Material",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_Material_Stand_MaterialId",
                        column: x => x.Stand_MaterialId,
                        principalTable: "Material",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_Nib_NibId",
                        column: x => x.NibId,
                        principalTable: "Nib",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: true),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    PenId = table.Column<int>(type: "INTEGER", nullable: true),
                    RemoveNib = table.Column<bool>(type: "INTEGER", nullable: true),
                    GeneralProductId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductOrder_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductOrder_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductOrder_Product_GeneralProductId",
                        column: x => x.GeneralProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductOrder_Product_PenId",
                        column: x => x.PenId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nib_BodyMaterialId",
                table: "Nib",
                column: "BodyMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Nib_TipMaterialId",
                table: "Nib",
                column: "TipMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CartridgeStandardId",
                table: "Product",
                column: "CartridgeStandardId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ColourId",
                table: "Product",
                column: "ColourId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Converter_CartridgeStandardId",
                table: "Product",
                column: "Converter_CartridgeStandardId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_InkCartridge_CartridgeStandardId",
                table: "Product",
                column: "InkCartridge_CartridgeStandardId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_InkCartridge_ColourId",
                table: "Product",
                column: "InkCartridge_ColourId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_MaterialId",
                table: "Product",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_NibId",
                table: "Product",
                column: "NibId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Stand_MaterialId",
                table: "Product",
                column: "Stand_MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_CustomerId",
                table: "ProductOrder",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_GeneralProductId",
                table: "ProductOrder",
                column: "GeneralProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_OrderId",
                table: "ProductOrder",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_PenId",
                table: "ProductOrder",
                column: "PenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrator");

            migrationBuilder.DropTable(
                name: "ProductOrder");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "CartridgeStandard");

            migrationBuilder.DropTable(
                name: "InkColour");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "Nib");

            migrationBuilder.DropTable(
                name: "NibMaterial");
        }
    }
}
