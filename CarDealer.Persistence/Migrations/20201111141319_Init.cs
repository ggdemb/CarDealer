using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarDealer.Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "AvailibleCarHiLoSequence",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "CarStates",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name_Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateId = table.Column<byte>(type: "tinyint", nullable: false),
                    Engine_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Engine_EuroStandard_Value = table.Column<int>(type: "int", nullable: true),
                    Engine_EngineCapacity_DisplacementInCm3 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Engine_BatteryCapacity_CapacityInKwh = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Transmission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentMileage_MileageInKm = table.Column<int>(type: "int", nullable: true),
                    BasePrice_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsReserved = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_CarStates_StateId",
                        column: x => x.StateId,
                        principalTable: "CarStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarHistoryItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfItem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Mileage_MileageInKm = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvailibleCarId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarHistoryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarHistoryItems_Cars_AvailibleCarId",
                        column: x => x.AvailibleCarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarHistoryItems_AvailibleCarId",
                table: "CarHistoryItems",
                column: "AvailibleCarId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_StateId",
                table: "Cars",
                column: "StateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarHistoryItems");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "CarStates");

            migrationBuilder.DropSequence(
                name: "AvailibleCarHiLoSequence");
        }
    }
}
