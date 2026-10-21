using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "product.pdf";          // source PDF
        const string outputPdfPath = "product_verified.pdf"; // stamped PDF
        const string qrImagePath = "qr_code.png";          // QR code image file
        const string verificationText = "Verify product authenticity at https://example.com/verify";

        // Ensure required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(qrImagePath))
        {
            Console.Error.WriteLine($"QR image not found: {qrImagePath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdfPath);

        // ---------- Create QR code image stamp ----------
        ImageStamp qrStamp = new ImageStamp(qrImagePath)
        {
            // Position the QR code at the bottom‑right corner of the page
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment   = VerticalAlignment.Bottom,
            // Optional margins from the page edges
            XIndent = 20, // points from the right edge
            YIndent = 20, // points from the bottom edge
            // Scale the image to a reasonable size (e.g., 100x100 points)
            Width  = 100,
            Height = 100,
            // Ensure the image is placed above existing content
            Background = false
        };

        // ---------- Create descriptive text stamp ----------
        TextStamp textStamp = new TextStamp(verificationText)
        {
            // Position the text just above the QR code
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment   = VerticalAlignment.Bottom,
            XIndent = 20,
            YIndent = 130, // QR height (100) + some padding
            Background = false
        };
        // Configure font and appearance
        textStamp.TextState.Font = FontRepository.FindFont("Arial");
        textStamp.TextState.FontSize = 12;
        textStamp.TextState.ForegroundColor = Color.DarkBlue;

        // Add the stamps to every page
        foreach (Page page in pdfDocument.Pages)
        {
            page.AddStamp(qrStamp);
            page.AddStamp(textStamp);
        }

        // Save the stamped PDF
        pdfDocument.Save(outputPdfPath);
        Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
    }
}
