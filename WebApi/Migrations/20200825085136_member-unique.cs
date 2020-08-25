using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class memberunique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMembers_Projects_ProjectId",
                table: "ProjectMembers");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "ProjectMembers",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMembers_AccountId_ProjectId",
                table: "ProjectMembers",
                columns: new[] { "AccountId", "ProjectId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMembers_Accounts_AccountId",
                table: "ProjectMembers",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMembers_Projects_ProjectId",
                table: "ProjectMembers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMembers_Accounts_AccountId",
                table: "ProjectMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMembers_Projects_ProjectId",
                table: "ProjectMembers");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMembers_AccountId_ProjectId",
                table: "ProjectMembers");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "ProjectMembers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMembers_Projects_ProjectId",
                table: "ProjectMembers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
