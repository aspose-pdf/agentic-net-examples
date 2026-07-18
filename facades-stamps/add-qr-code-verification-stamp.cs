using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;          // TextStamp, FontRepository, FontStyles

class ProductVerificationStamp
{
    static void Main()
    {
        const string inputPdfPath  = "product_catalog.pdf";   // source PDF
        const string outputPdfPath = "product_catalog_stamped.pdf";
        const string qrImagePath   = "qr_code.png";          // QR code image file
        const string description   = "Product Verification\nScan QR code to verify authenticity";

        // ------------------------------------------------------------
        // Validate required files
        // ------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(qrImagePath))
        {
            Console.Error.WriteLine($"QR code image not found: {qrImagePath}");
            return;
        }

        // ------------------------------------------------------------
        // Load the PDF document
        // ------------------------------------------------------------
        Document pdfDocument = new Document(inputPdfPath);

        // ------------------------------------------------------------
        // 1) Image stamp – the QR code
        // ------------------------------------------------------------
        ImageStamp qrStamp = new ImageStamp(qrImagePath)
        {
            Background = false,          // draw on top of page content
            Opacity    = 0.9f,
            XIndent    = 100f,           // position from left (points)
            YIndent    = 500f,           // position from bottom (points)
            Width      = 120f,
            Height     = 120f
        };

        // ------------------------------------------------------------
        // 2) Text stamp – descriptive verification text
        // ------------------------------------------------------------
        TextStamp txtStamp = new TextStamp(description)
        {
            Background = false,
            Opacity    = 0.9f,
            XIndent    = 240f,
            YIndent    = 500f
        };
        txtStamp.TextState.Font = FontRepository.FindFont("Helvetica");
        txtStamp.TextState.FontSize = 12;
        txtStamp.TextState.FontStyle = FontStyles.Regular;
        txtStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

        // ------------------------------------------------------------
        // Add both stamps to every page (or change to a specific page set)
        // ------------------------------------------------------------
        foreach (Page page in pdfDocument.Pages)
        {
            page.AddStamp(qrStamp);
            page.AddStamp(txtStamp);
        }

        // ------------------------------------------------------------
        // Save the stamped PDF
        // ------------------------------------------------------------
        pdfDocument.Save(outputPdfPath);
        Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
    }
}
