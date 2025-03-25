using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranDinhDuong_2280600533.Migrations
{
    /// <inheritdoc />
    public partial class themImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "ProductImages",
                newName: "Url");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "ProductImages",
                newName: "ImageUrl");
        }
    }
}
