using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Migrations
{
    /// <inheritdoc />
    public partial class PrenotazioniERitiroAlBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prenotazioni_Prodotti_ProdottoId",
                table: "Prenotazioni");

            migrationBuilder.AlterColumn<int>(
                name: "ProdottoId",
                table: "Prenotazioni",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Prenotazioni",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumeroPersone",
                table: "Prenotazioni",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumeroTavolo",
                table: "Prenotazioni",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModalitaRitiro",
                table: "Ordini",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NoteOrdine",
                table: "Ordini",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Prenotazioni_Prodotti_ProdottoId",
                table: "Prenotazioni",
                column: "ProdottoId",
                principalTable: "Prodotti",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prenotazioni_Prodotti_ProdottoId",
                table: "Prenotazioni");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Prenotazioni");

            migrationBuilder.DropColumn(
                name: "NumeroPersone",
                table: "Prenotazioni");

            migrationBuilder.DropColumn(
                name: "NumeroTavolo",
                table: "Prenotazioni");

            migrationBuilder.DropColumn(
                name: "ModalitaRitiro",
                table: "Ordini");

            migrationBuilder.DropColumn(
                name: "NoteOrdine",
                table: "Ordini");

            migrationBuilder.AlterColumn<int>(
                name: "ProdottoId",
                table: "Prenotazioni",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Prenotazioni_Prodotti_ProdottoId",
                table: "Prenotazioni",
                column: "ProdottoId",
                principalTable: "Prodotti",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
