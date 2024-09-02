using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdministrationAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fees",
                columns: table => new
                {
                    FeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HourlyImbalanceFee = table.Column<float>(type: "real", nullable: true),
                    ImbalanceFee = table.Column<float>(type: "real", nullable: true),
                    PeakLoadFee = table.Column<float>(type: "real", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimestampUTC = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VolumeFee = table.Column<float>(type: "real", nullable: true),
                    WeeklyFee = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fees", x => x.FeeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fees");
        }
    }
}
