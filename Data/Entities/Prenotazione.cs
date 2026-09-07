namespace OnlineShop.Data.Entities;

public class Prenotazione
{
    public int Id { get; set; }

    public DateTime DataPrenotazione { get; set; } = DateTime.Now;

    public string Stato { get; set; } = "InAttesa";

    public int UtenteId { get; set; }

    public Utente Utente { get; set; } = null!;

    public int ProdottoId { get; set; }

    public Prodotto Prodotto { get; set; } = null!;
}