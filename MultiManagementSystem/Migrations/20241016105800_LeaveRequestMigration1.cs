using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class LeaveRequestMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Manager",
                table: "Manager");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobApplication",
                table: "JobApplication");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployedWorker",
                table: "EmployedWorker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractWorker",
                table: "ContractWorker");

            migrationBuilder.RenameTable(
                name: "Manager",
                newName: "Managers");

            migrationBuilder.RenameTable(
                name: "JobApplication",
                newName: "JobApplications");

            migrationBuilder.RenameTable(
                name: "EmployedWorker",
                newName: "EmployedWorkers");

            migrationBuilder.RenameTable(
                name: "ContractWorker",
                newName: "ContractWorkers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Managers",
                table: "Managers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobApplications",
                table: "JobApplications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployedWorkers",
                table: "EmployedWorkers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractWorkers",
                table: "ContractWorkers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "LeaveRequests",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequests", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Managers",
                table: "Managers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobApplications",
                table: "JobApplications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployedWorkers",
                table: "EmployedWorkers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractWorkers",
                table: "ContractWorkers");

            migrationBuilder.RenameTable(
                name: "Managers",
                newName: "Manager");

            migrationBuilder.RenameTable(
                name: "JobApplications",
                newName: "JobApplication");

            migrationBuilder.RenameTable(
                name: "EmployedWorkers",
                newName: "EmployedWorker");

            migrationBuilder.RenameTable(
                name: "ContractWorkers",
                newName: "ContractWorker");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Manager",
                table: "Manager",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobApplication",
                table: "JobApplication",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployedWorker",
                table: "EmployedWorker",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractWorker",
                table: "ContractWorker",
                column: "Id");
        }
    }
}
