using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICOVID19.Migrations
{
    /// <inheritdoc />
    public partial class FixFrequency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FrecuencyTypeId",
                table: "Frequency");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FrecuencyTypeId",
                table: "Frequency",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
