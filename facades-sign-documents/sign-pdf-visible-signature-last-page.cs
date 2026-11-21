using System;
using System.IO;
using System.Drawing; // needed for Color and Rectangle used by PdfFileSignature
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // PKCS7, SignatureCustomAppearance

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Determine the last page and its dimensions
        int lastPageNumber;
        System.Drawing.Rectangle signatureRect; // fully qualified to avoid ambiguity
        using (Document doc = new Document(inputPath))
        {
            lastPageNumber = doc.Pages.Count; // 1‑based indexing
            double pageWidth = doc.Pages[lastPageNumber].PageInfo.Width;
            double pageHeight = doc.Pages[lastPageNumber].PageInfo.Height;

            // Width = 150 points, Height = 50 points, placed at bottom‑right corner
            int rectX = (int)Math.Round(pageWidth - 150);
            int rectY = 0; // bottom of the page (Y = 0 in Aspose PDF coordinate system)
            int rectWidth = 150;
            int rectHeight = 50;
            signatureRect = new System.Drawing.Rectangle(rectX, rectY, rectWidth, rectHeight);
        }

        // Create PKCS7 signature object (loads the certificate internally)
        PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
        {
            Reason = "Approved",
            Location = "Office",
            ContactInfo = "John Doe",
            // Optional custom appearance – you can adjust colors, fonts, etc.
            CustomAppearance = new SignatureCustomAppearance
            {
                FontSize = 8,
                ShowReason = true,
                ShowLocation = true,
                ShowContactInfo = true,
                // Convert System.Drawing.Color to Aspose.Pdf.Color using FromRgb
                BackgroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.FromArgb(80, 173, 216, 230))
            }
        };

        // Sign the PDF using the facade API
        PdfFileSignature pdfSigner = new PdfFileSignature();
        pdfSigner.BindPdf(inputPath);
        // If you have an image you want to use as the signature graphic, set the path here:
        // pdfSigner.SignatureAppearance = "signature.png";
        pdfSigner.Sign(
            lastPageNumber,          // page number (1‑based)
            pkcs7.Reason,           // reason
            pkcs7.ContactInfo,      // contact
            pkcs7.Location,         // location
            true,                   // visible = true (appearance will be drawn)
            signatureRect,          // rectangle where the signature will appear (System.Drawing.Rectangle)
            pkcs7);                 // the PKCS7 signature object

        pdfSigner.Save(outputPath);
        pdfSigner.Close();

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
