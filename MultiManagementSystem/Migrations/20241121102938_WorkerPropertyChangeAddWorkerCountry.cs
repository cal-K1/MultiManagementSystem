using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class WorkerPropertyChangeAddWorkerCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Country",
                table: "Workers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Workers");
        }
    }
}
