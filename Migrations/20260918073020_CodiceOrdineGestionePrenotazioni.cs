using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Migrations
{
    /// <inheritdoc />
    public partial class CodiceOrdineGestionePrenotazioni : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodiceOrdine",
                table: "Ordini",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("UPDATE \"Ordini\" SET \"CodiceOrdine\" = 'OM-LEGACY-' || printf('%08d', \"Id\") WHERE \"CodiceOrdine\" = '';");

            migrationBuilder.CreateIndex(
                name: "IX_Ordini_CodiceOrdine",
                table: "Ordini",
                column: "CodiceOrdine",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ordini_CodiceOrdine",
                table: "Ordini");

            migrationBuilder.DropColumn(
                name: "CodiceOrdine",
                table: "Ordini");
        }
    }
}
