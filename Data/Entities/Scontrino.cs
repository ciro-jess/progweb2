namespace OnlineShop.Data.Entities;

public class Scontrino
{
    public int Id { get; set; }

    public int OrdineId { get; set; }

    public Ordine Ordine { get; set; } = null!;

    public string NumeroScontrino { get; set; } = string.Empty;

    public string? PdfUrl { get; set; }

    public DateTime DataEmissione { get; set; } = DateTime.Now;
}
