using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auto_Circuit.Migrations
{
    /// <inheritdoc />
    public partial class RoleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Account",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "1", "1" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Admin",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "2", "2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Account",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "aeab8c44-4cb6-4be1-a65d-dae33f44d2e4", "AEAB8C44-4CB6-4BE1-A65D-DAE33F44D2E4" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Admin",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "687a6a01-cfef-4116-80fc-12a73d27a2be", "687A6A01-CFEF-4116-80FC-12A73D27A2BE" });
        }
    }
}
