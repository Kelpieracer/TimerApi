using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class ProjectMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMembers_Accounts_AccountId",
                table: "ProjectMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectMembers",
                table: "ProjectMembers");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "ProjectMembers");

            migrationBuilder.AddColumn<int>(
                name: "ProjectMemberId",
                table: "ProjectMembers",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectMembers",
                table: "ProjectMembers",
                column: "ProjectMemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectMembers",
                table: "ProjectMembers");

            migrationBuilder.DropColumn(
                name: "ProjectMemberId",
                table: "ProjectMembers");

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "ProjectMembers",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectMembers",
                table: "ProjectMembers",
                column: "MemberId");

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
