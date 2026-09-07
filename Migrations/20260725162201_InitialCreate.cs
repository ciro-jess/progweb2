using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Newsletters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    DataIscrizione = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Attiva = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newsletters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OtpVerifiche",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    CodiceOtp = table.Column<string>(type: "TEXT", nullable: false),
                    DataCreazione = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Scadenza = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Utilizzato = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtpVerifiche", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prodotti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Descrizione = table.Column<string>(type: "TEXT", nullable: true),
                    Categoria = table.Column<string>(type: "TEXT", nullable: false),
                    Prezzo = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    QuantitaMagazzino = table.Column<int>(type: "INTEGER", nullable: false),
                    ImmagineUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Disponibile = table.Column<bool>(type: "INTEGER", nullable: false),
                    DataInserimento = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodotti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sconti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Codice = table.Column<string>(type: "TEXT", nullable: false),
                    Percentuale = table.Column<decimal>(type: "TEXT", nullable: false),
                    DataInizio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataFine = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Attivo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sconti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utenti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Cognome = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Telefono = table.Column<string>(type: "TEXT", nullable: true),
                    Ruolo = table.Column<string>(type: "TEXT", nullable: false),
                    DataRegistrazione = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utenti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carrelli",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UtenteId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataCreazione = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrelli", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carrelli_Utenti_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "Utenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ordini",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UtenteId = table.Column<int>(type: "INTEGER", nullable: false),
                    Totale = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    Stato = table.Column<string>(type: "TEXT", nullable: false),
                    DataOrdine = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordini", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ordini_Utenti_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "Utenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prenotazioni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataPrenotazione = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Stato = table.Column<string>(type: "TEXT", nullable: false),
                    UtenteId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProdottoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prenotazioni", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prenotazioni_Prodotti_ProdottoId",
                        column: x => x.ProdottoId,
                        principalTable: "Prodotti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prenotazioni_Utenti_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "Utenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarrelloProdotti",
                columns: table => new
                {
                    CarrelloId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProdottoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantita = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrelloProdotti", x => new { x.CarrelloId, x.ProdottoId });
                    table.ForeignKey(
                        name: "FK_CarrelloProdotti_Carrelli_CarrelloId",
                        column: x => x.CarrelloId,
                        principalTable: "Carrelli",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarrelloProdotti_Prodotti_ProdottoId",
                        column: x => x.ProdottoId,
                        principalTable: "Prodotti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DatiFatturazioni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Cognome = table.Column<string>(type: "TEXT", nullable: false),
                    Indirizzo = table.Column<string>(type: "TEXT", nullable: false),
                    Citta = table.Column<string>(type: "TEXT", nullable: false),
                    Cap = table.Column<string>(type: "TEXT", nullable: false),
                    Provincia = table.Column<string>(type: "TEXT", nullable: false),
                    Nazione = table.Column<string>(type: "TEXT", nullable: false),
                    CodiceFiscale = table.Column<string>(type: "TEXT", nullable: true),
                    PartitaIva = table.Column<string>(type: "TEXT", nullable: true),
                    RagioneSociale = table.Column<string>(type: "TEXT", nullable: true),
                    Pec = table.Column<string>(type: "TEXT", nullable: true),
                    CodiceDestinatario = table.Column<string>(type: "TEXT", nullable: true),
                    OrdineId = table.Column<int>(type: "INTEGER", nullable: false)
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
                    NumeroFattura = table.Column<string>(type: "TEXT", nullable: false),
                    DataEmissione = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Imponibile = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    Iva = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    Totale = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    PdfUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Stato = table.Column<string>(type: "TEXT", nullable: false),
                    OrdineId = table.Column<int>(type: "INTEGER", nullable: false)
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
                name: "OrdiniProdotti",
                columns: table => new
                {
                    OrdineId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProdottoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantita = table.Column<int>(type: "INTEGER", nullable: false),
                    PrezzoUnitario = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdiniProdotti", x => new { x.OrdineId, x.ProdottoId });
                    table.ForeignKey(
                        name: "FK_OrdiniProdotti_Ordini_OrdineId",
                        column: x => x.OrdineId,
                        principalTable: "Ordini",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdiniProdotti_Prodotti_ProdottoId",
                        column: x => x.ProdottoId,
                        principalTable: "Prodotti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pagamenti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MetodoPagamento = table.Column<string>(type: "TEXT", nullable: false),
                    Importo = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    Stato = table.Column<string>(type: "TEXT", nullable: false),
                    TransactionId = table.Column<string>(type: "TEXT", nullable: true),
                    DataPagamento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OrdineId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamenti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pagamenti_Ordini_OrdineId",
                        column: x => x.OrdineId,
                        principalTable: "Ordini",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Scontrini",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrdineId = table.Column<int>(type: "INTEGER", nullable: false),
                    NumeroScontrino = table.Column<string>(type: "TEXT", nullable: false),
                    PdfUrl = table.Column<string>(type: "TEXT", nullable: true),
                    DataEmissione = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scontrini", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scontrini_Ordini_OrdineId",
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
                    Descrizione = table.Column<string>(type: "TEXT", nullable: false),
                    Quantita = table.Column<int>(type: "INTEGER", nullable: false),
                    PrezzoUnitario = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    IvaPercentuale = table.Column<decimal>(type: "TEXT", precision: 5, scale: 2, nullable: false),
                    FatturaId = table.Column<int>(type: "INTEGER", nullable: false)
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
                name: "IX_Carrelli_UtenteId",
                table: "Carrelli",
                column: "UtenteId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrelloProdotti_ProdottoId",
                table: "CarrelloProdotti",
                column: "ProdottoId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Ordini_UtenteId",
                table: "Ordini",
                column: "UtenteId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdiniProdotti_ProdottoId",
                table: "OrdiniProdotti",
                column: "ProdottoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamenti_OrdineId",
                table: "Pagamenti",
                column: "OrdineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prenotazioni_ProdottoId",
                table: "Prenotazioni",
                column: "ProdottoId");

            migrationBuilder.CreateIndex(
                name: "IX_Prenotazioni_UtenteId",
                table: "Prenotazioni",
                column: "UtenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Scontrini_OrdineId",
                table: "Scontrini",
                column: "OrdineId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrelloProdotti");

            migrationBuilder.DropTable(
                name: "DatiFatturazioni");

            migrationBuilder.DropTable(
                name: "FattureDettagli");

            migrationBuilder.DropTable(
                name: "Newsletters");

            migrationBuilder.DropTable(
                name: "OrdiniProdotti");

            migrationBuilder.DropTable(
                name: "OtpVerifiche");

            migrationBuilder.DropTable(
                name: "Pagamenti");

            migrationBuilder.DropTable(
                name: "Prenotazioni");

            migrationBuilder.DropTable(
                name: "Sconti");

            migrationBuilder.DropTable(
                name: "Scontrini");

            migrationBuilder.DropTable(
                name: "Carrelli");

            migrationBuilder.DropTable(
                name: "Fatture");

            migrationBuilder.DropTable(
                name: "Prodotti");

            migrationBuilder.DropTable(
                name: "Ordini");

            migrationBuilder.DropTable(
                name: "Utenti");
        }
    }
}
