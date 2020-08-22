using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class RemovedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Projects_ProjectId",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Account_ManagerId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Account_ManagerId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_Account_AccountId",
                table: "RefreshToken");

            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Account_ManagerId",
                table: "Topics");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_Account_WorkerId",
                table: "WorkItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.RenameTable(
                name: "Account",
                newName: "Accounts");

            migrationBuilder.RenameIndex(
                name: "IX_Account_ProjectId",
                table: "Accounts",
                newName: "IX_Accounts_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Projects_ProjectId",
                table: "Accounts",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Accounts_ManagerId",
                table: "Customers",
                column: "ManagerId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Accounts_ManagerId",
                table: "Projects",
                column: "ManagerId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_Accounts_AccountId",
                table: "RefreshToken",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Accounts_ManagerId",
                table: "Topics",
                column: "ManagerId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_Accounts_WorkerId",
                table: "WorkItems",
                column: "WorkerId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Projects_ProjectId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Accounts_ManagerId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Accounts_ManagerId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_Accounts_AccountId",
                table: "RefreshToken");

            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Accounts_ManagerId",
                table: "Topics");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_Accounts_WorkerId",
                table: "WorkItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "Account");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_ProjectId",
                table: "Account",
                newName: "IX_Account_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Projects_ProjectId",
                table: "Account",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Account_ManagerId",
                table: "Customers",
                column: "ManagerId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Account_ManagerId",
                table: "Projects",
                column: "ManagerId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_Account_AccountId",
                table: "RefreshToken",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Account_ManagerId",
                table: "Topics",
                column: "ManagerId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_Account_WorkerId",
                table: "WorkItems",
                column: "WorkerId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
