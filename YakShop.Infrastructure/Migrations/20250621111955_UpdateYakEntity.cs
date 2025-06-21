using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YakShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateYakEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CanBeShavedAt",
                table: "Yak",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsDead",
                table: "Yak",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanBeShavedAt",
                table: "Yak");

            migrationBuilder.DropColumn(
                name: "IsDead",
                table: "Yak");
        }
    }
}
