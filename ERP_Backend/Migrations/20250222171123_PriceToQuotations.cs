using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP_Backend.Migrations
{
    public partial class PriceToQuotations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Quotations",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Quotations");
        }
    }
}
