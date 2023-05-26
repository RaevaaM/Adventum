using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adventum.Migrations
{
    /// <inheritdoc />
    public partial class Add_Fields_To_Events : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Events",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Events");
        }
    }
}
