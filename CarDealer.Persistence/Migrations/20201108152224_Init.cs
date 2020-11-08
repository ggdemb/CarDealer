using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarDealer.Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarStates",
                columns: table => new
                {
                    Id = table.Column<byte>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Brand = table.Column<string>(nullable: true),
                    Name_Model = table.Column<string>(nullable: true),
                    StateId = table.Column<byte>(nullable: false),
                    Engine_Type = table.Column<int>(nullable: true),
                    Engine_EuroStandard_Value = table.Column<int>(nullable: true),
                    Engine_EngineCapacity_DisplacementInCm3 = table.Column<decimal>(nullable: true),
                    Engine_BatteryCapacity_CapacityInKwh = table.Column<decimal>(nullable: true),
                    Transmission = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    CurrentMileage_MileageInKm = table.Column<int>(nullable: true),
                    BasePrice_Amount = table.Column<decimal>(nullable: true),
                    IsReserved = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true)
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
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfItem = table.Column<DateTime>(nullable: false),
                    Mileage_MileageInKm = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AvailibleCarId = table.Column<long>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true)
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
        }
    }
}
