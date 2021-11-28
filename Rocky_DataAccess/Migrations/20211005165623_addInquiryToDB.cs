using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rocky_DataAccess.Migrations
{
    public partial class addInquiryToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InquiryHeader",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    InquiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquiryHeader", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InquiryHeader_AspNetUsers_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InquiryDetail",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InquiryHeaderID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquiryDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InquiryDetail_InquiryHeader_InquiryHeaderID",
                        column: x => x.InquiryHeaderID,
                        principalTable: "InquiryHeader",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InquiryDetail_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InquiryDetail_InquiryHeaderID",
                table: "InquiryDetail",
                column: "InquiryHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryDetail_ProductID",
                table: "InquiryDetail",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryHeader_ApplicationUserID",
                table: "InquiryHeader",
                column: "ApplicationUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InquiryDetail");

            migrationBuilder.DropTable(
                name: "InquiryHeader");
        }
    }
}
