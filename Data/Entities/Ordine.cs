using System;
using System.Collections.Generic;
using OnlineShop.Model;

namespace OnlineShop.Data.Entities
{

        public class Ordine
        {
            public int Id { get; set; }

            public string CodiceOrdine { get; set; } = string.Empty;

            public int UtenteId { get; set; }

            public Utente Utente { get; set; } = null!;

            public decimal Totale { get; set; }

            public string Stato { get; set; } = "";
            // InAttesa, Accettato, Rifiutato, Pagato, Spedito, Consegnato

            public string ModalitaRitiro { get; set; } = "Al banco";

            public string? NoteOrdine { get; set; }

            public DateTime DataOrdine { get; set; } = DateTime.Now;

            public List<OrdineProdotto> OrdineProdotti { get; set; } = new();

            public Pagamento? Pagamento { get; set; }

            public Scontrino? Scontrino { get; set; }

           

        }
    }