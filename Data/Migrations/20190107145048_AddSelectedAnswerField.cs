using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddSelectedAnswerField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTrue",
                table: "Answers");

            migrationBuilder.AddColumn<Guid>(
                name: "SelectedAnswerId",
                table: "Questions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedAnswerId",
                table: "Questions");

            migrationBuilder.AddColumn<bool>(
                name: "IsTrue",
                table: "Answers",
                nullable: false,
                defaultValue: false);
        }
    }
}
