﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICOVID19.Migrations
{
    /// <inheritdoc />
    public partial class AddPopulatedV101 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Populate",
                table: "Province_state",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Populate",
                table: "Country",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Populate",
                table: "Province_state");

            migrationBuilder.DropColumn(
                name: "Populate",
                table: "Country");
        }
    }
}
