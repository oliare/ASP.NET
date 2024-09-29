using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiStore.Migrations
{
    /// <inheritdoc />
    public partial class RenamePriorityColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Priotity",
                table: "ProductImages",
                newName: "Priority");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Priority",
                table: "ProductImages",
                newName: "Priotity");
        }
    }
}
