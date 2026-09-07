namespace OnlineShop.Data.Entities;

public class Sconto
{
    public int Id { get; set; }

    public string Codice { get; set; } = string.Empty;

    public decimal Percentuale { get; set; }

    public DateTime DataInizio { get; set; }

    public DateTime DataFine { get; set; }

    public bool Attivo { get; set; } = true;
}