using Microsoft.EntityFrameworkCore.Migrations;

namespace RestApiDemo.Infrastructure.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceContent_Resources_ResourceId",
                table: "ResourceContent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResourceContent",
                table: "ResourceContent");

            migrationBuilder.AlterColumn<int>(
                name: "ResourceId",
                table: "ResourceContent",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ResourceContent",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ResourceContent",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResourceContent",
                table: "ResourceContent",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceContent_Resources_ResourceId",
                table: "ResourceContent",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceContent_Resources_ResourceId",
                table: "ResourceContent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResourceContent",
                table: "ResourceContent");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ResourceContent");

            migrationBuilder.AlterColumn<int>(
                name: "ResourceId",
                table: "ResourceContent",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ResourceContent",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResourceContent",
                table: "ResourceContent",
                column: "Key");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceContent_Resources_ResourceId",
                table: "ResourceContent",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
