using Microsoft.EntityFrameworkCore.Migrations;

namespace Rocky_DataAccess.Migrations
{
    public partial class ChangeID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ApplicationType_ApplicationTypeID",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "ApplicationTypeID",
                table: "Product",
                newName: "ApplicationTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ApplicationTypeID",
                table: "Product",
                newName: "IX_Product_ApplicationTypeId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ApplicationType",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ApplicationType_ApplicationTypeId",
                table: "Product",
                column: "ApplicationTypeId",
                principalTable: "ApplicationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ApplicationType_ApplicationTypeId",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "ApplicationTypeId",
                table: "Product",
                newName: "ApplicationTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ApplicationTypeId",
                table: "Product",
                newName: "IX_Product_ApplicationTypeID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ApplicationType",
                newName: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ApplicationType_ApplicationTypeID",
                table: "Product",
                column: "ApplicationTypeID",
                principalTable: "ApplicationType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
