using System.Security.Cryptography.X509Certificates;

namespace OnlineShop.Data.Entities;

public class CarrelloProdottoOpzione
{
    public int CarrelloProdottoId {  get; set; } 

    public CarrelloProdotto CarrelloProdotto {get; set;}  = null!;

    public int OpzioneProdottoId { get; set; } 

    public OpzioneProdotto OpzioneProdotto {get; set; } = null!;

    public decimal Sovrapprezzo {get; set; }


}