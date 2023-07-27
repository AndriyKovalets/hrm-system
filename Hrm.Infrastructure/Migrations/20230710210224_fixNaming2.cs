using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hrm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixNaming2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Departments_DepartamentId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "DepartamentRole",
                table: "AspNetUsers",
                newName: "DepartmentRole");

            migrationBuilder.RenameColumn(
                name: "DepartamentId",
                table: "AspNetUsers",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_DepartamentId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Departments_DepartmentId",
                table: "AspNetUsers",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Departments_DepartmentId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "DepartmentRole",
                table: "AspNetUsers",
                newName: "DepartamentRole");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "AspNetUsers",
                newName: "DepartamentId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_DepartmentId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_DepartamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Departments_DepartamentId",
                table: "AspNetUsers",
                column: "DepartamentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}
