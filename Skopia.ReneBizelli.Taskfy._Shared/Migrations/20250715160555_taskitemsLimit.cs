using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skopia.ReneBizelli.Taskfy._Shared.Migrations
{
    /// <inheritdoc />
    public partial class taskitemsLimit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaskLimit",
                table: "Projects",
                newName: "TaskItemsLimit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaskItemsLimit",
                table: "Projects",
                newName: "TaskLimit");
        }
    }
}
