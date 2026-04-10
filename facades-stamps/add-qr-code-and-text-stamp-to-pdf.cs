using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // for EncodingType and FormattedText

class ProductVerificationStamp
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "product_catalog.pdf";
        const string qrImagePath    = "qr_code.png";
        const string outputPdfPath  = "product_catalog_with_stamp.pdf";

        // Ensure input files exist
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

        // Initialize the PdfFileStamp facade (does NOT implement IDisposable)
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile  = inputPdfPath;
        fileStamp.OutputFile = outputPdfPath;

        // Bind the source PDF to the facade
        fileStamp.BindPdf(inputPdfPath);

        // Create a Aspose.Pdf.Facades.Stamp object
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // Position the stamp on the page (origin is lower‑left corner)
        stamp.SetOrigin(100, 150);          // X = 100, Y = 150 (adjust as needed)
        stamp.SetImageSize(120, 120);       // Size of the QR code image
        stamp.Opacity = 0.9f;               // Slight transparency
        stamp.IsBackground = false;         // Draw on top of page content

        // Bind the QR code image to the stamp
        stamp.BindImage(qrImagePath);

        // Create formatted text for the descriptive label
        // Note: FormattedText uses System.Drawing.Color (Windows‑only) for the text color
        FormattedText ft = new FormattedText(
            "Product Verified",                     // Text to display
            System.Drawing.Color.Black,             // Text color
            "Helvetica",                            // Font name
            EncodingType.Winansi,                   // Encoding
            false,                                  // Do not embed the font
            12);                                    // Font size

        // Bind the text to the same stamp (image + text)
        stamp.BindLogo(ft);

        // Add the stamp to the PDF file
        fileStamp.AddStamp(stamp);

        // Finalize and save the output PDF
        fileStamp.Close();

        Console.WriteLine($"Aspose.Pdf.Facades.Stamp added successfully. Output saved to '{outputPdfPath}'.");
    }
}