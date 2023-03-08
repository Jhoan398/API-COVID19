using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace APICOVID19.Migrations
{
    /// <inheritdoc />
    public partial class NewmodelV10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_CountryCase_Report_CountryCaseId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_ProvinceStateCase_Report_ProvinceStateCaseReportId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccinateds_CountryCase_Report_CaseReportId",
                table: "Vaccinateds");

            migrationBuilder.DropTable(
                name: "CountryCase_Report");

            migrationBuilder.DropTable(
                name: "ProvinceStateCase_Report");

            migrationBuilder.DropIndex(
                name: "IX_Vaccinateds_CaseReportId",
                table: "Vaccinateds");

            migrationBuilder.DropColumn(
                name: "CaseReportId",
                table: "Vaccinateds");

            migrationBuilder.RenameColumn(
                name: "CountryCaseReportId",
                table: "Vaccinateds",
                newName: "CountryId");

            migrationBuilder.RenameColumn(
                name: "ProvinceStateCaseReportId",
                table: "Cases",
                newName: "ProvinceStateId");

            migrationBuilder.RenameColumn(
                name: "CountryCaseId",
                table: "Cases",
                newName: "CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Cases_ProvinceStateCaseReportId",
                table: "Cases",
                newName: "IX_Cases_ProvinceStateId");

            migrationBuilder.RenameIndex(
                name: "IX_Cases_CountryCaseId",
                table: "Cases",
                newName: "IX_Cases_CountryId");

            migrationBuilder.AddColumn<int>(
                name: "ProvinceStateCaseId",
                table: "Cases",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Frecuency_Type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Frecuency = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frecuency_Type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Worldmap_Data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worldmap_Data", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Frecuency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Deaths = table.Column<decimal>(type: "numeric", nullable: false),
                    Recovered = table.Column<decimal>(type: "numeric", nullable: false),
                    Active = table.Column<decimal>(type: "numeric", nullable: false),
                    DateReport = table.Column<int>(type: "integer", nullable: false),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    FrecuencyTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frecuency", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Frecuency_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Frecuency_Frecuency_Type_FrecuencyTypeId",
                        column: x => x.FrecuencyTypeId,
                        principalTable: "Frecuency_Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vaccinateds_CountryId",
                table: "Vaccinateds",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Frecuency_CountryId",
                table: "Frecuency",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Frecuency_FrecuencyTypeId",
                table: "Frecuency",
                column: "FrecuencyTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Country_CountryId",
                table: "Cases",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Province_state_ProvinceStateId",
                table: "Cases",
                column: "ProvinceStateId",
                principalTable: "Province_state",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccinateds_Country_CountryId",
                table: "Vaccinateds",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Country_CountryId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Province_state_ProvinceStateId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccinateds_Country_CountryId",
                table: "Vaccinateds");

            migrationBuilder.DropTable(
                name: "Frecuency");

            migrationBuilder.DropTable(
                name: "Worldmap_Data");

            migrationBuilder.DropTable(
                name: "Frecuency_Type");

            migrationBuilder.DropIndex(
                name: "IX_Vaccinateds_CountryId",
                table: "Vaccinateds");

            migrationBuilder.DropColumn(
                name: "ProvinceStateCaseId",
                table: "Cases");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "Vaccinateds",
                newName: "CountryCaseReportId");

            migrationBuilder.RenameColumn(
                name: "ProvinceStateId",
                table: "Cases",
                newName: "ProvinceStateCaseReportId");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "Cases",
                newName: "CountryCaseId");

            migrationBuilder.RenameIndex(
                name: "IX_Cases_ProvinceStateId",
                table: "Cases",
                newName: "IX_Cases_ProvinceStateCaseReportId");

            migrationBuilder.RenameIndex(
                name: "IX_Cases_CountryId",
                table: "Cases",
                newName: "IX_Cases_CountryCaseId");

            migrationBuilder.AddColumn<int>(
                name: "CaseReportId",
                table: "Vaccinateds",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CountryCase_Report",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    DateReport = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryCase_Report", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryCase_Report_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProvinceStateCase_Report",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProvinceStateId = table.Column<int>(type: "integer", nullable: false),
                    DateReport = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvinceStateCase_Report", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProvinceStateCase_Report_Province_state_ProvinceStateId",
                        column: x => x.ProvinceStateId,
                        principalTable: "Province_state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vaccinateds_CaseReportId",
                table: "Vaccinateds",
                column: "CaseReportId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryCase_Report_CountryId",
                table: "CountryCase_Report",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvinceStateCase_Report_ProvinceStateId",
                table: "ProvinceStateCase_Report",
                column: "ProvinceStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_CountryCase_Report_CountryCaseId",
                table: "Cases",
                column: "CountryCaseId",
                principalTable: "CountryCase_Report",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_ProvinceStateCase_Report_ProvinceStateCaseReportId",
                table: "Cases",
                column: "ProvinceStateCaseReportId",
                principalTable: "ProvinceStateCase_Report",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccinateds_CountryCase_Report_CaseReportId",
                table: "Vaccinateds",
                column: "CaseReportId",
                principalTable: "CountryCase_Report",
                principalColumn: "Id");
        }
    }
}
