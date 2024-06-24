using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFDesigner2022Tests.Migrations
{
    /// <inheritdoc />
    public partial class EFModelDatabase_00240405 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "EntityChild",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameChild = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityChild", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityParent",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameParent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityParent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParentToChilds",
                schema: "dbo",
                columns: table => new
                {
                    EntityChild_Id = table.Column<long>(type: "bigint", nullable: false),
                    EntityParent_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentToChilds", x => new { x.EntityChild_Id, x.EntityParent_Id });
                    table.ForeignKey(
                        name: "FK_ParentToChilds_EntityChild_EntityChild_Id",
                        column: x => x.EntityChild_Id,
                        principalSchema: "dbo",
                        principalTable: "EntityChild",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParentToChilds_EntityParent_EntityParent_Id",
                        column: x => x.EntityParent_Id,
                        principalSchema: "dbo",
                        principalTable: "EntityParent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParentToChilds_EntityParent_Id",
                schema: "dbo",
                table: "ParentToChilds",
                column: "EntityParent_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParentToChilds",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EntityChild",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EntityParent",
                schema: "dbo");
        }
    }
}
