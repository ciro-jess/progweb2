namespace OnlineShop.Data.Entities;

public class Prenotazione
{
    public int Id { get; set; }

    public DateTime DataPrenotazione { get; set; } = DateTime.Now;

    public int NumeroPersone { get; set; } = 2;

    public int? NumeroTavolo { get; set; }

    public string? Note { get; set; }

    public string Stato { get; set; } = "InAttesa";

    public int UtenteId { get; set; }

    public Utente Utente { get; set; } = null!;

    public int? ProdottoId { get; set; }

    public Prodotto? Prodotto { get; set; }
}