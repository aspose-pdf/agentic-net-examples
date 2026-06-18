using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // required for FormattedText and EncodingType

class ProductVerificationStamp
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "product_catalog.pdf";
        const string outputPdfPath  = "product_catalog_with_stamp.pdf";
        const string qrImagePath    = "qr_code.png"; // QR code image file
        const string verificationText = "Scan to verify product";

        // Verify input files exist
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

        // Initialize the PdfFileStamp facade and bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdfPath); // loads the PDF to be stamped

        // Create a Aspose.Pdf.Facades.Stamp object (facade stamp)
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // ----- Image part (QR code) -----
        // Bind the QR code image to the stamp
        stamp.BindImage(qrImagePath);
        // Position the stamp on the page (origin is lower‑left corner)
        stamp.SetOrigin(100f, 150f); // X = 100, Y = 150 (adjust as needed)
        // Define the displayed size of the image
        stamp.SetImageSize(120f, 120f); // width = 120, height = 120

        // ----- Text part (descriptive verification text) -----
        // FormattedText constructor requires System.Drawing.Color
        FormattedText ft = new FormattedText(
            verificationText,                     // text
            System.Drawing.Color.Black,           // text color
            "Helvetica",                          // font name
            EncodingType.Winansi,                 // encoding
            false,                                // isEmbedded (false = use system font)
            12);                                  // font size

        // Bind the formatted text to the same stamp (text will appear alongside the image)
        stamp.BindLogo(ft);

        // Optional visual settings
        stamp.IsBackground = false;   // draw on top of page content
        stamp.Opacity = 0.85f;        // semi‑transparent

        // Add the prepared stamp to the PDF
        fileStamp.AddStamp(stamp);

        // Save the stamped PDF to the output file
        fileStamp.Save(outputPdfPath);
        fileStamp.Close(); // releases resources

        Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
    }
}