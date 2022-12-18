using Microsoft.EntityFrameworkCore.Migrations;

namespace Kindergarten2.Data.Migrations
{
    public partial class DeleteRestrict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Children_ECAs_ECAId",
                table: "Children");

            migrationBuilder.DropForeignKey(
                name: "FK_Children_Groups_GroupId",
                table: "Children");

            migrationBuilder.DropForeignKey(
                name: "FK_Children_Menus_MenuId",
                table: "Children");

            migrationBuilder.DropForeignKey(
                name: "FK_Children_Trips_TripId",
                table: "Children");

            migrationBuilder.AddForeignKey(
                name: "FK_Children_ECAs_ECAId",
                table: "Children",
                column: "ECAId",
                principalTable: "ECAs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Groups_GroupId",
                table: "Children",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Menus_MenuId",
                table: "Children",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Trips_TripId",
                table: "Children",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Children_ECAs_ECAId",
                table: "Children");

            migrationBuilder.DropForeignKey(
                name: "FK_Children_Groups_GroupId",
                table: "Children");

            migrationBuilder.DropForeignKey(
                name: "FK_Children_Menus_MenuId",
                table: "Children");

            migrationBuilder.DropForeignKey(
                name: "FK_Children_Trips_TripId",
                table: "Children");

            migrationBuilder.AddForeignKey(
                name: "FK_Children_ECAs_ECAId",
                table: "Children",
                column: "ECAId",
                principalTable: "ECAs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Groups_GroupId",
                table: "Children",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Menus_MenuId",
                table: "Children",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Trips_TripId",
                table: "Children",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
