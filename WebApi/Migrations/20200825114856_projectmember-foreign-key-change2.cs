using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class projectmemberforeignkeychange2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_ProjectMembers_Accounts_AccountForeignKey",
            //    table: "ProjectMembers");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMembers_AccountForeignKey",
                table: "ProjectMembers");

            migrationBuilder.DropColumn(
                name: "AccountForeignKey",
                table: "ProjectMembers");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ProjectMembers_Accounts_AccountId",
            //    table: "ProjectMembers",
            //    column: "AccountId",
            //    principalTable: "Accounts",
            //    principalColumn: "AccountId",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_ProjectMembers_Accounts_AccountId",
            //    table: "ProjectMembers");

            migrationBuilder.AddColumn<int>(
                name: "AccountForeignKey",
                table: "ProjectMembers",
                type: "int",
                nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProjectMembers_AccountForeignKey",
            //    table: "ProjectMembers",
            //    column: "AccountForeignKey");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ProjectMembers_Accounts_AccountForeignKey",
            //    table: "ProjectMembers",
            //    column: "AccountForeignKey",
            //    principalTable: "Accounts",
            //    principalColumn: "AccountId",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
