using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddCompanyTable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Company");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Company",
                newName: "CompanyName");

            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Company",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Company_AdminId",
                table: "Company",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Admin_AdminId",
                table: "Company",
                column: "AdminId",
                principalTable: "Admin",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_Admin_AdminId",
                table: "Company");

            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropIndex(
                name: "IX_Company_AdminId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Company");

            migrationBuilder.RenameColumn(
                name: "CompanyName",
                table: "Company",
                newName: "Password");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Company",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
