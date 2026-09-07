namespace OnlineShop.Data.Entities;

public class Pagamento
{
    public int Id { get; set; }

    public string MetodoPagamento { get; set; } = string.Empty;

    public decimal Importo { get; set; }

    public string Stato { get; set; } = "InAttesa";

    public string? TransactionId { get; set; }

    public DateTime DataPagamento { get; set; } = DateTime.Now;

    public int OrdineId { get; set; }

    public Ordine Ordine { get; set; } = null!;
}