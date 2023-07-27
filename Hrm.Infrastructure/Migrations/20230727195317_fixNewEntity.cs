using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hrm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixNewEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_AspNetUsers_CreatorId1",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_CreatorId1",
                table: "News");

            migrationBuilder.DropColumn(
                name: "CreatorId1",
                table: "News");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorId",
                table: "News",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_News_CreatorId",
                table: "News",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_AspNetUsers_CreatorId",
                table: "News",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_AspNetUsers_CreatorId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_CreatorId",
                table: "News");

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "News",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorId1",
                table: "News",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_News_CreatorId1",
                table: "News",
                column: "CreatorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_News_AspNetUsers_CreatorId1",
                table: "News",
                column: "CreatorId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
