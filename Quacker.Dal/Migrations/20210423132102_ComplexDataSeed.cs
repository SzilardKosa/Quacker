using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Quacker.Dal.Migrations
{
    public partial class ComplexDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Followings",
                columns: new[] { "FollowerId", "FollowedId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 5 },
                    { 1, 6 },
                    { 1, 7 }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Content", "CreationDate", "ReceiverId", "SenderId" },
                values: new object[,]
                {
                    { 1, "Hi man!", new DateTime(2021, 1, 9, 9, 15, 0, 0, DateTimeKind.Unspecified), 2, 1 },
                    { 2, "Heyyo!", new DateTime(2021, 1, 9, 9, 15, 0, 0, DateTimeKind.Unspecified), 1, 2 },
                    { 3, "How are you?", new DateTime(2021, 1, 9, 9, 15, 0, 0, DateTimeKind.Unspecified), 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "CreationDate", "HasImage", "UserId" },
                values: new object[,]
                {
                    { 1, "What a vacation it was!!", new DateTime(2021, 1, 9, 9, 15, 0, 0, DateTimeKind.Unspecified), false, 2 },
                    { 2, "What a vacation it was!! Here is a pic!", new DateTime(2021, 1, 9, 9, 15, 0, 0, DateTimeKind.Unspecified), true, 2 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreationDate", "PostId", "UserId" },
                values: new object[] { 1, "Well said", new DateTime(2021, 1, 9, 9, 15, 0, 0, DateTimeKind.Unspecified), 1, 1 });

            migrationBuilder.InsertData(
                table: "Likes",
                columns: new[] { "UserId", "PostId" },
                values: new object[] { 1, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Followings",
                keyColumns: new[] { "FollowerId", "FollowedId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "Followings",
                keyColumns: new[] { "FollowerId", "FollowedId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "Followings",
                keyColumns: new[] { "FollowerId", "FollowedId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "Followings",
                keyColumns: new[] { "FollowerId", "FollowedId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "Followings",
                keyColumns: new[] { "FollowerId", "FollowedId" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                table: "Followings",
                keyColumns: new[] { "FollowerId", "FollowedId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "Likes",
                keyColumns: new[] { "UserId", "PostId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
