using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class more_FKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMembers_Projects_ProjectId",
                table: "ProjectMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_TopicsForProject_Projects_ProjectId",
                table: "TopicsForProject");

            migrationBuilder.DropForeignKey(
                name: "FK_TopicsForProject_Topics_TopicId",
                table: "TopicsForProject");

            migrationBuilder.AddColumn<int>(
                name: "TopicId",
                table: "WorkItems",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TopicId",
                table: "TopicsForProject",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "TopicsForProject",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "TopicsForProject",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "ProjectMembers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItems_TopicId",
                table: "WorkItems",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicsForProject_AccountId",
                table: "TopicsForProject",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMembers_Projects_ProjectId",
                table: "ProjectMembers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TopicsForProject_Accounts_AccountId",
                table: "TopicsForProject",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TopicsForProject_Projects_ProjectId",
                table: "TopicsForProject",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TopicsForProject_Topics_TopicId",
                table: "TopicsForProject",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "TopicId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_Topics_TopicId",
                table: "WorkItems",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "TopicId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMembers_Projects_ProjectId",
                table: "ProjectMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_TopicsForProject_Accounts_AccountId",
                table: "TopicsForProject");

            migrationBuilder.DropForeignKey(
                name: "FK_TopicsForProject_Projects_ProjectId",
                table: "TopicsForProject");

            migrationBuilder.DropForeignKey(
                name: "FK_TopicsForProject_Topics_TopicId",
                table: "TopicsForProject");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_Topics_TopicId",
                table: "WorkItems");

            migrationBuilder.DropIndex(
                name: "IX_WorkItems_TopicId",
                table: "WorkItems");

            migrationBuilder.DropIndex(
                name: "IX_TopicsForProject_AccountId",
                table: "TopicsForProject");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "WorkItems");

            migrationBuilder.AlterColumn<int>(
                name: "TopicId",
                table: "TopicsForProject",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "TopicsForProject",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "TopicsForProject",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "ProjectMembers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMembers_Projects_ProjectId",
                table: "ProjectMembers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TopicsForProject_Projects_ProjectId",
                table: "TopicsForProject",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TopicsForProject_Topics_TopicId",
                table: "TopicsForProject",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "TopicId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
