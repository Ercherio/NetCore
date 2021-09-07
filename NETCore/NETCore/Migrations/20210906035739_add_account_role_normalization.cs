using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NETCore.Migrations
{
    public partial class add_account_role_normalization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_persons",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_persons", x => x.NIK);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_universities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_universities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_tr_accounts",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_accounts", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_tb_tr_accounts_tb_m_persons_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_m_persons",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_tr_educations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GPA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniversityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_educations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_tr_educations_tb_m_universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "tb_m_universities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_tr_accountroles",
                columns: table => new
                {
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_accountroles", x => new { x.AccountId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_tb_tr_accountroles_tb_m_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tb_m_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_tr_accountroles_tb_tr_accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "tb_tr_accounts",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_tr_profiling",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Education_Id = table.Column<int>(type: "int", nullable: false),
                    EducationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_profiling", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_tb_tr_profiling_tb_tr_accounts_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_tr_accounts",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_tr_profiling_tb_tr_educations_EducationId",
                        column: x => x.EducationId,
                        principalTable: "tb_tr_educations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_accountroles_RoleId",
                table: "tb_tr_accountroles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_educations_UniversityId",
                table: "tb_tr_educations",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_profiling_EducationId",
                table: "tb_tr_profiling",
                column: "EducationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_tr_accountroles");

            migrationBuilder.DropTable(
                name: "tb_tr_profiling");

            migrationBuilder.DropTable(
                name: "tb_m_roles");

            migrationBuilder.DropTable(
                name: "tb_tr_accounts");

            migrationBuilder.DropTable(
                name: "tb_tr_educations");

            migrationBuilder.DropTable(
                name: "tb_m_persons");

            migrationBuilder.DropTable(
                name: "tb_m_universities");
        }
    }
}
