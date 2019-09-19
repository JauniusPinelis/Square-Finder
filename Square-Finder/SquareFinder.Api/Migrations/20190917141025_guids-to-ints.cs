using Microsoft.EntityFrameworkCore.Migrations;

namespace SquareFinder.Api.Migrations
{
    public partial class guidstoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Points_PointLists_PointListId",
                table: "Points");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Points",
                table: "Points");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PointLists",
                table: "PointLists");

            migrationBuilder.RenameTable(
                name: "Points",
                newName: "Point");

            migrationBuilder.RenameTable(
                name: "PointLists",
                newName: "PointList");

            migrationBuilder.RenameIndex(
                name: "IX_Points_PointListId",
                table: "Point",
                newName: "IX_Point_PointListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Point",
                table: "Point",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PointList",
                table: "PointList",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Point_PointList_PointListId",
                table: "Point",
                column: "PointListId",
                principalTable: "PointList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Point_PointList_PointListId",
                table: "Point");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PointList",
                table: "PointList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Point",
                table: "Point");

            migrationBuilder.RenameTable(
                name: "PointList",
                newName: "PointLists");

            migrationBuilder.RenameTable(
                name: "Point",
                newName: "Points");

            migrationBuilder.RenameIndex(
                name: "IX_Point_PointListId",
                table: "Points",
                newName: "IX_Points_PointListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PointLists",
                table: "PointLists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Points",
                table: "Points",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Points_PointLists_PointListId",
                table: "Points",
                column: "PointListId",
                principalTable: "PointLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
