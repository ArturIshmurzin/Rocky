using Microsoft.EntityFrameworkCore.Migrations;

namespace Rocky_DataAccess.Migrations
{
    public partial class AddApplicationTypeInProductModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationTypeID",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ApplicationTypeID",
                table: "Product",
                column: "ApplicationTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ApplicationType_ApplicationTypeID",
                table: "Product",
                column: "ApplicationTypeID",
                principalTable: "ApplicationType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ApplicationType_ApplicationTypeID",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ApplicationTypeID",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ApplicationTypeID",
                table: "Product");
        }
    }
}
