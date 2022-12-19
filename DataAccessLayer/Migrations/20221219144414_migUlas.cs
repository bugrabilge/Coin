using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class migUlas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Users_UsersUserID",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_UsersUserID",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "UsersUserID",
                table: "Contacts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsersUserID",
                table: "Contacts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_UsersUserID",
                table: "Contacts",
                column: "UsersUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Users_UsersUserID",
                table: "Contacts",
                column: "UsersUserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
