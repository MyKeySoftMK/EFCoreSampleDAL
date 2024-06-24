using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFDesigner2022Tests.Migrations
{
    /// <inheritdoc />
    public partial class EFModelDatabase_00240414 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntityReference",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameReference = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityReference", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityParent_x_EntityReference",
                schema: "dbo",
                columns: table => new
                {
                    EntityParent_Id = table.Column<long>(type: "bigint", nullable: false),
                    EntityReference_Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityParent_x_EntityReference", x => new { x.EntityParent_Id, x.EntityReference_Id });
                    table.ForeignKey(
                        name: "FK_EntityParent_x_EntityReference_EntityParent_EntityParent_Id",
                        column: x => x.EntityParent_Id,
                        principalSchema: "dbo",
                        principalTable: "EntityParent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntityParent_x_EntityReference_EntityReference_EntityReference_Id",
                        column: x => x.EntityReference_Id,
                        principalSchema: "dbo",
                        principalTable: "EntityReference",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityParent_x_EntityReference_EntityReference_Id",
                schema: "dbo",
                table: "EntityParent_x_EntityReference",
                column: "EntityReference_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityParent_x_EntityReference",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EntityReference",
                schema: "dbo");
        }
    }
}
