using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class WorkerMigrationRefactor6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create Workers table (it was already correct)
            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkerNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                });

            // Create the UserId table (seems to be related to the User class)
            migrationBuilder.CreateTable(
                name: "UserId", // Rename to "Users" for better clarity if it's the user table
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MonthlySalary = table.Column<int>(type: "int", nullable: false),
                    WeeklyHours = table.Column<int>(type: "int", nullable: false),
                    LeaveDaysRemaining = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserId", x => x.Id);
                });

            // Add WorkerId column to LeaveRequests table
            migrationBuilder.AddColumn<string>(
                name: "WorkerId",
                table: "LeaveRequests",
                nullable: true);  // Nullable or not based on your design

            // Create index on WorkerId for the LeaveRequests table
            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_WorkerId",
                table: "LeaveRequests",
                column: "WorkerId");

            // Add foreign key relationship to the Workers table
            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_Workers_WorkerId",
                table: "LeaveRequests",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_Workers_WorkerId",
                table: "LeaveRequests");

            migrationBuilder.DropTable(
                name: "UserId");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRequests_WorkerId",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "LeaveRequests");

            migrationBuilder.CreateTable(
                name: "ContractWorkers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContractEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeaveDaysRemaining = table.Column<int>(type: "int", nullable: false),
                    MonthlySalary = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeeklyHours = table.Column<int>(type: "int", nullable: false),
                    WorkerNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractWorkers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployedWorkers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeType = table.Column<int>(type: "int", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeaveDaysRemaining = table.Column<int>(type: "int", nullable: false),
                    MonthlySalary = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeeklyHours = table.Column<int>(type: "int", nullable: false),
                    WorkerNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployedWorkers", x => x.Id);
                });
        }
    }
}
