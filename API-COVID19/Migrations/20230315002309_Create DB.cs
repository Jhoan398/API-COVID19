﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace APICOVID19.Migrations
{
    /// <inheritdoc />
    public partial class CreateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CountryName = table.Column<string>(name: "Country_Name", type: "text", nullable: false),
                    CombinedKey = table.Column<string>(name: "Combined_Key", type: "text", nullable: false),
                    Populate = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Frequency_Type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Frequency = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frequency_Type", x => x.Id);
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
                name: "Province_state",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProvinceName = table.Column<string>(type: "text", nullable: false),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    Populate = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province_state", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Province_state_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vaccinateds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Dosis = table.Column<decimal>(type: "numeric", nullable: false),
                    AtLeastOneDosis = table.Column<decimal>(type: "numeric", nullable: false),
                    DateReport = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CountryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccinateds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vaccinateds_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Frequency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Deaths = table.Column<decimal>(type: "numeric", nullable: false),
                    Confirmed = table.Column<decimal>(type: "numeric", nullable: false),
                    Recovered = table.Column<decimal>(type: "numeric", nullable: false),
                    DateReport = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    FrecuencyTypeId = table.Column<int>(type: "integer", nullable: false),
                    FrequencyTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frequency", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Frequency_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Frequency_Frequency_Type_FrequencyTypeId",
                        column: x => x.FrequencyTypeId,
                        principalTable: "Frequency_Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Deaths = table.Column<decimal>(type: "numeric", nullable: false),
                    Confirmed = table.Column<decimal>(type: "numeric", nullable: false),
                    Recovered = table.Column<decimal>(type: "numeric", nullable: false),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    ProvinceStateId = table.Column<int>(type: "integer", nullable: false),
                    DateReport = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cases_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cases_Province_state_ProvinceStateId",
                        column: x => x.ProvinceStateId,
                        principalTable: "Province_state",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CountryId",
                table: "Cases",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_ProvinceStateId",
                table: "Cases",
                column: "ProvinceStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Frequency_CountryId",
                table: "Frequency",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Frequency_FrequencyTypeId",
                table: "Frequency",
                column: "FrequencyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Province_state_CountryId",
                table: "Province_state",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccinateds_CountryId",
                table: "Vaccinateds",
                column: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "Frequency");

            migrationBuilder.DropTable(
                name: "Vaccinateds");

            migrationBuilder.DropTable(
                name: "Worldmap_Data");

            migrationBuilder.DropTable(
                name: "Province_state");

            migrationBuilder.DropTable(
                name: "Frequency_Type");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
