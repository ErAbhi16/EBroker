using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System.Diagnostics.CodeAnalysis;

namespace EBroker.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UnitPrice = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Funds = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TraderHolding",
                columns: table => new
                {
                    EquityId = table.Column<int>(type: "integer", nullable: false),
                    TraderId = table.Column<int>(type: "integer", nullable: false),
                    UnitHoldings = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraderHolding", x => new { x.TraderId, x.EquityId });
                    table.ForeignKey(
                        name: "FK_TraderHolding_Equity_EquityId",
                        column: x => x.EquityId,
                        principalTable: "Equity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TraderHolding_Trader_TraderId",
                        column: x => x.TraderId,
                        principalTable: "Trader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TraderTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EquityId = table.Column<int>(type: "integer", nullable: false),
                    TraderId = table.Column<int>(type: "integer", nullable: false),
                    TransactionUnits = table.Column<int>(type: "integer", nullable: false),
                    TraderAction = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraderTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TraderTransaction_Equity_EquityId",
                        column: x => x.EquityId,
                        principalTable: "Equity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TraderTransaction_Trader_TraderId",
                        column: x => x.TraderId,
                        principalTable: "Trader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Equity",
                columns: new[] { "Id", "Name", "UnitPrice" },
                values: new object[,]
                {
                    { 12, "Tata Motors", 445.55000000000001 },
                    { 14, "Indusind Bank Ltd", 848.0 },
                    { 15, "HDFC Bank", 1424.5 },
                    { 23, "Reliance Industries Ltd", 2276.8000000000002 },
                    { 45, "Rain Industries Ltd", 200.5 },
                    { 67, "Goa Carbon Ltd", 311.94999999999999 }
                });

            migrationBuilder.InsertData(
                table: "Trader",
                columns: new[] { "Id", "Funds", "Name" },
                values: new object[,]
                {
                    { 1, 4500.5, "Abhi" },
                    { 2, 1234.0, "Saurav" },
                    { 3, 4500.5, "Ashwani" },
                    { 4, 2319.0, "GuptaJi" },
                    { 5, 8344.0, "Arshbeer" },
                    { 6, 9000.0, "Saurav" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TraderHolding_EquityId",
                table: "TraderHolding",
                column: "EquityId");

            migrationBuilder.CreateIndex(
                name: "IX_TraderTransaction_EquityId",
                table: "TraderTransaction",
                column: "EquityId");

            migrationBuilder.CreateIndex(
                name: "IX_TraderTransaction_TraderId",
                table: "TraderTransaction",
                column: "TraderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TraderHolding");

            migrationBuilder.DropTable(
                name: "TraderTransaction");

            migrationBuilder.DropTable(
                name: "Equity");

            migrationBuilder.DropTable(
                name: "Trader");
        }
    }
}
