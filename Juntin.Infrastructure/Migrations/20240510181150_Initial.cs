using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Juntin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    ConfirmedEmail = table.Column<bool>(type: "boolean", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    GoogleAccountId = table.Column<string>(type: "text", nullable: true),
                    GoogleAuthToken = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailConfirmation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ConfirmationToken = table.Column<string>(type: "text", nullable: false),
                    ConfirmedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailConfirmation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailConfirmation_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JuntinPlay",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    TextColor = table.Column<string>(type: "text", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JuntinPlay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JuntinPlay_User_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdminJuntin",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    JuntinPlayId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminJuntin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminJuntin_JuntinPlay_JuntinPlayId",
                        column: x => x.JuntinPlayId,
                        principalTable: "JuntinPlay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdminJuntin_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JuntinMovie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    UrlImage = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsWatchedEveryone = table.Column<bool>(type: "boolean", nullable: false),
                    TmdbId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    JuntinPlayId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JuntinMovie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JuntinMovie_JuntinPlay_JuntinPlayId",
                        column: x => x.JuntinPlayId,
                        principalTable: "JuntinPlay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JuntinMovie_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserJuntin",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    JuntinPlayId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserJuntin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserJuntin_JuntinPlay_JuntinPlayId",
                        column: x => x.JuntinPlayId,
                        principalTable: "JuntinPlay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserJuntin_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserViewedJuntinMovie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserJuntinId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsViewed = table.Column<bool>(type: "boolean", nullable: false),
                    JuntinMovieId = table.Column<Guid>(type: "uuid", nullable: false),
                    ViewedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserViewedJuntinMovie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserViewedJuntinMovie_JuntinMovie_JuntinMovieId",
                        column: x => x.JuntinMovieId,
                        principalTable: "JuntinMovie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserViewedJuntinMovie_UserJuntin_UserJuntinId",
                        column: x => x.UserJuntinId,
                        principalTable: "UserJuntin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminJuntin_JuntinPlayId",
                table: "AdminJuntin",
                column: "JuntinPlayId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminJuntin_UserId",
                table: "AdminJuntin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailConfirmation_UserId",
                table: "EmailConfirmation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_JuntinMovie_JuntinPlayId",
                table: "JuntinMovie",
                column: "JuntinPlayId");

            migrationBuilder.CreateIndex(
                name: "IX_JuntinMovie_UserId",
                table: "JuntinMovie",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_JuntinPlay_OwnerId",
                table: "JuntinPlay",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserJuntin_JuntinPlayId",
                table: "UserJuntin",
                column: "JuntinPlayId");

            migrationBuilder.CreateIndex(
                name: "IX_UserJuntin_UserId",
                table: "UserJuntin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserViewedJuntinMovie_JuntinMovieId",
                table: "UserViewedJuntinMovie",
                column: "JuntinMovieId");

            migrationBuilder.CreateIndex(
                name: "IX_UserViewedJuntinMovie_UserJuntinId",
                table: "UserViewedJuntinMovie",
                column: "UserJuntinId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminJuntin");

            migrationBuilder.DropTable(
                name: "EmailConfirmation");

            migrationBuilder.DropTable(
                name: "UserViewedJuntinMovie");

            migrationBuilder.DropTable(
                name: "JuntinMovie");

            migrationBuilder.DropTable(
                name: "UserJuntin");

            migrationBuilder.DropTable(
                name: "JuntinPlay");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
