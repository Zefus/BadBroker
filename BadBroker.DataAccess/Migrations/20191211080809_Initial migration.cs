using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BadBroker.DataAccess.Migrations
{
    public partial class Initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RatesData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatesData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RatesPerDate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Rate = table.Column<decimal>(nullable: false),
                    RatesDataId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatesPerDate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatesPerDate_RatesData_RatesDataId",
                        column: x => x.RatesDataId,
                        principalTable: "RatesData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RatesPerDate_RatesDataId",
                table: "RatesPerDate",
                column: "RatesDataId");

            migrationBuilder.CreateIndex(
                name: "IX_RatesData_Date",
                table: "RatesData",
                column: "Date",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RatesPerDate");

            migrationBuilder.DropTable(
                name: "RatesData");
        }
    }
}
