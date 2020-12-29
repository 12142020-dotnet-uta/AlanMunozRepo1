using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HelloWorldDemo.Migrations
{
    public partial class initmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    PlayerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.PlayerID);
                });

            migrationBuilder.CreateTable(
                name: "matches",
                columns: table => new
                {
                    MatchID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Player1PlayerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Player2PlayerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matches", x => x.MatchID);
                    table.ForeignKey(
                        name: "FK_matches_players_Player1PlayerID",
                        column: x => x.Player1PlayerID,
                        principalTable: "players",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_matches_players_Player2PlayerID",
                        column: x => x.Player2PlayerID,
                        principalTable: "players",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "rounds",
                columns: table => new
                {
                    RoundID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerChoice = table.Column<int>(type: "int", nullable: false),
                    Player2Choice = table.Column<int>(type: "int", nullable: false),
                    WinningPlayerPlayerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MatchID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rounds", x => x.RoundID);
                    table.ForeignKey(
                        name: "FK_rounds_players_WinningPlayerPlayerID",
                        column: x => x.WinningPlayerPlayerID,
                        principalTable: "players",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_rounds_match_MatchID",
                        column: x => x.MatchID,
                        principalTable: "match",
                        principalColumn: "MatchID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_matches_Player1PlayerID",
                table: "matches",
                column: "Player1PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_matches_Player2PlayerID",
                table: "matches",
                column: "Player2PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_rounds_WinningPlayerPlayerID",
                table: "rounds",
                column: "WinningPlayerPlayerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "matches");

            migrationBuilder.DropTable(
                name: "rounds");

            migrationBuilder.DropTable(
                name: "players");
        }
    }
}
