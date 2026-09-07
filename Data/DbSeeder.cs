using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using OnlineShop.Data.Entities;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        await SeedUtentiAsync(db);
        await SeedProdottiAsync(db);
        await SeedOrdiniAsync(db);
        await SeedIdentityUsersAsync(db, userManager, roleManager);
        await SeedOpzioniProdottoAsync(db);
        await CollegaOpzioniAiProdottiAsync(db);
    }

    private static async Task SeedUtentiAsync(ApplicationDbContext db)
    {
        if (await db.Utenti.AnyAsync())
            return;

        var utenti = new List<Utente>
        {
            new Utente
            {
                Nome = "Ciro",
                Cognome = "Colonna",
                Email = "ciro.colonna@gmail.com",
                PasswordHash = "1Angelica",
                Telefono = "3331112233",
                Ruolo = "cliente",
                DataRegistrazione = new DateTime(2025, 1, 10, 9, 30, 0)
            },
            new Utente
            {
                Nome = "Marta",
                Cognome = "Bianchi",
                Email = "marta.bianchi@gmail.com",
                PasswordHash = "Marta2025",
                Telefono = "3332223344",
                Ruolo = "cliente",
                DataRegistrazione = new DateTime(2025, 1, 12, 10, 15, 0)
            },
            new Utente
            {
                Nome = "Luca",
                Cognome = "Rossi",
                Email = "luca.rossi@gmail.com",
                PasswordHash = "LucaRossi",
                Telefono = "3333334455",
                Ruolo = "cliente",
                DataRegistrazione = new DateTime(2025, 1, 15, 11, 0, 0)
            },
            new Utente
            {
                Nome = "Sara",
                Cognome = "Verdi",
                Email = "sara.verdi@gmail.com",
                PasswordHash = "SaraV2025",
                Telefono = "3334445566",
                Ruolo = "cliente",
                DataRegistrazione = new DateTime(2025, 2, 1, 8, 45, 0)
            },
            new Utente
            {
                Nome = "Giovanni",
                Cognome = "Neri",
                Email = "giovanni.neri@gmail.com",
                PasswordHash = "GiovaNeri",
                Telefono = "3335556677",
                Ruolo = "cliente",
                DataRegistrazione = new DateTime(2025, 2, 5, 14, 20, 0)
            },
            new Utente
            {
                Nome = "Elena",
                Cognome = "Gallo",
                Email = "elena.gallo@gmail.com",
                PasswordHash = "ElenaG",
                Telefono = "3336667788",
                Ruolo = "cliente",
                DataRegistrazione = new DateTime(2025, 2, 10, 16, 10, 0)
            },
            new Utente
            {
                Nome = "Paolo",
                Cognome = "Marini",
                Email = "paolo.marini@gmail.com",
                PasswordHash = "PMarini99",
                Telefono = "3337778899",
                Ruolo = "cliente",
                DataRegistrazione = new DateTime(2025, 3, 3, 12, 30, 0)
            },
            new Utente
            {
                Nome = "Chiara",
                Cognome = "Lombardi",
                Email = "chiara.lombardi@gmail.com",
                PasswordHash = "ChiaraL",
                Telefono = "3338889900",
                Ruolo = "cliente",
                DataRegistrazione = new DateTime(2025, 3, 8, 9, 5, 0)
            },
            new Utente
            {
                Nome = "Marco",
                Cognome = "De Luca",
                Email = "marco.deluca@gmail.com",
                PasswordHash = "MarcoDL",
                Telefono = "3339990011",
                Ruolo = "cliente",
                DataRegistrazione = new DateTime(2025, 4, 1, 13, 40, 0)
            },
            new Utente
            {
                Nome = "Admin",
                Cognome = "Shop",
                Email = "admin.shop@gmail.com",
                PasswordHash = "Admin",
                Telefono = "3470001122",
                Ruolo = "admin",
                DataRegistrazione = new DateTime(2025, 4, 12, 7, 50, 0)
            }
        };

        db.Utenti.AddRange(utenti);
        await db.SaveChangesAsync();
    }

    private static async Task SeedProdottiAsync(ApplicationDbContext db)
    {
        if (await db.Prodotti.AnyAsync())
            return;

        var prodotti = new List<Prodotto>
        {
            new Prodotto
            {
                Nome = "Anello Oro Bianco",
                Descrizione = "Anello elegante in oro bianco con pietra centrale.",
                Categoria = "Gioiello",
                Prezzo = 1299.00m,
                QuantitaMagazzino = 12,
                ImmagineUrl = "/images/anello-oro-bianco.jpg",
                Disponibile = true,
                DataInserimento = new DateTime(2025, 5, 1, 10, 0, 0)
            },
            new Prodotto
            {
                Nome = "Collana in Argento",
                Descrizione = "Collana sottile in argento con pendente a cuore.",
                Categoria = "Gioiello",
                Prezzo = 890.00m,
                QuantitaMagazzino = 8,
                ImmagineUrl = "/images/collana-argento.jpg",
                Disponibile = true,
                DataInserimento = new DateTime(2025, 5, 3, 11, 30, 0)
            },
            new Prodotto
            {
                Nome = "Orologio da Polso",
                Descrizione = "Orologio sportivo con cinturino in acciaio.",
                Categoria = "Orologio",
                Prezzo = 1590.00m,
                QuantitaMagazzino = 6,
                ImmagineUrl = "/images/orologio-polso.jpg",
                Disponibile = true,
                DataInserimento = new DateTime(2025, 5, 5, 9, 15, 0)
            },
            new Prodotto
            {
                Nome = "Borsa in Pelle",
                Descrizione = "Borsa elegante in pelle nera con zip laterale.",
                Categoria = "Borsa",
                Prezzo = 1190.00m,
                QuantitaMagazzino = 7,
                ImmagineUrl = "/images/borsa-pelle.jpg",
                Disponibile = true,
                DataInserimento = new DateTime(2025, 5, 6, 15, 0, 0)
            },
            new Prodotto
            {
                Nome = "Bracciale in Oro",
                Descrizione = "Bracciale in oro giallo con chiusura a moschettone.",
                Categoria = "Gioiello",
                Prezzo = 1499.00m,
                QuantitaMagazzino = 10,
                ImmagineUrl = "/images/bracciale-oro.jpg",
                Disponibile = true,
                DataInserimento = new DateTime(2025, 5, 8, 13, 45, 0)
            },
            new Prodotto
            {
                Nome = "Anello Diamante",
                Descrizione = "Anello con diamante sintetico e montatura in platino.",
                Categoria = "Anello",
                Prezzo = 2140.00m,
                QuantitaMagazzino = 5,
                ImmagineUrl = "/images/anello-diamante.jpg",
                Disponibile = true,
                DataInserimento = new DateTime(2025, 5, 10, 17, 20, 0)
            },
            new Prodotto
            {
                Nome = "Cintura in Cuir",
                Descrizione = "Cintura elegante in cuoio nero con fibbia in metallo.",
                Categoria = "Accessori",
                Prezzo = 650.00m,
                QuantitaMagazzino = 14,
                ImmagineUrl = "/images/cintura-cuir.jpg",
                Disponibile = true,
                DataInserimento = new DateTime(2025, 5, 12, 12, 10, 0)
            },
            new Prodotto
            {
                Nome = "Orologio Elegante",
                Descrizione = "Orologio classico con quadrante blu e cinturino in pelle.",
                Categoria = "Orologio",
                Prezzo = 1790.00m,
                QuantitaMagazzino = 9,
                ImmagineUrl = "/images/orologio-elegante.jpg",
                Disponibile = true,
                DataInserimento = new DateTime(2025, 5, 14, 16, 30, 0)
            },
            new Prodotto
            {
                Nome = "Borsa Mini",
                Descrizione = "Borsa mini per eventi e serate speciali.",
                Categoria = "Borsa",
                Prezzo = 980.00m,
                QuantitaMagazzino = 11,
                ImmagineUrl = "/images/borsa-mini.jpg",
                Disponibile = true,
                DataInserimento = new DateTime(2025, 5, 16, 18, 0, 0)
            },
            new Prodotto
            {
                Nome = "Cuffia in Metallo",
                Descrizione = "Cuffia elegante con dettagli in metallo satinato.",
                Categoria = "Gioiello",
                Prezzo = 740.00m,
                QuantitaMagazzino = 13,
                ImmagineUrl = "/images/cuffia-metallo.jpg",
                Disponibile = true,
                DataInserimento = new DateTime(2025, 5, 18, 8, 40, 0)
            }
        };

        db.Prodotti.AddRange(prodotti);
        await db.SaveChangesAsync();
    }

    private static async Task SeedIdentityUsersAsync(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        string[] roles = { "Admin", "Cliente" };
        foreach (var roleName in roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        var utenti = await db.Utenti.AsNoTracking().ToListAsync();
        foreach (var utente in utenti)
        {
            var email = utente.Email.Trim();
            var identityUser = await userManager.FindByEmailAsync(email);
            if (identityUser == null)
            {
                identityUser = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                var rawPassword = string.IsNullOrWhiteSpace(utente.PasswordHash)
                    ? "Password1!"
                    : utente.PasswordHash;

                var createResult = await userManager.CreateAsync(identityUser, rawPassword);
                if (!createResult.Succeeded)
                {
                    rawPassword = "Password1!";
                    createResult = await userManager.CreateAsync(identityUser, rawPassword);
                }

                if (!createResult.Succeeded)
                {
                    throw new InvalidOperationException($"Impossibile creare l'utente Identity '{email}': {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
                }
            }

            string? ruolo = utente.Ruolo?.Trim().ToLowerInvariant() switch
            {
                "admin" => "Admin",
                "cliente" => "Cliente",
                _ => null
            };

            if (ruolo is not null && !await userManager.IsInRoleAsync(identityUser, ruolo))
            {
                await userManager.AddToRoleAsync(identityUser, ruolo);
            }
        }
    }

    private static async Task SeedOrdiniAsync(ApplicationDbContext db)
    {
        if (await db.Ordini.AnyAsync())
            return;

        var utenti = await db.Utenti.OrderBy(u => u.Id).ToListAsync();
        var prodotti = await db.Prodotti.OrderBy(p => p.Id).ToListAsync();

        var ordini = new List<Ordine>();
        var baseDate = new DateTime(2026, 6, 1, 12, 0, 0);

        for (int i = 0; i < 10; i++)
        {
            var utente = utenti[i % utenti.Count];
            var prodotto = prodotti[i % prodotti.Count];
            var quantita = 1 + (i % 3);
            var prezzoUnitario = prodotto.Prezzo;
            var totale = prezzoUnitario * quantita;
            var iva = totale * 0.22m;

            var ordine = new Ordine
            {
                UtenteId = utente.Id,
                Totale = totale + iva,
                Stato = i % 2 == 0 ? "Pagato" : "Spedito",
                DataOrdine = baseDate.AddDays(i),
                OrdineProdotti = new List<OrdineProdotto>()
            };

            ordine.OrdineProdotti.Add(new OrdineProdotto
            {
                Ordine = ordine,
                ProdottoId = prodotto.Id,
                Prodotto = prodotto,
                Quantita = quantita,
                PrezzoUnitario = prezzoUnitario
            });

            ordine.Pagamento = new Pagamento
            {
                MetodoPagamento = i % 2 == 0 ? "Carta" : "PayPal",
                Importo = ordine.Totale,
                Stato = "Accettato",
                TransactionId = $"TX-{1000 + i}",
                DataPagamento = baseDate.AddDays(i).AddHours(2),
                Ordine = ordine
            };

            ordine.Scontrino = new Scontrino
            {
                NumeroScontrino = $"SCT-{3000 + i}",
                PdfUrl = $"/scontrini/{3000 + i}.pdf",
                DataEmissione = baseDate.AddDays(i).AddHours(4),
                Ordine = ordine
            };

            ordini.Add(ordine);
        }

        db.Ordini.AddRange(ordini);
        await db.SaveChangesAsync();
    }
    private static async Task SeedOpzioniProdottoAsync(ApplicationDbContext db)
    {
        var opzioniDaInserire = new[]
        {
            new
            {
                Nome = " Piadina ",
                Categoria = "Pane",
                Sovrapprezzo = 0m
            },
            new
            {
                Nome = " Panino kebab ",
                Categoria = "Pane",
                Sovrapprezzo = 0m
            },
            new
            {
                Nome = " pollo ",
                Categoria = "Carne",
                Sovrapprezzo = 0m
            },
            new
            {
                Nome = " Vitello ",
                Categoria = "carne",
                Sovrapprezzo = 0m
            },
            new
            {
                Nome = " falafel ",
                Categoria = "Carne",
                Sovrapprezzo = 0m
            },
            new
            {
                Nome = " Pomodoro ",
                Categoria = "verdura",
                Sovrapprezzo = 0m
            },
            new
            {
                Nome = " Cipolla ",
                Categoria = "Verdura ",
                Sovrapprezzo = 0m
            },
            new
            {
                Nome = "Cavolo",
                Categoria = "Verdura",
                Sovrapprezzo = 0m
            },
            new
            {
                Nome = " Mais ",
                Categoria = "Verdura",
                Sovrapprezzo = 0m
            },
            new
            {
                Nome = " Salsa yogurt ",
                Categoria = "salse",
                Sovrapprezzo = 0m
            },
            new
            {
                Nome = " Salsa piccante ",
                Categoria = "salse",
                Sovrapprezzo = 0m
            },
            new
            {
                Nome = " maionese  ",
                Categoria = "salse",
                Sovrapprezzo = 0m
            },
            new
            {
                Nome = "  salsa barbecue ",
                Categoria = "salse",
                Sovrapprezzo = 0m
            },
            new
            {
                Nome = " Patatine ",
                Categoria = "extra",
                Sovrapprezzo = 0m
            }
        };
        foreach (var datiOpzione in opzioniDaInserire)
        {
            var esiste = await db.OpzioniProdotti.AnyAsync(o =>
                o.Nome == datiOpzione.Nome && o.Categoria == datiOpzione.Categoria);

            if (esiste)
            {
                continue;
            }

            db.OpzioniProdotti.Add(new OpzioneProdotto
            {
                Nome = datiOpzione.Nome,
                Categoria = datiOpzione.Categoria,
                Sovrapprezzo = datiOpzione.Sovrapprezzo,
                Disponibile = true
            });

        }
        await db.SaveChangesAsync();
    }

    private static async Task CollegaOpzioniAiProdottiAsync(ApplicationDbContext db)
    {
        var prodottiPersonalizzabili = await db.Prodotti
            .Where(p => p.Personalizzabile)
            .ToListAsync();

        if (prodottiPersonalizzabili.Count == 0)
        {
            return;
        }

        var opzioni = await db.OpzioniProdotti
            .Where(o => o.Disponibile)
            .ToListAsync();

        foreach (var prodotto in prodottiPersonalizzabili)
        {
            var opzioniGiaCollegate = await db.ProdottiOpzioni
                .Where(po => po.ProdottoId == prodotto.Id)
                .Select(po => po.OpzioneProdottoId)
                .ToListAsync();

            var idsGiaCollegati = opzioniGiaCollegate.ToHashSet();

            foreach (var opzione in opzioni)
            {
                if (idsGiaCollegati.Contains(opzione.Id))
                {
                    continue;
                }

                db.ProdottiOpzioni.Add(new ProdottoOpzione
                {
                    ProdottoId = prodotto.Id,
                    OpzioneProdottoId = opzione.Id
                });
            }
        }

        await db.SaveChangesAsync();
    }
}