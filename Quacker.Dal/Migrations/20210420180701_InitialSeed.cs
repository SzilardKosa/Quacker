using Microsoft.EntityFrameworkCore.Migrations;

namespace Quacker.Dal.Migrations
{
    public partial class InitialSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Picture" },
                values: new object[,]
                {
                    { 1, "What a vacation it was! Am i right?!", null },
                    { 2, "What a vacation it was", "https://cdn.britannica.com/82/195482-050-2373E635/Amalfi-Italy.jpg" },
                    { 3, "What a vacation it was", null },
                    { 4, "What a vacation it was", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
