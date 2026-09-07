namespace OnlineShop.Data.Entities;

public class Prodotto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string? Descrizione { get; set; }

    public string Categoria { get; set; } = string.Empty;
    // Gioiello, Anello, Orologio, Borsa

    public decimal Prezzo { get; set; }

    public int QuantitaMagazzino { get; set; }

    public string? ImmagineUrl { get; set; }

    public bool Disponibile { get; set; } = true;

    public DateTime DataInserimento { get; set; } = DateTime.Now;

    public List<CarrelloProdotto> CarrelloProdotti { get; set; } = new();

    public List<OrdineProdotto> OrdineProdotti { get; set; } = new();

    public List<Prenotazione> Prenotazioni { get; set; } = new();
    public DettaglioProdotto? DettaglioProdotto { get; set;}

    public bool Personalizzabile {get; set;} 

    public List<ProdottoOpzione> ProdottiOpzioni {get; set; } = new();


    
}