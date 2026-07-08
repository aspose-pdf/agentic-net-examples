using System;
using System.Drawing;
using System.Globalization;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class LocaleSignatureDemo
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string certFile   = "certificate.pfx";   // signing certificate
        const string certPass   = "password";           // certificate password

        // Define a rectangle for the visible signature (x, y, width, height)
        Rectangle signatureRect = new Rectangle(100, 100, 200, 100);

        // German signature (de‑DE)
        SignPdf(
            inputPdf,
            "signed_de.pdf",
            certFile,
            certPass,
            page: 1,
            rect: signatureRect,
            cultureCode: "de-DE",
            reasonLabel: "Grund",
            locationLabel: "Ort",
            contactLabel: "Kontakt",
            reason: "Ich stimme zu",
            contact: "max.mustermann@example.de",
            location: "Berlin");

        // Spanish signature (es‑ES)
        SignPdf(
            inputPdf,
            "signed_es.pdf",
            certFile,
            certPass,
            page: 1,
            rect: signatureRect,
            cultureCode: "es-ES",
            reasonLabel: "Motivo",
            locationLabel: "Ubicación",
            contactLabel: "Contacto",
            reason: "Estoy de acuerdo",
            contact: "juan.perez@example.es",
            location: "Madrid");

        // Japanese signature (ja‑JP)
        SignPdf(
            inputPdf,
            "signed_ja.pdf",
            certFile,
            certPass,
            page: 1,
            rect: signatureRect,
            cultureCode: "ja-JP",
            reasonLabel: "理由",
            locationLabel: "場所",
            contactLabel: "連絡先",
            reason: "同意します",
            contact: "taro.yamada@example.jp",
            location: "東京");
    }

    static void SignPdf(
        string inputPath,
        string outputPath,
        string certPath,
        string certPassword,
        int page,
        Rectangle rect,
        string cultureCode,
        string reasonLabel,
        string locationLabel,
        string contactLabel,
        string reason,
        string contact,
        string location)
    {
        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the facade and bind the PDF
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPath);

            // Optional: set a graphic appearance for the signature (e.g., a logo)
            // pdfSign.SignatureAppearance = "signature_logo.jpg";

            // Load the signing certificate
            pdfSign.SetCertificate(certPath, certPassword);

            // Create a PKCS#1 signature object and set basic properties
            PKCS1 signature = new PKCS1(certPath, certPassword)
            {
                Reason      = reason,
                ContactInfo = contact,
                Location    = location
            };

            // Configure locale‑specific appearance
            SignatureCustomAppearance appearance = new SignatureCustomAppearance
            {
                Culture          = new CultureInfo(cultureCode),
                ReasonLabel      = reasonLabel,
                LocationLabel    = locationLabel,
                ContactInfoLabel = contactLabel,
                // Optional: you can also customize font, colors, etc.
                FontFamilyName   = "Arial",
                FontSize         = 10,
                ForegroundColor  = Aspose.Pdf.Color.Blue,
                BackgroundColor  = Aspose.Pdf.Color.Transparent
            };

            // Assign the custom appearance to the signature
            signature.CustomAppearance = appearance;

            // Apply the signature to the specified page and rectangle
            pdfSign.Sign(page, visible: true, annotRect: rect, sig: signature);

            // Save the signed PDF
            pdfSign.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}' with locale '{cultureCode}'.");
    }
}