using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auto_Circuit.Data.Migrations
{
    /// <inheritdoc />
    public partial class iPendingField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPending",
                table: "Vacations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPending",
                table: "Vacations");
        }
    }
}
