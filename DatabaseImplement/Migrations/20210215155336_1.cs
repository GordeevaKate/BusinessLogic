using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseImplement.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Oklad = table.Column<double>(nullable: false),
                    Comission = table.Column<double>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    Pasport = table.Column<string>(nullable: false),
                    ClientFIO = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dogovors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    AgentId = table.Column<int>(nullable: false),
                    Summa = table.Column<double>(nullable: false),
                    data = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dogovors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Raions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reiss",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfId = table.Column<int>(nullable: false),
                    ToId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Cena = table.Column<double>(nullable: false),
                    Time = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reiss", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(nullable: false),
                    Login = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zarplatas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Summa = table.Column<double>(nullable: false),
                    data = table.Column<DateTime>(nullable: false),
                    Period = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zarplatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dogovor_Reiss",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReisId = table.Column<int>(nullable: false),
                    DogovorId = table.Column<int>(nullable: false),
                    NadbavkaCena = table.Column<double>(nullable: false),
                    NadbavkaTime = table.Column<double>(nullable: false),
                    Comm = table.Column<string>(nullable: false),
                    Obem = table.Column<double>(nullable: false),
                    ves = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dogovor_Reiss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dogovor_Reiss_Dogovors_DogovorId",
                        column: x => x.DogovorId,
                        principalTable: "Dogovors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dogovor_Reiss_Reiss_ReisId",
                        column: x => x.ReisId,
                        principalTable: "Reiss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dogovor_Reiss_DogovorId",
                table: "Dogovor_Reiss",
                column: "DogovorId");

            migrationBuilder.CreateIndex(
                name: "IX_Dogovor_Reiss_ReisId",
                table: "Dogovor_Reiss",
                column: "ReisId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Dogovor_Reiss");

            migrationBuilder.DropTable(
                name: "Raions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Zarplatas");

            migrationBuilder.DropTable(
                name: "Dogovors");

            migrationBuilder.DropTable(
                name: "Reiss");
        }
    }
}
