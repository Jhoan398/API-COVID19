using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace APICOVID19.Migrations
{
    /// <inheritdoc />
    public partial class V02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Country_data_covid");

            migrationBuilder.CreateTable(
                name: "Data_Covid",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Infected = table.Column<decimal>(type: "numeric", nullable: false),
                    Deads = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalCases = table.Column<decimal>(name: "Total_Cases", type: "numeric", nullable: false),
                    DateReport = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProvinceStateId = table.Column<int>(type: "integer", nullable: false),
                    CountryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Data_Covid", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Data_Covid_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Data_Covid_Province_state_ProvinceStateId",
                        column: x => x.ProvinceStateId,
                        principalTable: "Province_state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Data_Covid_CountryId",
                table: "Data_Covid",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Data_Covid_ProvinceStateId",
                table: "Data_Covid",
                column: "ProvinceStateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Data_Covid");

            migrationBuilder.CreateTable(
                name: "Country_data_covid",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    ProvinceStateId = table.Column<int>(type: "integer", nullable: false),
                    DateReport = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deads = table.Column<decimal>(type: "numeric", nullable: false),
                    Infected = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalCases = table.Column<decimal>(name: "Total_Cases", type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country_data_covid", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Country_data_covid_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Country_data_covid_Province_state_ProvinceStateId",
                        column: x => x.ProvinceStateId,
                        principalTable: "Province_state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Country_data_covid_CountryId",
                table: "Country_data_covid",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_data_covid_ProvinceStateId",
                table: "Country_data_covid",
                column: "ProvinceStateId");
        }
    }
}
