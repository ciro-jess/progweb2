using System.ComponentModel.DataAnnotations;
using OnlineShop.Data.Entities;

namespace OnlineShop.ViewModels;

public class ProdottoViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Il nome è obbligatorio.")]
    [StringLength(100, ErrorMessage = "Il nome non può superare 100 caratteri.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "La categoria è obbligatoria.")]
    [StringLength(50, ErrorMessage = "La categoria non può superare 50 caratteri.")]
    public string Categoria { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "La descrizione non può superare 500 caratteri.")]
    public string? Descrizione { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Il prezzo deve essere maggiore di zero.")]
    public decimal Prezzo { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "La quantità non può essere negativa.")]
    public int QuantitaMagazzino { get; set; }

    public string? ImmagineUrl { get; set; }

    public bool Disponibile { get; set; } = true;

    public DateTime DataInserimento { get; set; } = DateTime.Now;

    public static ProdottoViewModel FromEntity(Prodotto prodotto)
    {
        return new ProdottoViewModel
        {
            Id = prodotto.Id,
            Nome = prodotto.Nome,
            Categoria = prodotto.Categoria,
            Descrizione = prodotto.Descrizione,
            Prezzo = prodotto.Prezzo,
            QuantitaMagazzino = prodotto.QuantitaMagazzino,
            ImmagineUrl = prodotto.ImmagineUrl,
            Disponibile = prodotto.Disponibile,
            DataInserimento = prodotto.DataInserimento
        };
    }

    public Prodotto ToEntity()
    {
        return new Prodotto
        {
            Id = Id,
            Nome = Nome,
            Categoria = Categoria,
            Descrizione = Descrizione,
            Prezzo = Prezzo,
            QuantitaMagazzino = QuantitaMagazzino,
            ImmagineUrl = ImmagineUrl,
            Disponibile = Disponibile,
            DataInserimento = DataInserimento
        };
    }

    public void UpdateEntity(Prodotto prodotto)
    {
        prodotto.Nome = Nome;
        prodotto.Categoria = Categoria;
        prodotto.Descrizione = Descrizione;
        prodotto.Prezzo = Prezzo;
        prodotto.QuantitaMagazzino = QuantitaMagazzino;
        prodotto.ImmagineUrl = ImmagineUrl;
        prodotto.Disponibile = Disponibile;
    }
}
