using System;
using System.IO;
using System.Drawing; // for Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ProductVerificationStamp
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "product_catalog.pdf";   // PDF to stamp
        const string outputPdfPath  = "product_catalog_stamped.pdf";
        const string qrCodeImagePath = "qr_code.png";         // QR code image file
        const string verificationText = "Verify product at https://example.com/verify";

        // Ensure files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(qrCodeImagePath))
        {
            Console.Error.WriteLine($"QR code image not found: {qrCodeImagePath}");
            return;
        }

        // Initialize PdfFileStamp facade using the non‑obsolete API
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdfPath);

        // Create a stamp (Aspose.Pdf.Facades.Stamp)
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // Position the stamp (origin is lower‑left corner of the page)
        stamp.SetOrigin(100, 500);          // X = 100, Y = 500 (adjust as needed)
        stamp.SetImageSize(120, 120);       // Width = 120, Height = 120
        stamp.Opacity = 0.85f;
        stamp.IsBackground = false;

        // Bind the QR code image to the stamp
        stamp.BindImage(qrCodeImagePath);

        // Prepare formatted text for the descriptive message
        // Use System.Drawing.Color, fully‑qualified Facades types, and a float font size
        Aspose.Pdf.Facades.FormattedText formattedText = new Aspose.Pdf.Facades.FormattedText(
            verificationText,
            System.Drawing.Color.Black,               // Text color
            "Helvetica",                             // Font name
            Aspose.Pdf.Facades.EncodingType.Winansi, // Encoding
            false,                                    // Do not embed the font
            12f);                                     // Font size (float)

        // Bind the formatted text (logo) to the same stamp – it will appear beside the image
        stamp.BindLogo(formattedText);

        // Add the composed stamp to all pages of the PDF
        fileStamp.AddStamp(stamp);

        // Save the stamped PDF and release resources
        fileStamp.Save(outputPdfPath);
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
    }
}
