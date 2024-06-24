using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFDesigner2022Tests.Migrations
{
    /// <inheritdoc />
    public partial class EFModelDatabase_00240417 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntityParent_x_EntityReference_EntityParent_EntityParent_Id",
                schema: "dbo",
                table: "EntityParent_x_EntityReference");

            migrationBuilder.DropForeignKey(
                name: "FK_EntityParent_x_EntityReference_EntityReference_EntityReference_Id",
                schema: "dbo",
                table: "EntityParent_x_EntityReference");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EntityParent_x_EntityReference",
                schema: "dbo",
                table: "EntityParent_x_EntityReference");

            migrationBuilder.RenameTable(
                name: "EntityParent_x_EntityReference",
                schema: "dbo",
                newName: "RefToParent",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_EntityParent_x_EntityReference_EntityReference_Id",
                schema: "dbo",
                table: "RefToParent",
                newName: "IX_RefToParent_EntityReference_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefToParent",
                schema: "dbo",
                table: "RefToParent",
                columns: new[] { "EntityParent_Id", "EntityReference_Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_RefToParent_EntityParent_EntityParent_Id",
                schema: "dbo",
                table: "RefToParent",
                column: "EntityParent_Id",
                principalSchema: "dbo",
                principalTable: "EntityParent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefToParent_EntityReference_EntityReference_Id",
                schema: "dbo",
                table: "RefToParent",
                column: "EntityReference_Id",
                principalSchema: "dbo",
                principalTable: "EntityReference",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefToParent_EntityParent_EntityParent_Id",
                schema: "dbo",
                table: "RefToParent");

            migrationBuilder.DropForeignKey(
                name: "FK_RefToParent_EntityReference_EntityReference_Id",
                schema: "dbo",
                table: "RefToParent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefToParent",
                schema: "dbo",
                table: "RefToParent");

            migrationBuilder.RenameTable(
                name: "RefToParent",
                schema: "dbo",
                newName: "EntityParent_x_EntityReference",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_RefToParent_EntityReference_Id",
                schema: "dbo",
                table: "EntityParent_x_EntityReference",
                newName: "IX_EntityParent_x_EntityReference_EntityReference_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntityParent_x_EntityReference",
                schema: "dbo",
                table: "EntityParent_x_EntityReference",
                columns: new[] { "EntityParent_Id", "EntityReference_Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_EntityParent_x_EntityReference_EntityParent_EntityParent_Id",
                schema: "dbo",
                table: "EntityParent_x_EntityReference",
                column: "EntityParent_Id",
                principalSchema: "dbo",
                principalTable: "EntityParent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EntityParent_x_EntityReference_EntityReference_EntityReference_Id",
                schema: "dbo",
                table: "EntityParent_x_EntityReference",
                column: "EntityReference_Id",
                principalSchema: "dbo",
                principalTable: "EntityReference",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
