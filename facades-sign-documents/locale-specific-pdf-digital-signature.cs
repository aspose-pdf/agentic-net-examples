using System;
using System.IO;
using System.Globalization;
using System.Drawing; // Needed for System.Drawing.Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    // Path to a digital certificate (PFX) used for signing. Replace with a real certificate if needed.
    private const string CertificatePath = "cert.pfx";
    private const string CertificatePassword = "password";

    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // ---------------------------------------------------------------------
            // Helper that signs a given page with locale‑specific appearance settings
            // ---------------------------------------------------------------------
            void SignPage(int pageNumber, string imagePath, string contactInfo, string locationInfo,
                          string reason, string signatureLabel, string culture)
            {
                // Ensure the requested page exists
                if (doc.Pages.Count < pageNumber) return;

                // Create the PKCS7 object that carries the digital signature data
                PKCS7 pkcs7 = new PKCS7(CertificatePath, CertificatePassword)
                {
                    Reason = reason,
                    Location = locationInfo,
                    ContactInfo = contactInfo,
                    // Configure the visual appearance of the signature
                    CustomAppearance = new SignatureCustomAppearance
                    {
                        // The label that appears next to the signature graphic (e.g., "Unterschrift")
                        ReasonLabel = signatureLabel,
                        // Show the standard fields – they will be filled with the values above
                        ShowContactInfo = true,
                        ShowLocation = true,
                        ShowReason = true,
                        // Use a font that can render German, Spanish and Japanese characters
                        FontFamilyName = "Arial Unicode MS",
                        FontSize = 12,
                        // Culture influences date formatting and default label localisation
                        Culture = new CultureInfo(culture),
                        // Draw the image (if any) in the foreground of the signature rectangle
                        IsForegroundImage = true
                    }
                };

                // The image that represents the handwritten signature (optional)
                PdfFileSignature pdfSignature = new PdfFileSignature(doc);
                pdfSignature.SignatureAppearance = imagePath; // path to PNG/JPG of the handwritten signature

                // Define where on the page the signature will be placed.
                // System.Drawing.Rectangle uses (x, y, width, height).
                // Here we place the signature at (100,100) with a size of 150x50 points.
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 150, 50);

                // Sign the page. The overload with (pageNumber, true, rect, pkcs7) creates a visible signature.
                pdfSignature.Sign(pageNumber, true, rect, pkcs7);
            }

            // German appearance (de‑DE)
            SignPage(
                pageNumber: 1,
                imagePath: "signature_german.png",
                contactInfo: "Kontakt: Max Mustermann",
                locationInfo: "Berlin, Deutschland",
                reason: "Dokument unterschrieben",
                signatureLabel: "Unterschrift",
                culture: "de-DE"
            );

            // Spanish appearance (es‑ES)
            SignPage(
                pageNumber: 2,
                imagePath: "signature_spanish.png",
                contactInfo: "Contacto: Juan Pérez",
                locationInfo: "Madrid, España",
                reason: "Documento firmado",
                signatureLabel: "Firma",
                culture: "es-ES"
            );

            // Japanese appearance (ja‑JP)
            SignPage(
                pageNumber: 3,
                imagePath: "signature_japanese.png",
                contactInfo: "連絡先: 山田太郎",
                locationInfo: "東京、日本",
                reason: "文書に署名しました",
                signatureLabel: "署名",
                culture: "ja-JP"
            );

            // Save the signed PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
