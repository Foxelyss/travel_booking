using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBooking.Data.Migrations
{
    /// <inheritdoc />
    public partial class ManyBookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Books_AccountId",
                table: "Books");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AccountId",
                table: "Books",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Books_AccountId",
                table: "Books");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AccountId",
                table: "Books",
                column: "AccountId",
                unique: true);
        }
    }
}
