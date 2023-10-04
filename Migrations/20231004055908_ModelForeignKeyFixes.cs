using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PenShop.Migrations
{
    /// <inheritdoc />
    public partial class ModelForeignKeyFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nib_NibMaterial_BodyMaterialId",
                table: "Nib");

            migrationBuilder.DropForeignKey(
                name: "FK_Nib_NibMaterial_TipMaterialId",
                table: "Nib");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_CartridgeStandard_CartridgeStandardId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_CartridgeStandard_Converter_CartridgeStandardId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_CartridgeStandard_InkCartridge_CartridgeStandardId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_InkColour_ColourId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_InkColour_InkCartridge_ColourId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Material_MaterialId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Material_Stand_MaterialId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Nib_NibId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Product_GeneralProductId",
                table: "ProductOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Product_PenId",
                table: "ProductOrder");

            migrationBuilder.DropColumn(
                name: "InkBottle_Capacity",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "InkCartridge_Capacity",
                table: "Product",
                newName: "Converter_Capacity");

            migrationBuilder.RenameColumn(
                name: "InkCartridge_ColourId",
                table: "Product",
                newName: "NibAccessory_NibId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_InkCartridge_ColourId",
                table: "Product",
                newName: "IX_Product_NibAccessory_NibId");

            migrationBuilder.AlterColumn<int>(
                name: "TipMaterialId",
                table: "Nib",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BodyMaterialId",
                table: "Nib",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Nib_NibMaterial_BodyMaterialId",
                table: "Nib",
                column: "BodyMaterialId",
                principalTable: "NibMaterial",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Nib_NibMaterial_TipMaterialId",
                table: "Nib",
                column: "TipMaterialId",
                principalTable: "NibMaterial",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_CartridgeStandard_CartridgeStandardId",
                table: "Product",
                column: "CartridgeStandardId",
                principalTable: "CartridgeStandard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_CartridgeStandard_Converter_CartridgeStandardId",
                table: "Product",
                column: "Converter_CartridgeStandardId",
                principalTable: "CartridgeStandard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_CartridgeStandard_InkCartridge_CartridgeStandardId",
                table: "Product",
                column: "InkCartridge_CartridgeStandardId",
                principalTable: "CartridgeStandard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_InkColour_ColourId",
                table: "Product",
                column: "ColourId",
                principalTable: "InkColour",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Material_MaterialId",
                table: "Product",
                column: "MaterialId",
                principalTable: "Material",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Material_Stand_MaterialId",
                table: "Product",
                column: "Stand_MaterialId",
                principalTable: "Material",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Nib_NibAccessory_NibId",
                table: "Product",
                column: "NibAccessory_NibId",
                principalTable: "Nib",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Nib_NibId",
                table: "Product",
                column: "NibId",
                principalTable: "Nib",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_Product_GeneralProductId",
                table: "ProductOrder",
                column: "GeneralProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_Product_PenId",
                table: "ProductOrder",
                column: "PenId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nib_NibMaterial_BodyMaterialId",
                table: "Nib");

            migrationBuilder.DropForeignKey(
                name: "FK_Nib_NibMaterial_TipMaterialId",
                table: "Nib");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_CartridgeStandard_CartridgeStandardId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_CartridgeStandard_Converter_CartridgeStandardId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_CartridgeStandard_InkCartridge_CartridgeStandardId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_InkColour_ColourId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Material_MaterialId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Material_Stand_MaterialId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Nib_NibAccessory_NibId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Nib_NibId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Product_GeneralProductId",
                table: "ProductOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Product_PenId",
                table: "ProductOrder");

            migrationBuilder.RenameColumn(
                name: "Converter_Capacity",
                table: "Product",
                newName: "InkCartridge_Capacity");

            migrationBuilder.RenameColumn(
                name: "NibAccessory_NibId",
                table: "Product",
                newName: "InkCartridge_ColourId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_NibAccessory_NibId",
                table: "Product",
                newName: "IX_Product_InkCartridge_ColourId");

            migrationBuilder.AddColumn<float>(
                name: "InkBottle_Capacity",
                table: "Product",
                type: "REAL",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TipMaterialId",
                table: "Nib",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "BodyMaterialId",
                table: "Nib",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Nib_NibMaterial_BodyMaterialId",
                table: "Nib",
                column: "BodyMaterialId",
                principalTable: "NibMaterial",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Nib_NibMaterial_TipMaterialId",
                table: "Nib",
                column: "TipMaterialId",
                principalTable: "NibMaterial",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_CartridgeStandard_CartridgeStandardId",
                table: "Product",
                column: "CartridgeStandardId",
                principalTable: "CartridgeStandard",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_CartridgeStandard_Converter_CartridgeStandardId",
                table: "Product",
                column: "Converter_CartridgeStandardId",
                principalTable: "CartridgeStandard",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_CartridgeStandard_InkCartridge_CartridgeStandardId",
                table: "Product",
                column: "InkCartridge_CartridgeStandardId",
                principalTable: "CartridgeStandard",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_InkColour_ColourId",
                table: "Product",
                column: "ColourId",
                principalTable: "InkColour",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_InkColour_InkCartridge_ColourId",
                table: "Product",
                column: "InkCartridge_ColourId",
                principalTable: "InkColour",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Material_MaterialId",
                table: "Product",
                column: "MaterialId",
                principalTable: "Material",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Material_Stand_MaterialId",
                table: "Product",
                column: "Stand_MaterialId",
                principalTable: "Material",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Nib_NibId",
                table: "Product",
                column: "NibId",
                principalTable: "Nib",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_Product_GeneralProductId",
                table: "ProductOrder",
                column: "GeneralProductId",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_Product_PenId",
                table: "ProductOrder",
                column: "PenId",
                principalTable: "Product",
                principalColumn: "Id");
        }
    }
}
