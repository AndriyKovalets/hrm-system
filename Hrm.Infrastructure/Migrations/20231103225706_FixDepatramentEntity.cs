using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hrm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixDepatramentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinEmployeeInVacation",
                table: "Departments",
                newName: "MinEmployeeNeed");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinEmployeeNeed",
                table: "Departments",
                newName: "MinEmployeeInVacation");
        }
    }
}
