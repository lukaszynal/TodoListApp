using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoListDal.Migrations
{
    public partial class FinalVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Tasks");

            migrationBuilder.AddColumn<bool>(
                name: "CompletedTasksVisible",
                table: "TodoLists",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "TodoLists",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasReminder",
                table: "Tasks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsListVisible",
                table: "Tasks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Tasks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Tasks",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReminderDuration",
                table: "Tasks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedTasksVisible",
                table: "TodoLists");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "TodoLists");

            migrationBuilder.DropColumn(
                name: "HasReminder",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "IsListVisible",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ReminderDuration",
                table: "Tasks");

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Tasks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
