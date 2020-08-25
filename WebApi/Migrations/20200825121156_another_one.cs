using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class another_one : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_ProjectMembers_Accounts_AccountId",
            //    table: "ProjectMembers");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMembers_AccountId",
                table: "ProjectMembers");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "ProjectMembers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "ProjectMembers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMembers_AccountId",
                table: "ProjectMembers",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMembers_Accounts_AccountId",
                table: "ProjectMembers",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
