namespace OnlineShop.Data.Entities;

public class DettaglioProdotto
{
    public int Id {get; set; }
    public string? Descrizione { get; set;} 
    public string? Marca {get; set; }
    public string? Materiale { get; set; }
    public string? Colore { get; set;}
    public string? Misura { get; set; }
    public string? Peso { get; set;}
    public string? Modello { get; set; }
    public string? Garanzia { get; set; }
    public string? Certificazione {get; set; }
    public string? Note { get; set;}

    // FK verso prodotto 
    public int ProdottoId { get; set;}

    // navigazione verso prodotto

    public Prodotto? Prodotto {get; set; } = null!;

}