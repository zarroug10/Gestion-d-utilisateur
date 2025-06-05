using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auto_Circuit.Data.Migrationsn
{
    /// <inheritdoc />
    public partial class UpdatedRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkTimes_AspNetUsers_UserId",
                table: "WorkTimes");

            migrationBuilder.DropIndex(
                name: "IX_WorkTimes_UserId",
                table: "WorkTimes");

            migrationBuilder.CreateIndex(
                name: "IX_WorkTimes_UserId",
                table: "WorkTimes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkTimes_AspNetUsers_UserId",
                table: "WorkTimes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkTimes_AspNetUsers_UserId",
                table: "WorkTimes");

            migrationBuilder.DropIndex(
                name: "IX_WorkTimes_UserId",
                table: "WorkTimes");

            migrationBuilder.CreateIndex(
                name: "IX_WorkTimes_UserId",
                table: "WorkTimes",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkTimes_AspNetUsers_UserId",
                table: "WorkTimes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
