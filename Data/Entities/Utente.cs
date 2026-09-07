using System;
using System.Collections.Generic;
using System.Data;


namespace OnlineShop.Data.Entities
{
    public class Utente
    {
     public int Id { get; set;}

     public string Nome { get; set;} = string.Empty;

     public string Cognome { get; set;} = string.Empty;

     public string Email  {get; set;} = string.Empty;

     public string NomeCompleto => $"{Nome} {Cognome}".Trim();

     public string PasswordHash { get; set;} = string.Empty;

     public string? Telefono {get;  set; } 


     public string Ruolo {get; set;} = "Cliente";

     public DateTime DataRegistrazione { get; set;} = DateTime.Now;

     public List<Carrello> Carrelli { get; set;} = new();

     public List<Ordine> Ordini { get; set;} = new(); 

     public List<Prenotazione> Prenotazioni {get; set;} = new();
    }



}