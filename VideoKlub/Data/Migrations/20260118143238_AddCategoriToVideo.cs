using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoKlub.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoriToVideo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Videos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Videos_CategoryId",
                table: "Videos",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Categories_CategoryId",
                table: "Videos",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Categories_CategoryId",
                table: "Videos");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Videos_CategoryId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Videos");
        }
    }
}
