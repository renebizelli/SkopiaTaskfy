using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skopia.ReneBizelli.Taskfy._Shared.Migrations
{
    /// <inheritdoc />
    public partial class projectusersrefact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsUsers_Projects_ProjectId",
                table: "ProjectsUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsUsers_Users_UserId",
                table: "ProjectsUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ProjectsUsers",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "ProjectsUsers",
                newName: "ProjectsId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectsUsers_UserId",
                table: "ProjectsUsers",
                newName: "IX_ProjectsUsers_UsersId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsUsers_Projects_ProjectsId",
                table: "ProjectsUsers",
                column: "ProjectsId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsUsers_Users_UsersId",
                table: "ProjectsUsers",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsUsers_Projects_ProjectsId",
                table: "ProjectsUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsUsers_Users_UsersId",
                table: "ProjectsUsers");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "ProjectsUsers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ProjectsId",
                table: "ProjectsUsers",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectsUsers_UsersId",
                table: "ProjectsUsers",
                newName: "IX_ProjectsUsers_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsUsers_Projects_ProjectId",
                table: "ProjectsUsers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsUsers_Users_UserId",
                table: "ProjectsUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
