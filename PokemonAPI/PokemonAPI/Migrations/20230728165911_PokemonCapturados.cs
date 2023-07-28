using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonAPI.Migrations
{
    /// <inheritdoc />
    public partial class PokemonCapturados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumeroDePokemon",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PokemonId",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Regiao",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PokemonsCapturados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PokemonName = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonsCapturados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sprites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    back_default = table.Column<string>(type: "TEXT", nullable: false),
                    back_shiny = table.Column<string>(type: "TEXT", nullable: false),
                    front_default = table.Column<string>(type: "TEXT", nullable: false),
                    front_shiny = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprites", x => x.Id);
                });

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
                name: "Type2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    url = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type2", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pokemon",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    weight = table.Column<int>(type: "INTEGER", nullable: false),
                    height = table.Column<int>(type: "INTEGER", nullable: false),
                    spritesId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemon", x => x.id);
                    table.ForeignKey(
                        name: "FK_Pokemon_Sprites_spritesId",
                        column: x => x.spritesId,
                        principalTable: "Sprites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pokemon_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    base_stat = table.Column<int>(type: "INTEGER", nullable: false),
                    effort = table.Column<int>(type: "INTEGER", nullable: false),
                    statId = table.Column<int>(type: "INTEGER", nullable: false),
                    Pokemonid = table.Column<int>(type: "INTEGER", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    slot = table.Column<int>(type: "INTEGER", nullable: false),
                    typeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Pokemonid = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Type_Pokemon_Pokemonid",
                        column: x => x.Pokemonid,
                        principalTable: "Pokemon",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Type_Type2_typeId",
                        column: x => x.typeId,
                        principalTable: "Type2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_spritesId",
                table: "Pokemon",
                column: "spritesId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_UserId",
                table: "Pokemon",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Stat_Pokemonid",
                table: "Stat",
                column: "Pokemonid");

            migrationBuilder.CreateIndex(
                name: "IX_Stat_statId",
                table: "Stat",
                column: "statId");

            migrationBuilder.CreateIndex(
                name: "IX_Type_Pokemonid",
                table: "Type",
                column: "Pokemonid");

            migrationBuilder.CreateIndex(
                name: "IX_Type_typeId",
                table: "Type",
                column: "typeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonsCapturados");

            migrationBuilder.DropTable(
                name: "Stat");

            migrationBuilder.DropTable(
                name: "Type");

            migrationBuilder.DropTable(
                name: "Stat2");

            migrationBuilder.DropTable(
                name: "Pokemon");

            migrationBuilder.DropTable(
                name: "Type2");

            migrationBuilder.DropTable(
                name: "Sprites");

            migrationBuilder.DropColumn(
                name: "NumeroDePokemon",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PokemonId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Regiao",
                table: "Users");
        }
    }
}
