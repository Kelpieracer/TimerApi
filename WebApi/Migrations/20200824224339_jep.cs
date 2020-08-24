using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class jep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_Rates_RateId",
                table: "WorkItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_Topics_TopicId",
                table: "WorkItems");

            migrationBuilder.DropIndex(
                name: "IX_WorkItems_RateId",
                table: "WorkItems");

            migrationBuilder.DropIndex(
                name: "IX_WorkItems_TopicId",
                table: "WorkItems");

            migrationBuilder.DropColumn(
                name: "RateId",
                table: "WorkItems");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "WorkItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RateId",
                table: "WorkItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TopicId",
                table: "WorkItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkItems_RateId",
                table: "WorkItems",
                column: "RateId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItems_TopicId",
                table: "WorkItems",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_Rates_RateId",
                table: "WorkItems",
                column: "RateId",
                principalTable: "Rates",
                principalColumn: "RateId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_Topics_TopicId",
                table: "WorkItems",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "TopicId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
