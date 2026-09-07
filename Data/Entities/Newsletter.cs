namespace OnlineShop.Data.Entities;
public class Newsletter
{
    public int Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public DateTime DataIscrizione { get; set; } = DateTime.Now;

    public bool Attiva { get; set; } = true;
}