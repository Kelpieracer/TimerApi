using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class SomeTweaksToIdFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Accounts_ManagerId",
                table: "Topics");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_Projects_ProjectId",
                table: "WorkItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_Rates_RateId",
                table: "WorkItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_Topics_TopicsTopicId",
                table: "WorkItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_Accounts_WorkerId",
                table: "WorkItems");

            migrationBuilder.DropIndex(
                name: "IX_WorkItems_RateId",
                table: "WorkItems");

            migrationBuilder.DropIndex(
                name: "IX_WorkItems_TopicsTopicId",
                table: "WorkItems");

            migrationBuilder.DropIndex(
                name: "IX_WorkItems_WorkerId",
                table: "WorkItems");

            migrationBuilder.DropIndex(
                name: "IX_Topics_ManagerId",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "TopicsTopicId",
                table: "WorkItems");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "WorkItems");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Topics");

            migrationBuilder.AlterColumn<int>(
                name: "RateId",
                table: "WorkItems",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "WorkItems",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "WorkItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TopicId",
                table: "WorkItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Topics",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_Projects_ProjectId",
                table: "WorkItems",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_Projects_ProjectId",
                table: "WorkItems");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "WorkItems");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "WorkItems");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Topics");

            migrationBuilder.AlterColumn<int>(
                name: "RateId",
                table: "WorkItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "WorkItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "TopicsTopicId",
                table: "WorkItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkerId",
                table: "WorkItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Topics",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkItems_RateId",
                table: "WorkItems",
                column: "RateId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItems_TopicsTopicId",
                table: "WorkItems",
                column: "TopicsTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItems_WorkerId",
                table: "WorkItems",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_ManagerId",
                table: "Topics",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Accounts_ManagerId",
                table: "Topics",
                column: "ManagerId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_Projects_ProjectId",
                table: "WorkItems",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_Rates_RateId",
                table: "WorkItems",
                column: "RateId",
                principalTable: "Rates",
                principalColumn: "RateId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_Topics_TopicsTopicId",
                table: "WorkItems",
                column: "TopicsTopicId",
                principalTable: "Topics",
                principalColumn: "TopicId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_Accounts_WorkerId",
                table: "WorkItems",
                column: "WorkerId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
