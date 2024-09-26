using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiStore.Migrations
{
    /// <inheritdoc />
    public partial class AddProductImagesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImageEntity_Products_ProductId",
                table: "ProductImageEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductImageEntity",
                table: "ProductImageEntity");

            migrationBuilder.RenameTable(
                name: "ProductImageEntity",
                newName: "ProductImages");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImageEntity_ProductId",
                table: "ProductImages",
                newName: "IX_ProductImages_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages");

            migrationBuilder.RenameTable(
                name: "ProductImages",
                newName: "ProductImageEntity");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImageEntity",
                newName: "IX_ProductImageEntity_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductImageEntity",
                table: "ProductImageEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImageEntity_Products_ProductId",
                table: "ProductImageEntity",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
