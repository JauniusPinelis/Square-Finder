using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SquareFinder.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SquareEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SquareEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PointList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    SquareId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointList_SquareEntity_SquareId",
                        column: x => x.SquareId,
                        principalTable: "SquareEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Point",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    X = table.Column<int>(nullable: false),
                    Y = table.Column<int>(nullable: false),
                    PointListId = table.Column<int>(nullable: false),
                    SquareEntityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Point", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Point_PointList_PointListId",
                        column: x => x.PointListId,
                        principalTable: "PointList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Point_SquareEntity_SquareEntityId",
                        column: x => x.SquareEntityId,
                        principalTable: "SquareEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Point_PointListId",
                table: "Point",
                column: "PointListId");

            migrationBuilder.CreateIndex(
                name: "IX_Point_SquareEntityId",
                table: "Point",
                column: "SquareEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_PointList_SquareId",
                table: "PointList",
                column: "SquareId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Point");

            migrationBuilder.DropTable(
                name: "PointList");

            migrationBuilder.DropTable(
                name: "SquareEntity");
        }
    }
}
