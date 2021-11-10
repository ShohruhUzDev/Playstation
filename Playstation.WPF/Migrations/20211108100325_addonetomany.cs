using Microsoft.EntityFrameworkCore.Migrations;

namespace Playstation.WPF.Migrations
{
    public partial class addonetomany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TarrifId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TarrifId",
                table: "Orders",
                column: "TarrifId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Tarrifs_TarrifId",
                table: "Orders",
                column: "TarrifId",
                principalTable: "Tarrifs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Tarrifs_TarrifId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_TarrifId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TarrifId",
                table: "Orders");
        }
    }
}
