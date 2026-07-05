using System;
using System.IO;
using System.Drawing; // For System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "product.pdf";
        const string outputPdf = "product_verified.pdf";
        const string qrImagePath = "qr.png";
        const string description = "Scan to verify product";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(qrImagePath))
        {
            Console.Error.WriteLine($"QR code image not found: {qrImagePath}");
            return;
        }

        // Use the Facades version of PdfFileStamp (does NOT implement IDisposable)
        Aspose.Pdf.Facades.PdfFileStamp fileStamp = new Aspose.Pdf.Facades.PdfFileStamp();
        try
        {
            // Bind the source PDF document
            fileStamp.BindPdf(inputPdf);

            // ---------- QR CODE IMAGE STAMP ----------
            Aspose.Pdf.Facades.Stamp qrStamp = new Aspose.Pdf.Facades.Stamp();
            qrStamp.BindImage(qrImagePath);          // Set the QR code image
            qrStamp.SetOrigin(100, 700);             // Position (x, y) from bottom‑left
            qrStamp.SetImageSize(100, 100);          // Width and height in points
            qrStamp.Opacity = 0.9f;                  // Slight transparency
            qrStamp.IsBackground = false;            // Draw on top of page content
            fileStamp.AddStamp(qrStamp);             // Add image stamp to the document

            // ---------- DESCRIPTIVE TEXT STAMP ----------
            // Create FormattedText with System.Drawing.Color and a float font size
            Aspose.Pdf.Facades.FormattedText ft = new Aspose.Pdf.Facades.FormattedText(
                description,                         // Text to display
                System.Drawing.Color.Black,          // Text color (System.Drawing.Color)
                "Helvetica",                        // Font name
                Aspose.Pdf.Facades.EncodingType.Winansi, // Encoding
                false,                               // IsEmbedded (false = use system font)
                12f);                                // Font size (float)

            Aspose.Pdf.Facades.Stamp textStamp = new Aspose.Pdf.Facades.Stamp();
            textStamp.BindLogo(ft);                  // Attach the formatted text
            textStamp.SetOrigin(210, 720);           // Position near the QR code
            textStamp.Opacity = 0.9f;
            textStamp.IsBackground = false;
            fileStamp.AddStamp(textStamp);           // Add text stamp to the document

            // Save the stamped PDF
            fileStamp.Save(outputPdf);
        }
        finally
        {
            // Ensure resources are released and the output file is written
            fileStamp.Close();
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}
