using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonAPI.Migrations
{
    /// <inheritdoc />
    public partial class UserUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stat");

            migrationBuilder.DropTable(
                name: "Stat2");

            migrationBuilder.DropColumn(
                name: "PokemonId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "url",
                table: "Type2");

            migrationBuilder.DropColumn(
                name: "back_default",
                table: "Sprites");

            migrationBuilder.DropColumn(
                name: "back_shiny",
                table: "Sprites");

            migrationBuilder.DropColumn(
                name: "front_shiny",
                table: "Sprites");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PokemonId",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "url",
                table: "Type2",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "back_default",
                table: "Sprites",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "back_shiny",
                table: "Sprites",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "front_shiny",
                table: "Sprites",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Stat2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    url = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stat2", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    statId = table.Column<int>(type: "INTEGER", nullable: false),
                    Pokemonid = table.Column<int>(type: "INTEGER", nullable: true),
                    base_stat = table.Column<int>(type: "INTEGER", nullable: false),
                    effort = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stat_Pokemon_Pokemonid",
                        column: x => x.Pokemonid,
                        principalTable: "Pokemon",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Stat_Stat2_statId",
                        column: x => x.statId,
                        principalTable: "Stat2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stat_Pokemonid",
                table: "Stat",
                column: "Pokemonid");

            migrationBuilder.CreateIndex(
                name: "IX_Stat_statId",
                table: "Stat",
                column: "statId");
        }
    }
}
