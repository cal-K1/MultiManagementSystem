using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class WorkerMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "EmployedWorkers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LeaveDaysRemaining",
                table: "EmployedWorkers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MonthlySalary",
                table: "EmployedWorkers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WeeklyHours",
                table: "EmployedWorkers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WorkerNumber",
                table: "EmployedWorkers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "ContractWorkers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LeaveDaysRemaining",
                table: "ContractWorkers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MonthlySalary",
                table: "ContractWorkers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WeeklyHours",
                table: "ContractWorkers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WorkerNumber",
                table: "ContractWorkers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "EmployedWorkers");

            migrationBuilder.DropColumn(
                name: "LeaveDaysRemaining",
                table: "EmployedWorkers");

            migrationBuilder.DropColumn(
                name: "MonthlySalary",
                table: "EmployedWorkers");

            migrationBuilder.DropColumn(
                name: "WeeklyHours",
                table: "EmployedWorkers");

            migrationBuilder.DropColumn(
                name: "WorkerNumber",
                table: "EmployedWorkers");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "ContractWorkers");

            migrationBuilder.DropColumn(
                name: "LeaveDaysRemaining",
                table: "ContractWorkers");

            migrationBuilder.DropColumn(
                name: "MonthlySalary",
                table: "ContractWorkers");

            migrationBuilder.DropColumn(
                name: "WeeklyHours",
                table: "ContractWorkers");

            migrationBuilder.DropColumn(
                name: "WorkerNumber",
                table: "ContractWorkers");
        }
    }
}
