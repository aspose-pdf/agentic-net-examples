using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // required for PKCS7 and SignatureCustomAppearance

class LocaleSignatureDemo
{
    static void Main()
    {
        // Input PDF to be signed
        const string inputPdf = "document.pdf";
        // Output signed PDF
        const string outputPdf = "document_signed.pdf";
        // Path to the signing certificate (PFX)
        const string certPath = "certificate.pfx";
        // Certificate password
        const string certPassword = "certPassword";

        // Choose language: "de" for German, "es" for Spanish, "ja" for Japanese
        string languageCode = "de";

        // Validate files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate not found: {certPath}");
            return;
        }

        // Create a PdfFileSignature instance (no using – it does not implement IDisposable)
        PdfFileSignature pdfSignature = new PdfFileSignature();
        pdfSignature.BindPdf(inputPdf);

        // Prepare locale‑specific data
        string contactInfo = "support@example.com";
        string location = GetLocationForLanguage(languageCode);
        string reason = GetReasonForLanguage(languageCode);

        // Create PKCS7 object and assign basic signature properties
        PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
        {
            ContactInfo = contactInfo,
            Location = location,          // Fixed: use Location property
            Reason = reason               // Fixed: use Reason property
        };

        // Optional: customise visual appearance of the signature
        SignatureCustomAppearance customAppearance = new SignatureCustomAppearance
        {
            ShowContactInfo = true,
            ShowLocation = true,
            ShowReason = true,
            // You can also set background/foreground images or colors here if needed
        };
        pkcs7.CustomAppearance = customAppearance;

        // Sign the document using the PKCS7 object (the overload that accepts PKCS7)
        pdfSignature.Sign(outputPdf, pkcs7);

        Console.WriteLine($"PDF signed successfully for language '{languageCode}'. Output: {outputPdf}");
    }

    // Returns a locale‑specific reason text for the digital signature
    private static string GetReasonForLanguage(string lang)
    {
        switch (lang.ToLower())
        {
            case "de": // German
                return "Unterschrift des Dokuments";
            case "es": // Spanish
                return "Firma del documento";
            case "ja": // Japanese
                return "文書への署名";
            default:
                return "Document signature";
        }
    }

    // Returns a locale‑specific location text for the digital signature
    private static string GetLocationForLanguage(string lang)
    {
        switch (lang.ToLower())
        {
            case "de":
                return "Berlin, Deutschland";
            case "es":
                return "Madrid, España";
            case "ja":
                return "東京、日本";
            default:
                return "Unknown Location";
        }
    }
}
