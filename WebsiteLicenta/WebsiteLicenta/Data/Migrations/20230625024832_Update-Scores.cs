using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebsiteLicenta.Data.Migrations
{
    public partial class UpdateScores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "BoardItem");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "BoardItem",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
