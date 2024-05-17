using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Juntin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class inviteJuntinPlay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InviteJuntinPlay",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    JuntinPlayId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpireAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Link = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InviteJuntinPlay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InviteJuntinPlay_JuntinPlay_JuntinPlayId",
                        column: x => x.JuntinPlayId,
                        principalTable: "JuntinPlay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InviteJuntinPlay_JuntinPlayId",
                table: "InviteJuntinPlay",
                column: "JuntinPlayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InviteJuntinPlay");
        }
    }
}
