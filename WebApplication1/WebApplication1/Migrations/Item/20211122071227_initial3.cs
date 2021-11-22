using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations.Item
{
    public partial class initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DbItem",
                columns: table => new
                {
                    ItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RenderingEngine = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Browser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Platform = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EngineVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CSSGrade = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbItem", x => x.ItemID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbItem");
        }
    }
}
