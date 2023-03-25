using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICOVID19.Migrations
{
    /// <inheritdoc />
    public partial class adjustCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ISO3",
                table: "Country",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ISO3",
                table: "Country");
        }
    }
}
