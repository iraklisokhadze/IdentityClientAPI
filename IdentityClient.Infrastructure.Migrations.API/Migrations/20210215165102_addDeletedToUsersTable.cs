using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityClient.Infrastructure.Migrations.API.Migrations
{
    public partial class addDeletedToUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Users");
        }
    }
}
