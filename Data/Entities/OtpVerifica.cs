namespace OnlineShop.Data.Entities;

public class OtpVerifica
{
    public int Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public string CodiceOtp { get; set; } = string.Empty;

    public DateTime DataCreazione { get; set; } = DateTime.Now;

    public DateTime Scadenza { get; set; }

    public bool Utilizzato { get; set; } = false;
}