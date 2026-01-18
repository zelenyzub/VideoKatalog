using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoKlub.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveToVideos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Videos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Videos");
        }
    }
}
