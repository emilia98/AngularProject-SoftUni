using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class UpdatePropertyModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Decription",
                table: "Properties",
                newName: "Description");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Properties",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_LocationId",
                table: "Properties",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Locations_LocationId",
                table: "Properties",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Locations_LocationId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_LocationId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Properties",
                newName: "Decription");
        }
    }
}
