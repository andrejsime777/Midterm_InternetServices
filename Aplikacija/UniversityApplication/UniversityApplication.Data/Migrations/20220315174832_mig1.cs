using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversityApplication.Data.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Owner = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    SignDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    TotalGoals = table.Column<int>(type: "int", nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clubs",
                columns: new[] { "Id", "City", "Country", "Name", "Owner" },
                values: new object[,]
                {
                    { 1, "Belgrade", "Serbia", "Partizan", "Andrej" },
                    { 2, "Belgrade", "Serbia", "Crvena Zvezda", "Petko" },
                    { 3, "Skopje", "Macedonia", "Vardar", "Stanko" },
                    { 4, "Paris", "France", "Paris Saint Germain", "Filip" },
                    { 5, "Manchester", "UK", "Manchester United", "Martin" }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "ClubId", "DateOfBirth", "FirstName", "LastName", "Rank", "SignDate", "TotalGoals" },
                values: new object[,]
                {
                    { 5, 1, new DateTime(1990, 3, 15, 0, 0, 0, 0, DateTimeKind.Local), "Petko", "Stankovski", 5, new DateTime(2012, 3, 15, 0, 0, 0, 0, DateTimeKind.Local), 1522 },
                    { 1, 2, new DateTime(2002, 3, 15, 0, 0, 0, 0, DateTimeKind.Local), "Andrej", "Postolovski", 5, new DateTime(2018, 3, 15, 0, 0, 0, 0, DateTimeKind.Local), 5 },
                    { 3, 3, new DateTime(1972, 3, 15, 0, 0, 0, 0, DateTimeKind.Local), "Stanko", "Petkovski", 5, new DateTime(2002, 3, 15, 0, 0, 0, 0, DateTimeKind.Local), 75411 },
                    { 2, 4, new DateTime(1998, 3, 15, 0, 0, 0, 0, DateTimeKind.Local), "Filip", "Simonovski", 5, new DateTime(2016, 3, 15, 0, 0, 0, 0, DateTimeKind.Local), 859 },
                    { 4, 5, new DateTime(2010, 3, 15, 0, 0, 0, 0, DateTimeKind.Local), "Petar", "Petrovski", 5, new DateTime(2021, 3, 15, 0, 0, 0, 0, DateTimeKind.Local), 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_ClubId",
                table: "Players",
                column: "ClubId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Clubs");
        }
    }
}
