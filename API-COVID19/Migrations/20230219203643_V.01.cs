using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICOVID19.Migrations
{
    /// <inheritdoc />
    public partial class V01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Combined_Key",
                table: "Country",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Combined_Key",
                table: "Country");
        }
    }
}
