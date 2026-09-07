namespace OnlineShop.Data.Entities;

public class Carrello
{
    public int Id { get; set; }

    public int UtenteId { get; set; }

    public Utente Utente { get; set; } = null!;

    public DateTime DataCreazione { get; set; } = DateTime.Now;

    public List<CarrelloProdotto> CarrelloProdotti { get; set; } = new();

}