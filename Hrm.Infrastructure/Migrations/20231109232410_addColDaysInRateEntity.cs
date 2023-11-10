using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hrm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addColDaysInRateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Days",
                table: "VacationRates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Days",
                table: "VacationRates");
        }
    }
}
