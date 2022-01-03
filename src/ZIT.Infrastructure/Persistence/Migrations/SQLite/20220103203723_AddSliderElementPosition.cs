using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZIT.Infrastructure.Persistence.Migrations.SQLite
{
    public partial class AddSliderElementPosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "SliderElements",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SliderElements_Position",
                table: "SliderElements",
                column: "Position",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_SliderElements_POSITION_GREATER_OR_EQUAL_ZERO",
                table: "SliderElements",
                sql: "Position >= 0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SliderElements_Position",
                table: "SliderElements");

            migrationBuilder.DropCheckConstraint(
                name: "CK_SliderElements_POSITION_GREATER_OR_EQUAL_ZERO",
                table: "SliderElements");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "SliderElements");
        }
    }
}
