namespace OnlineShop.Data.Entities;

public class OrdineProdotto
{
    public int OrdineId { get; set; }
    

    public Ordine Ordine { get; set; } = null!;

    public int ProdottoId { get; set; }

    public Prodotto Prodotto { get; set; } = null!;

    public int Quantita { get; set; }

    public decimal PrezzoUnitario { get; set; }

    public string? OpzioniSelezionate { get; set; }
}