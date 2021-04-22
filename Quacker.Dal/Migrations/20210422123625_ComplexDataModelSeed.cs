using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Quacker.Dal.Migrations
{
    public partial class ComplexDataModelSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Followings_Users_FollowerId",
                table: "Followings");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Users_UserId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_ReceiverId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Posts");

            migrationBuilder.AddColumn<bool>(
                name: "HasImage",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasImage",
                table: "Posts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "HasImage", "UserName" },
                values: new object[,]
                {
                    { 1, true, "Marsh Holland" },
                    { 18, true, "Albert French" },
                    { 17, true, "Gregory Walker" },
                    { 16, true, "Hughie Taylor" },
                    { 15, true, "Danielle Wintringham" },
                    { 14, true, "Pearl Moreno" },
                    { 13, true, "Marmaduke Schwartz" },
                    { 12, true, "Tabitha Dean" },
                    { 11, true, "Angela Gray" },
                    { 10, true, "Britney Abbott" },
                    { 9, true, "Neal Mack" },
                    { 8, true, "Alec Reid" },
                    { 7, true, "Gaylord Mcgee" },
                    { 6, true, "Shawn Curtis" },
                    { 5, true, "Yvette Goodman" },
                    { 4, true, "Frank Kelley" },
                    { 3, true, "Daisy Alvarado" },
                    { 2, true, "Minerva Harrett" },
                    { 19, true, "Duncan Barnes" },
                    { 20, true, "Beatrice Johnston" }
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Followings_Users_FollowerId",
                table: "Followings",
                column: "FollowerId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Users_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_ReceiverId",
                table: "Messages",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Followings_Users_FollowerId",
                table: "Followings");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Users_UserId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_ReceiverId",
                table: "Messages");

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
                table: "Users",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "HasImage",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "HasImage",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Followings_Users_FollowerId",
                table: "Followings",
                column: "FollowerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Users_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_ReceiverId",
                table: "Messages",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
