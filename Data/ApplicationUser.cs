using Microsoft.AspNetCore.Identity;




public class ApplicationUser : IdentityUser
{
    public string Nome {get; set; } =string.Empty;
    public string Cognome {get; set; } =string.Empty;
}