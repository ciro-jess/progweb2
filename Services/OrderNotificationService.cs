using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using OnlineShop.Data.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Net;

namespace OnlineShop.Services;

public sealed class OrderNotificationService(
    IOptions<EmailOptions> options,
    ILogger<OrderNotificationService> logger) : IOrderNotificationService
{
    private readonly EmailOptions settings = options.Value;

    public Task SendOrderConfirmationAsync(Ordine ordine, CancellationToken cancellationToken = default)
    {
        var recipient = ordine.Utente?.Email;
        if (string.IsNullOrWhiteSpace(recipient))
        {
            return Task.CompletedTask;
        }

        var attachment = CreateReceiptPdf(ordine);
        var orderCode = Encode(ordine.CodiceOrdine);
        var pickup = Encode(ordine.ModalitaRitiro);
        return SendAsync(
            recipient,
            $"Conferma ordine {orderCode}",
            $"<h1>Grazie per il tuo ordine</h1><p>Il tuo ordine <strong>{orderCode}</strong> è stato ricevuto.</p><p>Modalità di ritiro: <strong>{pickup}</strong></p><p>Totale: <strong>{ordine.Totale:C}</strong></p><p>In allegato trovi lo scontrino PDF.</p>",
            attachment,
            $"scontrino-{ordine.CodiceOrdine}.pdf",
            cancellationToken);
    }

    public Task SendReservationCreatedAsync(Prenotazione prenotazione, CancellationToken cancellationToken = default)
    {
        var recipient = prenotazione.Utente?.Email;
        if (string.IsNullOrWhiteSpace(recipient))
        {
            return Task.CompletedTask;
        }

        var date = Encode(prenotazione.DataPrenotazione.ToString("dddd dd MMMM yyyy alle HH:mm"));
        var status = Encode(prenotazione.Stato);
        return SendAsync(
            recipient,
            "Richiesta di prenotazione ricevuta",
            $"<h1>Richiesta ricevuta</h1><p>Abbiamo ricevuto la tua richiesta per <strong>{date}</strong>.</p><p>Persone: <strong>{prenotazione.NumeroPersone}</strong></p><p>Stato: <strong>{status}</strong></p><p>Ti invieremo una nuova email quando la prenotazione sarà confermata.</p>",
            null,
            null,
            cancellationToken);
    }

    public Task SendReservationStatusAsync(Prenotazione prenotazione, CancellationToken cancellationToken = default)
    {
        var recipient = prenotazione.Utente?.Email;
        if (string.IsNullOrWhiteSpace(recipient))
        {
            return Task.CompletedTask;
        }

        var tableText = prenotazione.NumeroTavolo.HasValue
            ? $"<p>Tavolo assegnato: <strong>{prenotazione.NumeroTavolo}</strong></p>"
            : string.Empty;
        var reservationStatus = Encode(prenotazione.Stato);
        var reservationDate = Encode(prenotazione.DataPrenotazione.ToString("dddd dd MMMM yyyy alle HH:mm"));

        return SendAsync(
            recipient,
            $"Aggiornamento prenotazione: {reservationStatus}",
            $"<h1>Aggiornamento prenotazione</h1><p>La tua prenotazione del <strong>{reservationDate}</strong> è ora <strong>{reservationStatus}</strong>.</p>{tableText}",
            null,
            null,
            cancellationToken);
    }

    public Task SendOrderStatusAsync(Ordine ordine, CancellationToken cancellationToken = default)
    {
        var recipient = ordine.Utente?.Email;
        if (string.IsNullOrWhiteSpace(recipient))
        {
            return Task.CompletedTask;
        }

        var orderCode = Encode(ordine.CodiceOrdine);
        var orderStatus = Encode(ordine.Stato);
        var pickup = Encode(ordine.ModalitaRitiro);
        return SendAsync(
            recipient,
            $"Aggiornamento ordine {orderCode}: {orderStatus}",
            $"<h1>Aggiornamento ordine</h1><p>Lo stato del tuo ordine <strong>{orderCode}</strong> è ora <strong>{orderStatus}</strong>.</p><p>Modalità di ritiro: <strong>{pickup}</strong></p>",
            null,
            null,
            cancellationToken);
    }

    private static string Encode(string value) => WebUtility.HtmlEncode(value);

    private async Task SendAsync(
        string recipient,
        string subject,
        string html,
        byte[]? attachment,
        string? attachmentName,
        CancellationToken cancellationToken)
    {
        if (!settings.Enabled || string.IsNullOrWhiteSpace(settings.Host) || string.IsNullOrWhiteSpace(settings.FromAddress))
        {
            logger.LogWarning("Invio email non configurato. Destinatario: {Recipient}, oggetto: {Subject}", recipient, subject);
            return;
        }

        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(settings.FromName, settings.FromAddress));
            message.To.Add(MailboxAddress.Parse(recipient));
            message.Subject = subject;

            var body = new BodyBuilder { HtmlBody = html };
            if (attachment is not null && !string.IsNullOrWhiteSpace(attachmentName))
            {
                body.Attachments.Add(attachmentName, attachment, new ContentType("application", "pdf"));
            }

            message.Body = body.ToMessageBody();
            using var client = new SmtpClient();
            var socketOption = settings.UseSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls;
            await client.ConnectAsync(settings.Host, settings.Port, socketOption, cancellationToken);
            if (!string.IsNullOrWhiteSpace(settings.Username))
            {
                await client.AuthenticateAsync(settings.Username, settings.Password, cancellationToken);
            }

            await client.SendAsync(message, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Invio email fallito. Destinatario: {Recipient}, oggetto: {Subject}", recipient, subject);
        }
    }

    private static byte[] CreateReceiptPdf(Ordine ordine)
    {
        QuestPDF.Settings.License = LicenseType.Community;
        return Document.Create(document => document.Page(page =>
        {
            page.Margin(40);
            page.Header().Column(column =>
            {
                column.Item().Text("OTTAVA MERAVIGLIA").FontSize(20).Bold().FontColor(Colors.Teal.Darken2);
                column.Item().Text($"Scontrino ordine {ordine.CodiceOrdine}").FontSize(12);
            });
            page.Content().PaddingTop(20).Column(column =>
            {
                column.Spacing(8);
                column.Item().Text($"Data: {ordine.DataOrdine:dd/MM/yyyy HH:mm}");
                column.Item().Text($"Cliente: {ordine.Utente?.NomeCompleto ?? ordine.Utente?.Email ?? "Cliente"}");
                column.Item().Text($"Ritiro: {ordine.ModalitaRitiro}");
                column.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                foreach (var item in ordine.OrdineProdotti)
                {
                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text($"{item.Quantita} x {item.Prodotto?.Nome ?? "Prodotto"}");
                        row.ConstantItem(90).AlignRight().Text($"{item.PrezzoUnitario * item.Quantita:C}");
                    });
                }
                column.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                column.Item().AlignRight().Text($"Totale: {ordine.Totale:C}").FontSize(15).Bold();
            });
            page.Footer().AlignCenter().Text("Grazie per aver scelto Ottava Meraviglia");
        })).GeneratePdf();
    }
}

public interface IOrderNotificationService
{
    Task SendOrderConfirmationAsync(Ordine ordine, CancellationToken cancellationToken = default);
    Task SendReservationCreatedAsync(Prenotazione prenotazione, CancellationToken cancellationToken = default);
    Task SendReservationStatusAsync(Prenotazione prenotazione, CancellationToken cancellationToken = default);
    Task SendOrderStatusAsync(Ordine ordine, CancellationToken cancellationToken = default);
}
