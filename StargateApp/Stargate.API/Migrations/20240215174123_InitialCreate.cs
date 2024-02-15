using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StargateAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogTableEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Message = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogTableEntry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AstronautDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrentRank = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrentDutyTitle = table.Column<int>(type: "INTEGER", nullable: false),
                    CareerStartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CareerEndDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AstronautDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AstronautDetail_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AstronautDuty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false),
                    Rank = table.Column<int>(type: "INTEGER", nullable: false),
                    DutyTitle = table.Column<int>(type: "INTEGER", nullable: false),
                    DutyStartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DutyEndDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AstronautDuty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AstronautDuty_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "John Doe" },
                    { 2, "Jane Doe" }
                });

            migrationBuilder.InsertData(
                table: "AstronautDetail",
                columns: new[] { "Id", "CareerEndDate", "CareerStartDate", "CurrentDutyTitle", "CurrentRank", "PersonId" },
                values: new object[,]
                {
                    { 1, new DateTime(2010, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 7, 1 },
                    { 2, null, new DateTime(1995, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 8, 2 }
                });

            migrationBuilder.InsertData(
                table: "AstronautDuty",
                columns: new[] { "Id", "DutyEndDate", "DutyStartDate", "DutyTitle", "PersonId", "Rank" },
                values: new object[,]
                {
                    { 1, new DateTime(2000, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1, 5 },
                    { 2, new DateTime(2010, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, 5 },
                    { 3, null, new DateTime(2010, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 1, 7 },
                    { 4, new DateTime(2000, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1995, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2, 5 },
                    { 5, null, new DateTime(2000, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 2, 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AstronautDetail_PersonId",
                table: "AstronautDetail",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AstronautDuty_PersonId",
                table: "AstronautDuty",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AstronautDetail");

            migrationBuilder.DropTable(
                name: "AstronautDuty");

            migrationBuilder.DropTable(
                name: "LogTableEntry");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
