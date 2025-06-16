using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class _3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ObjectDescriptions_Profile_ProfileId",
                table: "ObjectDescriptions");

            migrationBuilder.DropIndex(
                name: "IX_ObjectDescriptions_ProfileId",
                table: "ObjectDescriptions");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "ObjectDescriptions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "ObjectDescriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ObjectDescriptions_ProfileId",
                table: "ObjectDescriptions",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_ObjectDescriptions_Profile_ProfileId",
                table: "ObjectDescriptions",
                column: "ProfileId",
                principalTable: "Profile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
