using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Latihan.Migrations
{
    /// <inheritdoc />
    public partial class updatemodeleducation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.NIK);
                });

            migrationBuilder.CreateTable(
                name: "Universities",
                columns: table => new
                {
                    Univ_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Univ_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universities", x => x.Univ_Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_Accounts_Employees_NIK",
                        column: x => x.NIK,
                        principalTable: "Employees",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    Education_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Degree = table.Column<int>(type: "int", nullable: false),
                    GPA = table.Column<float>(type: "real", nullable: true),
                    University_Id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.Education_Id);
                    table.ForeignKey(
                        name: "FK_Educations_Universities_University_Id",
                        column: x => x.University_Id,
                        principalTable: "Universities",
                        principalColumn: "Univ_Id");
                });

            migrationBuilder.CreateTable(
                name: "Profilings",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Education_Id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profilings", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_Profilings_Accounts_NIK",
                        column: x => x.NIK,
                        principalTable: "Accounts",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Profilings_Educations_Education_Id",
                        column: x => x.Education_Id,
                        principalTable: "Educations",
                        principalColumn: "Education_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Educations_University_Id",
                table: "Educations",
                column: "University_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Profilings_Education_Id",
                table: "Profilings",
                column: "Education_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profilings");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Universities");
        }
    }
}
