namespace OnlineShop.Data.Entities;

public class OpzioneProdotto
{
    public int Id { get; set; }

    public string Nome { get; set;} = string.Empty;

    public string Categoria { get; set; } = string.Empty;

    public decimal Sovrapprezzo {get; set; }

    public bool Disponibile { get; set; } = true; 




    public List<ProdottoOpzione> ProdottiOpzioni { get; set; } = new();

    public List<CarrelloProdottoOpzione> CarrelloProdottoOpzioni { get; set; } = new();
}


