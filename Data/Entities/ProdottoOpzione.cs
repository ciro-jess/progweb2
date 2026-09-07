namespace OnlineShop.Data.Entities; 

public class ProdottoOpzione
{  

     public int ProdottoId {get;set; }

     public Prodotto Prodotto {get; set;} = null!;
      
    public int OpzioneProdottoId {get; set; }

    public OpzioneProdotto OpzioneProdotto {get; set;}  =null!;

}