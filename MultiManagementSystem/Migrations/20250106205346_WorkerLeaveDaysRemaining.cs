using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class WorkerLeaveDaysRemaining : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_Workers_WorkerId",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "Notifications",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "WorkerName",
                table: "LeaveRequests");

            migrationBuilder.AddColumn<int>(
                name: "LeaveDaysRemaining",
                table: "Workers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "WorkerId",
                table: "LeaveRequests",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_Workers_WorkerId",
                table: "LeaveRequests",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_Workers_WorkerId",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "LeaveDaysRemaining",
                table: "Workers");

            migrationBuilder.AddColumn<string>(
                name: "Notifications",
                table: "Workers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "WorkerId",
                table: "LeaveRequests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkerName",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_Workers_WorkerId",
                table: "LeaveRequests",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
