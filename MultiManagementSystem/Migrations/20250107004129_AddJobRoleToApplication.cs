using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddJobRoleToApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JobRoleId",
                table: "JobApplications",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_JobRoleId",
                table: "JobApplications",
                column: "JobRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobRole_JobRoleId",
                table: "JobApplications",
                column: "JobRoleId",
                principalTable: "JobRole",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobRole_JobRoleId",
                table: "JobApplications");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_JobRoleId",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "JobRoleId",
                table: "JobApplications");
        }
    }
}
