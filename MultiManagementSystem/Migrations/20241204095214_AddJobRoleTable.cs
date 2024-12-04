using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddJobRoleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobRole_Description",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "JobRole_JobTitle",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "JobRole_Salary",
                table: "Workers");

            migrationBuilder.AddColumn<string>(
                name: "JobRoleId",
                table: "Workers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "JobRole",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRole", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobRole");

            migrationBuilder.DropColumn(
                name: "JobRoleId",
                table: "Workers");

            migrationBuilder.AddColumn<string>(
                name: "JobRole_Description",
                table: "Workers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobRole_JobTitle",
                table: "Workers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobRole_Salary",
                table: "Workers",
                type: "int",
                nullable: true);
        }
    }
}
