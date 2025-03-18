using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TranDinhDuong_2280600533.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4b7a7a8a-6bba-76fd-1684-abc123abc123", null, "Admin", "ADMIN" },
                    { "7e0ab912-9508-48c2-83e6-8313d3c0d504", null, "Employee", "EMPLOYEE" },
                    { "8313d3c0-dd50-4fac-b9be-7777cccc1111", null, "Customer", "CUSTOMER" },
                    { "8318afc0-9b24-4600-b520-4444bbbb2222", null, "Company", "COMPANY" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b7a7a8a-6bba-76fd-1684-abc123abc123");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e0ab912-9508-48c2-83e6-8313d3c0d504");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8313d3c0-dd50-4fac-b9be-7777cccc1111");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8318afc0-9b24-4600-b520-4444bbbb2222");
        }
    }
}
