using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Migrations
{
    /// <inheritdoc />
    public partial class RemoveInvoiceTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatiFatturazioni");

            migrationBuilder.DropTable(
                name: "FattureDettagli");

            migrationBuilder.DropTable(
                name: "Fatture");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DatiFatturazioni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrdineId = table.Column<int>(type: "INTEGER", nullable: false),
                    Cap = table.Column<string>(type: "TEXT", nullable: false),
                    Citta = table.Column<string>(type: "TEXT", nullable: false),
                    CodiceDestinatario = table.Column<string>(type: "TEXT", nullable: true),
                    CodiceFiscale = table.Column<string>(type: "TEXT", nullable: true),
                    Cognome = table.Column<string>(type: "TEXT", nullable: false),
                    Indirizzo = table.Column<string>(type: "TEXT", nullable: false),
                    Nazione = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    PartitaIva = table.Column<string>(type: "TEXT", nullable: true),
                    Pec = table.Column<string>(type: "TEXT", nullable: true),
                    Provincia = table.Column<string>(type: "TEXT", nullable: false),
                    RagioneSociale = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatiFatturazioni", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatiFatturazioni_Ordini_OrdineId",
                        column: x => x.OrdineId,
                        principalTable: "Ordini",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fatture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrdineId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataEmissione = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Imponibile = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    Iva = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    NumeroFattura = table.Column<string>(type: "TEXT", nullable: false),
                    PdfUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Stato = table.Column<string>(type: "TEXT", nullable: false),
                    Totale = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fatture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fatture_Ordini_OrdineId",
                        column: x => x.OrdineId,
                        principalTable: "Ordini",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FattureDettagli",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FatturaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Descrizione = table.Column<string>(type: "TEXT", nullable: false),
                    IvaPercentuale = table.Column<decimal>(type: "TEXT", precision: 5, scale: 2, nullable: false),
                    PrezzoUnitario = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    Quantita = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FattureDettagli", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FattureDettagli_Fatture_FatturaId",
                        column: x => x.FatturaId,
                        principalTable: "Fatture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DatiFatturazioni_OrdineId",
                table: "DatiFatturazioni",
                column: "OrdineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fatture_OrdineId",
                table: "Fatture",
                column: "OrdineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FattureDettagli_FatturaId",
                table: "FattureDettagli",
                column: "FatturaId");
        }
    }
}
