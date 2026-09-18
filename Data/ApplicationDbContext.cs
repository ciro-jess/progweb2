using System.Runtime.Intrinsics.Arm;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OnlineShop.Data.Entities;
using OnlineShop.Model;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ) : base( options) {}

    public override int SaveChanges()
    {
        AssegnaCodiciOrdine();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AssegnaCodiciOrdine();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void AssegnaCodiciOrdine()
    {
        foreach (var ordine in ChangeTracker.Entries<Ordine>()
                     .Where(entry => entry.State == EntityState.Added && string.IsNullOrWhiteSpace(entry.Entity.CodiceOrdine)))
        {
            ordine.Entity.CodiceOrdine = $"OM-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid():N}"[..19].ToUpperInvariant();
        }
    }


    public DbSet<Utente> Utenti { get; set; } = null!;
    public DbSet<Scontrino> Scontrini { get; set; } = null!;
    public DbSet<Sconto> Sconti { get; set; } = null!;
    public DbSet<Prodotto> Prodotti { get; set; } = null!;
    public DbSet<DettaglioProdotto> DettagliProdotti { get; set;} = null!;
    public DbSet<Prenotazione> Prenotazioni { get; set; } = null!;
    public DbSet<Pagamento> Pagamenti { get; set; } = null!;
    public DbSet<OtpVerifica> OtpVerifiche { get; set; } = null!;
    public DbSet<OrdineProdotto> OrdiniProdotti { get; set; } = null!;
    public DbSet<Ordine> Ordini { get; set; } = null!;
    public DbSet<Newsletter> Newsletters { get; set; } = null!;
    public DbSet<CarrelloProdotto> CarrelloProdotti { get; set; } = null!;
    public DbSet<OpzioneProdotto> OpzioniProdotti { get; set; } = null!;
    public DbSet<ProdottoOpzione> ProdottiOpzioni { get; set; } = null!;
    public DbSet<CarrelloProdottoOpzione> CarrelliProdottiOpzioni { get; set; } = null!;
   


    public DbSet<Carrello> Carrelli { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =========================
        // UTENTI
        // =========================
        modelBuilder.Entity<Utente>()
            .HasKey(u => u.Id);

        // Relazione Utente 1 - N Carrelli
        modelBuilder.Entity<Utente>()
            .HasMany(u => u.Carrelli)
            .WithOne(c => c.Utente)
            .HasForeignKey(c => c.UtenteId);

        // Relazione Utente 1 - N Ordini
        modelBuilder.Entity<Utente>()
            .HasMany(u => u.Ordini)
            .WithOne(o => o.Utente)
            .HasForeignKey(o => o.UtenteId);

        // Relazione Utente 1 - N Prenotazioni
        modelBuilder.Entity<Utente>()
            .HasMany(u => u.Prenotazioni)
            .WithOne(p => p.Utente)
            .HasForeignKey(p => p.UtenteId);


        // =========================
        // CARRELLI
        // =========================
        modelBuilder.Entity<Carrello>()
            .HasKey(c => c.Id);

        // Tabella ponte CarrelloProdotto
        modelBuilder.Entity<CarrelloProdotto>()
            .HasKey( cp => cp.Id );

        // Carrello 1 - N CarrelloProdotto
        modelBuilder.Entity<CarrelloProdotto>()
            .HasOne(cp => cp.Carrello)
            .WithMany(c => c.CarrelloProdotti)
            .HasForeignKey(cp => cp.CarrelloId);

        // Prodotto 1 - N CarrelloProdotto
        modelBuilder.Entity<CarrelloProdotto>()
            .HasOne(cp => cp.Prodotto)
            .WithMany(p => p.CarrelloProdotti)
            .HasForeignKey(cp => cp.ProdottoId);

        
        modelBuilder.Entity<DettaglioProdotto>().HasKey(dp => dp.Id);

        modelBuilder.Entity<Prodotto>()
            .HasOne(p => p.DettaglioProdotto)
            .WithOne(dp => dp.Prodotto)
            .HasForeignKey<DettaglioProdotto>(dp => dp.ProdottoId);

        // =========================
        // PRODOTTI
        // =========================
        modelBuilder.Entity<Prodotto>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Prodotto>()
            .Property(p => p.Prezzo)
            .HasPrecision(10, 2);

        // Prodotto 1 - N Prenotazioni
        modelBuilder.Entity<Prodotto>()
            .HasMany(p => p.Prenotazioni)
            .WithOne(pr => pr.Prodotto)
            .HasForeignKey(pr => pr.ProdottoId)
            .IsRequired(false);




        // =========================
        // ORDINI
        // =========================
        modelBuilder.Entity<Ordine>()
            .HasKey(o => o.Id);

        modelBuilder.Entity<Ordine>()
            .HasIndex(o => o.CodiceOrdine)
            .IsUnique();

        modelBuilder.Entity<Ordine>()
            .Property(o => o.Totale)
            .HasPrecision(10, 2);

        // Tabella ponte OrdineProdotto
        modelBuilder.Entity<OrdineProdotto>()
            .HasKey(op => new { op.OrdineId, op.ProdottoId });

        // Ordine 1 - N OrdineProdotto
        modelBuilder.Entity<OrdineProdotto>()
            .HasOne(op => op.Ordine)
            .WithMany(o => o.OrdineProdotti)
            .HasForeignKey(op => op.OrdineId);

        // Prodotto 1 - N OrdineProdotto
        modelBuilder.Entity<OrdineProdotto>()
            .HasOne(op => op.Prodotto)
            .WithMany(p => p.OrdineProdotti)
            .HasForeignKey(op => op.ProdottoId);

        modelBuilder.Entity<ProdottoOpzione>().HasKey(po => new {
            po.ProdottoId, po.OpzioneProdottoId
        });

        modelBuilder.Entity<ProdottoOpzione>()
            .HasOne(po => po.Prodotto)
            .WithMany(p => p.ProdottiOpzioni)
            .HasForeignKey (po => po.ProdottoId);

        modelBuilder.Entity<ProdottoOpzione>()
            .HasOne (po => po.OpzioneProdotto)
            .WithMany (o => o. ProdottiOpzioni)
            .HasForeignKey (po => po.OpzioneProdottoId);

        modelBuilder.Entity<CarrelloProdottoOpzione>()
            .HasKey(cpo => new {
                cpo.CarrelloProdottoId, cpo.OpzioneProdottoId
            });

        modelBuilder.Entity<CarrelloProdottoOpzione>()
            .HasOne(cpo => cpo.CarrelloProdotto)
            .WithMany (cp => cp.Opzioni)
            .HasForeignKey(cpo => cpo.CarrelloProdottoId)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<CarrelloProdottoOpzione>()
            .HasOne(cpo => cpo.OpzioneProdotto)
            .WithMany(o => o.CarrelloProdottoOpzioni)
            .HasForeignKey(cpo => cpo.OpzioneProdottoId);

            
        // =========================
        // PAGAMENTI
        // =========================
        modelBuilder.Entity<Pagamento>()
            .HasKey(pg => pg.Id);

        modelBuilder.Entity<Pagamento>()
            .Property(pg => pg.Importo)
            .HasPrecision(10, 2);

        // Ordine 1 - 1 Pagamento
        modelBuilder.Entity<Ordine>()
            .HasOne(o => o.Pagamento)
            .WithOne(p => p.Ordine)
            .HasForeignKey<Pagamento>(p => p.OrdineId);


        // =========================
        // SCONTRINI
        // =========================
        modelBuilder.Entity<Scontrino>()
            .HasKey(s => s.Id);

        // Ordine 1 - 1 Scontrino
        modelBuilder.Entity<Ordine>()
            .HasOne(o => o.Scontrino)
            .WithOne(s => s.Ordine)
            .HasForeignKey<Scontrino>(s => s.OrdineId);


        // =========================
        // PRENOTAZIONI
        // =========================
        modelBuilder.Entity<Prenotazione>()
            .HasKey(pr => pr.Id);


        // =========================
        // ALTRE TABELLE
        // =========================
        modelBuilder.Entity<Sconto>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<Newsletter>()
            .HasKey(n => n.Id);

        modelBuilder.Entity<OtpVerifica>()
            .HasKey(o => o.Id);
    }
}