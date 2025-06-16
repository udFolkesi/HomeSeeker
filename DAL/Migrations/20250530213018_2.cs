using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Announcements_AnnouncementId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ObjectDescriptionID",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "AnnouncementId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Announcements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Announcements_AnnouncementId",
                table: "Images",
                column: "AnnouncementId",
                principalTable: "Announcements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Announcements_AnnouncementId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Announcements");

            migrationBuilder.AlterColumn<int>(
                name: "AnnouncementId",
                table: "Images",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ObjectDescriptionID",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Announcements_AnnouncementId",
                table: "Images",
                column: "AnnouncementId",
                principalTable: "Announcements",
                principalColumn: "Id");
        }
    }
}
