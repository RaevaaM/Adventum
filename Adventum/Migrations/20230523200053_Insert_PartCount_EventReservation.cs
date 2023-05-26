using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adventum.Migrations
{
    /// <inheritdoc />
    public partial class Insert_PartCount_EventReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParticipantsCount",
                table: "EventReservations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParticipantsCount",
                table: "EventReservations");
        }
    }
}
