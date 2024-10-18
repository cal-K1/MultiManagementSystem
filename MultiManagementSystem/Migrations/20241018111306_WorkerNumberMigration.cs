using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class WorkerNumberMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployeType",
                table: "EmployedWorkers",
                newName: "EmployeeType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployeeType",
                table: "EmployedWorkers",
                newName: "EmployeType");
        }
    }
}
