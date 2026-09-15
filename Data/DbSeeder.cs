using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using OnlineShop.Data.Entities;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        await SeedUtentiAsync(db);
        await EnsureAdminExistsAsync(db);
        await SeedProdottiAsync(db);
        await SeedOrdiniAsync(db);
        await SeedIdentityUsersAsync(db, userManager, roleManager);
        await SeedOpzioniProdottoAsync(db);
        await CollegaOpzioniAiProdottiAsync(db);
    }

    private static async Task EnsureAdminExistsAsync(ApplicationDbContext db)
    {
        var adminEmail = "admin.shop@gmail.com";
        var admin = await db.Utenti.FirstOrDefaultAsync(u => u.Email.ToLower() == adminEmail.ToLower());

        if (admin is null)
        {
            db.Utenti.Add(new Utente
            {
                Nome = "Admin",
                Cognome = "Shop",
                Email = adminEmail,
                PasswordHash = "Admin",
                Telefono = "3470001122",
                Ruolo = "admin",
                DataRegistrazione = DateTime.Now
            });
            await db.SaveChangesAsync();
            return;
        }

        if (!string.Equals(admin.Ruolo, "admin", StringComparison.OrdinalIgnoreCase))
        {
            admin.Ruolo = "admin";
            await db.SaveChangesAsync();
        }
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
          
        };

        db.Utenti.AddRange(utenti);
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
            var email = (utente.Email ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(email))
            {
                continue;
            }

            var identityUser = await userManager.FindByEmailAsync(email);
            if (identityUser == null)
            {
                identityUser = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    Nome = utente.Nome,
                    Cognome = utente.Cognome
                };

                var rawPassword = GetSeedPasswordForUser(utente);
                var createResult = await userManager.CreateAsync(identityUser, rawPassword);

                if (!createResult.Succeeded)
                {
                    var fallbackPassword = "Password1!";
                    createResult = await userManager.CreateAsync(identityUser, fallbackPassword);
                }

                if (!createResult.Succeeded)
                {
                    throw new InvalidOperationException($"Impossibile creare l'utente Identity '{email}': {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
                }
            }
            else
            {
                var needsProfileSync = !string.Equals(identityUser.UserName, email, StringComparison.OrdinalIgnoreCase)
                    || !string.Equals(identityUser.Email, email, StringComparison.OrdinalIgnoreCase)
                    || !string.Equals(identityUser.Nome, utente.Nome, StringComparison.OrdinalIgnoreCase)
                    || !string.Equals(identityUser.Cognome, utente.Cognome, StringComparison.OrdinalIgnoreCase);

                if (needsProfileSync)
                {
                    identityUser.UserName = email;
                    identityUser.Email = email;
                    identityUser.Nome = utente.Nome;
                    identityUser.Cognome = utente.Cognome;
                    await userManager.UpdateAsync(identityUser);
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

    private static string GetSeedPasswordForUser(Utente utente)
    {
        var email = (utente.Email ?? string.Empty).Trim();

        if (string.Equals(email, "admin.shop@gmail.com", StringComparison.OrdinalIgnoreCase))
            return "Admin";

        if (string.Equals(email, "ciro.colonna@gmail.com", StringComparison.OrdinalIgnoreCase))
            return "1Angelica";

        if (string.Equals(email, "marta.bianchi@gmail.com", StringComparison.OrdinalIgnoreCase))
            return "Marta2025";

        if (string.Equals(email, "luca.rossi@gmail.com", StringComparison.OrdinalIgnoreCase))
            return "LucaRossi";

        if (string.Equals(email, "sara.verdi@gmail.com", StringComparison.OrdinalIgnoreCase))
            return "SaraV2025";

        if (string.Equals(email, "giovanni.neri@gmail.com", StringComparison.OrdinalIgnoreCase))
            return "GiovaNeri";

        if (string.Equals(email, "elena.gallo@gmail.com", StringComparison.OrdinalIgnoreCase))
            return "ElenaG";

        if (string.Equals(email, "paolo.marini@gmail.com", StringComparison.OrdinalIgnoreCase))
            return "PMarini99";

        if (string.Equals(email, "chiara.lombardi@gmail.com", StringComparison.OrdinalIgnoreCase))
            return "ChiaraL";

        if (string.Equals(email, "marco.deluca@gmail.com", StringComparison.OrdinalIgnoreCase))
            return "MarcoDL";

        return string.IsNullOrWhiteSpace(utente.PasswordHash) ? "Password1!" : utente.PasswordHash;
    }

    private static async Task SeedProdottiAsync(ApplicationDbContext db)
    {
        if (await db.Prodotti.AnyAsync())
            return;

        var prodotti = new[]
        {
            new Prodotto
            {
                Nome = "Kebab Classico",
                Categoria = "Kebab",
                Prezzo = 8.50m,
                QuantitaMagazzino = 20,
                Disponibile = true,
                Personalizzabile = true,
                DataInserimento = DateTime.Now,
                Descrizione = "Kebab classico con carne, insalata e salsa"
            },
            new Prodotto
            {
                Nome = "Kebab Vegano",
                Categoria = "Kebab",
                Prezzo = 9.00m,
                QuantitaMagazzino = 15,
                Disponibile = true,
                Personalizzabile = true,
                DataInserimento = DateTime.Now,
                Descrizione = "Kebab vegano con falafel e verdure"
            },
            new Prodotto
            {
                Nome = "Patatine Fritte",
                Categoria = "Extra",
                Prezzo = 3.50m,
                QuantitaMagazzino = 25,
                Disponibile = true,
                Personalizzabile = false,
                DataInserimento = DateTime.Now,
                Descrizione = "Patatine croccanti da accompagnare"
            },
            new Prodotto
            {
                Nome = " birra Heineken",
                Categoria = "Bevande",
                Prezzo = 2.50m,
                QuantitaMagazzino = 50,
                Disponibile = true,
                Personalizzabile = false,
                DataInserimento = DateTime.Now,
                Descrizione = "Bibita fresca da 500ml"
            }
        };

        db.Prodotti.AddRange(prodotti);
        await db.SaveChangesAsync();
    }

    private static async Task SeedOrdiniAsync(ApplicationDbContext db)
    {
        if (await db.Ordini.AnyAsync())
            return;

        var utenti = await db.Utenti.OrderBy(u => u.Id).ToListAsync();
        var prodotti = await db.Prodotti.OrderBy(p => p.Id).ToListAsync();

        if (prodotti.Count == 0)
            return;

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
                Categoria = "verdura",
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
                Nome = "Salsa yogurt",
                Categoria = "Salsa",
                Sovrapprezzo = 0m
            },
            new
            {
                Nome = "Salsa piccante",
                Categoria = "Salsa",
                Sovrapprezzo = 0m
            },
            new
            {
                Nome = "maionese",
                Categoria = "Salsa",
                Sovrapprezzo = 0m
            },
            new
            {
                Nome = "salsa barbecue ",
                Categoria = "Salsa",
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