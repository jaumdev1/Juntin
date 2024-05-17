using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Juntin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddViewsInJuntinMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "JuntinMovie",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Views",
                table: "JuntinMovie");
        }
    }
}
