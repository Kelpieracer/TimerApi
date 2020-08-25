using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class projectmemberhasaccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountForeignKey",
                table: "ProjectMembers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMembers_AccountForeignKey",
                table: "ProjectMembers",
                column: "AccountForeignKey");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMembers_Accounts_AccountForeignKey",
                table: "ProjectMembers",
                column: "AccountForeignKey",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMembers_Accounts_AccountForeignKey",
                table: "ProjectMembers");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMembers_AccountForeignKey",
                table: "ProjectMembers");

            migrationBuilder.DropColumn(
                name: "AccountForeignKey",
                table: "ProjectMembers");
        }
    }
}
