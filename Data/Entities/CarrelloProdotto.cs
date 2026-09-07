namespace OnlineShop.Data.Entities;

public class CarrelloProdotto
{
    public int Id { get; set; } 
    
    public int CarrelloId {get; set;}

    public Carrello? Carrello { get; set; }

    public int ProdottoId { get; set; }

    public Prodotto? Prodotto { get; set; } 
    public string? Note {get; set;}

    public int Quantita { get; set; } 

    public List<CarrelloProdottoOpzione>Opzioni {get; set; } = new();
}