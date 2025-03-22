using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrazilSurvival.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class GameState_PlayerScoreAddedRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GameStateToken",
                table: "PlayerScores",
                type: "CHAR(36)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "GameStates",
                columns: table => new
                {
                    Token = table.Column<string>(type: "CHAR(36)", nullable: false),
                    Health = table.Column<int>(type: "INTEGER", nullable: false),
                    Money = table.Column<int>(type: "INTEGER", nullable: false),
                    Power = table.Column<int>(type: "INTEGER", nullable: false),
                    Score = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    EndedAt = table.Column<DateTime>(type: "TIMESTAMP", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStates", x => x.Token);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerScores_GameStateToken",
                table: "PlayerScores",
                column: "GameStateToken");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerScores_GameStates_Gam~",
                table: "PlayerScores",
                column: "GameStateToken",
                principalTable: "GameStates",
                principalColumn: "Token",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerScores_GameStates_Gam~",
                table: "PlayerScores");

            migrationBuilder.DropTable(
                name: "GameStates");

            migrationBuilder.DropIndex(
                name: "IX_PlayerScores_GameStateToken",
                table: "PlayerScores");

            migrationBuilder.DropColumn(
                name: "GameStateToken",
                table: "PlayerScores");
        }
    }
}
