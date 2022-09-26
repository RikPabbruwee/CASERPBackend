using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cursussen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Duur = table.Column<int>(type: "int", nullable: false),
                    Titel = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursussen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteWeeks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Week = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteWeeks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CursusInstanties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Startdatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CursusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursusInstanties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CursusInstanties_Cursussen_CursusId",
                        column: x => x.CursusId,
                        principalTable: "Cursussen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cursisten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Voornaam = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Achternaam = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CursusInstantieId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursisten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cursisten_CursusInstanties_CursusInstantieId",
                        column: x => x.CursusInstantieId,
                        principalTable: "CursusInstanties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cursisten_CursusInstantieId",
                table: "Cursisten",
                column: "CursusInstantieId");

            migrationBuilder.CreateIndex(
                name: "IX_CursusInstanties_CursusId",
                table: "CursusInstanties",
                column: "CursusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cursisten");

            migrationBuilder.DropTable(
                name: "FavoriteWeeks");

            migrationBuilder.DropTable(
                name: "CursusInstanties");

            migrationBuilder.DropTable(
                name: "Cursussen");
        }
    }
}
