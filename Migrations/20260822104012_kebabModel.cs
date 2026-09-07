using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Migrations
{
    /// <inheritdoc />
    public partial class kebabModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CarrelloProdotti",
                table: "CarrelloProdotti");

            migrationBuilder.AddColumn<bool>(
                name: "Personalizzabile",
                table: "Prodotti",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CarrelloProdotti",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarrelloProdotti",
                table: "CarrelloProdotti",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "OpzioniProdotti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Categoria = table.Column<string>(type: "TEXT", nullable: false),
                    Sovrapprezzo = table.Column<decimal>(type: "TEXT", nullable: false),
                    Disponibile = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpzioniProdotti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarrelliProdottiOpzioni",
                columns: table => new
                {
                    CarrelloProdottoId = table.Column<int>(type: "INTEGER", nullable: false),
                    OpzioneProdottoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Sovrapprezzo = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrelliProdottiOpzioni", x => new { x.CarrelloProdottoId, x.OpzioneProdottoId });
                    table.ForeignKey(
                        name: "FK_CarrelliProdottiOpzioni_CarrelloProdotti_CarrelloProdottoId",
                        column: x => x.CarrelloProdottoId,
                        principalTable: "CarrelloProdotti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarrelliProdottiOpzioni_OpzioniProdotti_OpzioneProdottoId",
                        column: x => x.OpzioneProdottoId,
                        principalTable: "OpzioniProdotti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdottiOpzioni",
                columns: table => new
                {
                    ProdottoId = table.Column<int>(type: "INTEGER", nullable: false),
                    OpzioneProdottoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdottiOpzioni", x => new { x.ProdottoId, x.OpzioneProdottoId });
                    table.ForeignKey(
                        name: "FK_ProdottiOpzioni_OpzioniProdotti_OpzioneProdottoId",
                        column: x => x.OpzioneProdottoId,
                        principalTable: "OpzioniProdotti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdottiOpzioni_Prodotti_ProdottoId",
                        column: x => x.ProdottoId,
                        principalTable: "Prodotti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarrelloProdotti_CarrelloId",
                table: "CarrelloProdotti",
                column: "CarrelloId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrelliProdottiOpzioni_OpzioneProdottoId",
                table: "CarrelliProdottiOpzioni",
                column: "OpzioneProdottoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdottiOpzioni_OpzioneProdottoId",
                table: "ProdottiOpzioni",
                column: "OpzioneProdottoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrelliProdottiOpzioni");

            migrationBuilder.DropTable(
                name: "ProdottiOpzioni");

            migrationBuilder.DropTable(
                name: "OpzioniProdotti");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarrelloProdotti",
                table: "CarrelloProdotti");

            migrationBuilder.DropIndex(
                name: "IX_CarrelloProdotti_CarrelloId",
                table: "CarrelloProdotti");

            migrationBuilder.DropColumn(
                name: "Personalizzabile",
                table: "Prodotti");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CarrelloProdotti");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarrelloProdotti",
                table: "CarrelloProdotti",
                columns: new[] { "CarrelloId", "ProdottoId" });
        }
    }
}
