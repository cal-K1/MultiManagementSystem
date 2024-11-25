using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_Admin_AdminId",
                table: "Company");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Admin",
                table: "Admin");

            migrationBuilder.RenameTable(
                name: "Admin",
                newName: "Administrator");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Administrator",
                newName: "Username");

            migrationBuilder.AlterColumn<string>(
                name: "AdminId",
                table: "Company",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Administrator",
                table: "Administrator",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Administrator_AdminId",
                table: "Company",
                column: "AdminId",
                principalTable: "Administrator",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_Administrator_AdminId",
                table: "Company");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Administrator",
                table: "Administrator");

            migrationBuilder.RenameTable(
                name: "Administrator",
                newName: "Admin");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Admin",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "AdminId",
                table: "Company",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admin",
                table: "Admin",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Admin_AdminId",
                table: "Company",
                column: "AdminId",
                principalTable: "Admin",
                principalColumn: "Id");
        }
    }
}
