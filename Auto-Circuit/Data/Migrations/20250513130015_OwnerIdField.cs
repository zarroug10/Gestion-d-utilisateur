using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auto_Circuit.Data.Migrations
{
    /// <inheritdoc />
    public partial class OwnerIdField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Contracts");
        }
    }
}
