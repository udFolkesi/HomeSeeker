using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Profile_ProfileId",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Announcements");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "Announcements",
                newName: "ProfileID");

            migrationBuilder.RenameIndex(
                name: "IX_Announcements_ProfileId",
                table: "Announcements",
                newName: "IX_Announcements_ProfileID");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileID",
                table: "Announcements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Profile_ProfileID",
                table: "Announcements",
                column: "ProfileID",
                principalTable: "Profile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Profile_ProfileID",
                table: "Announcements");

            migrationBuilder.RenameColumn(
                name: "ProfileID",
                table: "Announcements",
                newName: "ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Announcements_ProfileID",
                table: "Announcements",
                newName: "IX_Announcements_ProfileId");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileId",
                table: "Announcements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Announcements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Profile_ProfileId",
                table: "Announcements",
                column: "ProfileId",
                principalTable: "Profile",
                principalColumn: "Id");
        }
    }
}
