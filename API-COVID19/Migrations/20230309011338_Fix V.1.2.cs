using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICOVID19.Migrations
{
    /// <inheritdoc />
    public partial class FixV12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProvinceStateCaseId",
                table: "Cases");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProvinceStateCaseId",
                table: "Cases",
                type: "integer",
                nullable: true);
        }
    }
}
