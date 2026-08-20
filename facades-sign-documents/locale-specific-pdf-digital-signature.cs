using System;
using System.IO;
using System.Globalization;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Example usage for three locales
        SignPdf(
            inputPath: "input.pdf",
            outputPath: "signed_de.pdf",
            certPath: "certificate.pfx",
            certPassword: "password",
            language: "de"); // German

        SignPdf(
            inputPath: "input.pdf",
            outputPath: "signed_es.pdf",
            certPath: "certificate.pfx",
            certPassword: "password",
            language: "es"); // Spanish

        SignPdf(
            inputPath: "input.pdf",
            outputPath: "signed_ja.pdf",
            certPath: "certificate.pfx",
            certPassword: "password",
            language: "ja"); // Japanese
    }

    /// <summary>
    /// Signs a PDF with locale‑specific appearance text.
    /// </summary>
    /// <param name="inputPath">Path to the source PDF.</param>
    /// <param name="outputPath">Path where the signed PDF will be saved.</param>
    /// <param name="certPath">Path to the PFX certificate.</param>
    /// <param name="certPassword">Password for the certificate.</param>
    /// <param name="language">
    /// ISO language code: "de" for German, "es" for Spanish, "ja" for Japanese.
    /// </param>
    static void SignPdf(string inputPath, string outputPath, string certPath, string certPassword, string language)
    {
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Use PKCS7 (not PKCS1) for digital signatures – it supports Reason, Location, ContactInfo, and CustomAppearance.
        var signature = new PKCS7(certPath, certPassword);
        signature.Reason = GetReasonForLanguage(language);
        signature.Location = GetLocationForLanguage(language);
        signature.ContactInfo = GetContactForLanguage(language);

        // Configure locale‑specific appearance.
        var appearance = new SignatureCustomAppearance
        {
            Culture = new CultureInfo(GetCultureName(language)),
            DigitalSignedLabel = GetDigitalSignedLabel(language),
            ReasonLabel = GetReasonLabel(language),
            LocationLabel = GetLocationLabel(language),
            DateSignedAtLabel = GetDateSignedAtLabel(language),
            ContactInfoLabel = GetContactInfoLabel(language)
        };

        // Assign the custom appearance to the signature.
        signature.CustomAppearance = appearance;

        // Define the rectangle where the signature will be placed.
        // PdfFileSignature.Sign expects System.Drawing.Rectangle.
        var rect = new System.Drawing.Rectangle(100, 100, 200, 100);

        // Use the facade to apply the signature.
        using (var pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPath);
            // Certificate is already bound via the PKCS7 object, but SetCertificate is required for internal processing.
            pdfSign.SetCertificate(certPath, certPassword);
            // Visible signature with custom appearance.
            pdfSign.Sign(page: 1, visible: true, annotRect: rect, sig: signature);
            pdfSign.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}' ({language}).");
    }

    // Helper methods to provide localized strings.

    static string GetCultureName(string language) => language switch
    {
        "de" => "de-DE",
        "es" => "es-ES",
        "ja" => "ja-JP",
        _ => "en-US"
    };

    static string GetReasonForLanguage(string language) => language switch
    {
        "de" => "Bestätigung",
        "es" => "Confirmación",
        "ja" => "確認",
        _ => "Confirmation"
    };

    static string GetContactForLanguage(string language) => language switch
    {
        "de" => "kontakt@beispiel.de",
        "es" => "contacto@ejemplo.es",
        "ja" => "contact@example.jp",
        _ => "contact@example.com"
    };

    static string GetLocationForLanguage(string language) => language switch
    {
        "de" => "Berlin, Deutschland",
        "es" => "Madrid, España",
        "ja" => "東京、日本",
        _ => "New York, USA"
    };

    static string GetDigitalSignedLabel(string language) => language switch
    {
        "de" => "Digital signiert von",
        "es" => "Firmado digitalmente por",
        "ja" => "デジタル署名者",
        _ => "Digitally signed by"
    };

    static string GetReasonLabel(string language) => language switch
    {
        "de" => "Grund",
        "es" => "Razón",
        "ja" => "理由",
        _ => "Reason"
    };

    static string GetLocationLabel(string language) => language switch
    {
        "de" => "Ort",
        "es" => "Ubicación",
        "ja" => "場所",
        _ => "Location"
    };

    static string GetDateSignedAtLabel(string language) => language switch
    {
        "de" => "Datum",
        "es" => "Fecha",
        "ja" => "日付",
        _ => "Date"
    };

    static string GetContactInfoLabel(string language) => language switch
    {
        "de" => "Kontakt",
        "es" => "Contacto",
        "ja" => "連絡先",
        _ => "Contact"
    };
}
