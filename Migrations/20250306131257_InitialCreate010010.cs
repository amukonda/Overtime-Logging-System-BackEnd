using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CBZ_OverTime_Logging.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate010010 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vw_Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserConnect_Id = table.Column<int>(type: "int", nullable: false),
                    Employee_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subsidiary_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vw_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "vw_OverTimeClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserConnect_Id = table.Column<int>(type: "int", nullable: false),
                    Employee_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: true),
                    Subsidiary_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Employee_Id = table.Column<int>(type: "int", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OvertimeHours = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LineManagerApprovalStatus = table.Column<bool>(type: "bit", nullable: false),
                    LineManagerComments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadApprovalStatus = table.Column<bool>(type: "bit", nullable: false),
                    HeadComments = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vw_OverTimeClaims", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vw_Employees");

            migrationBuilder.DropTable(
                name: "vw_OverTimeClaims");
        }
    }
}
