using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CBZ_OverTime_Logging.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate006 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserConnectId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserConnectId",
                table: "Employees");
        }
    }
}
