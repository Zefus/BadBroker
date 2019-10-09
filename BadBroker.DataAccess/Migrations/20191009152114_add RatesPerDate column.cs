using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BadBroker.DataAccess.Migrations
{
    public partial class addRatesPerDatecolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rates",
                table: "RatesData");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RatesPerDate");

            migrationBuilder.AddColumn<string>(
                name: "Rates",
                table: "RatesData",
                nullable: true);
        }
    }
}
