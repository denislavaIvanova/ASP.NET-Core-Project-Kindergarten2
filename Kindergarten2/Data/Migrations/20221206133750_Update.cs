using Microsoft.EntityFrameworkCore.Migrations;

namespace Kindergarten2.Data.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Partners_PartnerId",
                table: "Teachers");

            migrationBuilder.AddColumn<int>(
                name: "PartnerId",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PartnerId",
                table: "Menus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PartnerId",
                table: "ECAs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_PartnerId",
                table: "Trips",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_PartnerId",
                table: "Menus",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ECAs_PartnerId",
                table: "ECAs",
                column: "PartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ECAs_Partners_PartnerId",
                table: "ECAs",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Partners_PartnerId",
                table: "Menus",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Partners_PartnerId",
                table: "Teachers",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Partners_PartnerId",
                table: "Trips",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ECAs_Partners_PartnerId",
                table: "ECAs");

            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Partners_PartnerId",
                table: "Menus");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Partners_PartnerId",
                table: "Teachers");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Partners_PartnerId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_PartnerId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Menus_PartnerId",
                table: "Menus");

            migrationBuilder.DropIndex(
                name: "IX_ECAs_PartnerId",
                table: "ECAs");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                table: "ECAs");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Partners_PartnerId",
                table: "Teachers",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
