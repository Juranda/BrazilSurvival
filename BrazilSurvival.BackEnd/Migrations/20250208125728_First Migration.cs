using FirebirdSql.EntityFrameworkCore.Firebird.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrazilSurvival.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Challenges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerScores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "VARCHAR(6)", maxLength: 6, nullable: false),
                    Score = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerScores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChallengeOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn),
                    Action = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    ChallengeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChallengeOption_Challenges_~",
                        column: x => x.ChallengeId,
                        principalTable: "Challenges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChallengeOptionConsequence",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn),
                    Answer = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Consequence = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: false),
                    Health = table.Column<int>(type: "INTEGER", nullable: true),
                    Money = table.Column<int>(type: "INTEGER", nullable: true),
                    Power = table.Column<int>(type: "INTEGER", nullable: true),
                    ChallengeOptionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeOptionConsequence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChallengeOptionConsequence_~",
                        column: x => x.ChallengeOptionId,
                        principalTable: "ChallengeOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeOption_ChallengeId",
                table: "ChallengeOption",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeOptionConsequence_~",
                table: "ChallengeOptionConsequence",
                column: "ChallengeOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerScores_Name",
                table: "PlayerScores",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChallengeOptionConsequence");

            migrationBuilder.DropTable(
                name: "PlayerScores");

            migrationBuilder.DropTable(
                name: "ChallengeOption");

            migrationBuilder.DropTable(
                name: "Challenges");
        }
    }
}
