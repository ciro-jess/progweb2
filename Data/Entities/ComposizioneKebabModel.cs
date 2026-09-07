namespace OnlineShop.Model;
using System.ComponentModel.DataAnnotations.Schema;

public class ComposizioneKebabModel
{

    public int ProdottoId { get; set;} 

    public string NomeProdotto {get; set; } = string.Empty;

    public decimal PrezzoBase {get; set;} 

    public int Quantita {get; set;} = 1;

    public int? PaneSelezionamentoId{ get; set;}
    public int? CarneSelezionamentoId{get; set;} 


    public HashSet<int> Verdure {get; set;} = new();

    public HashSet<int> SalseSelezionate {get; set;} = new();

    public HashSet<int> ExtraSelezionati {get; set; } = new();
    

}