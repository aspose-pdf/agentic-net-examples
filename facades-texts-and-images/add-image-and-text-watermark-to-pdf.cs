using System;
using System.IO;
using System.Drawing;                     // Required for System.Drawing.Color
using Aspose.Pdf.Facades;                // Facade classes (PdfFileStamp, Stamp, FormattedText, EncodingType)
using Aspose.Pdf;                        // Core PDF classes
using Aspose.Pdf.Text;                   // Not needed for FormattedText when using Facades, but kept for completeness

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";   // PDF to be watermarked
        const string outputPdfPath  = "watermarked.pdf";
        const string imagePath      = "logo.png";    // Image part of the watermark
        const string watermarkText  = "CONFIDENTIAL";

        // Ensure input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // PdfFileStamp works with facades; it implements IDisposable.
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Bind the source PDF
            fileStamp.BindPdf(inputPdfPath);

            // ---------- Image stamp (semi‑transparent, placed as background) ----------
            Aspose.Pdf.Facades.Stamp imgStamp = new Aspose.Pdf.Facades.Stamp();
            imgStamp.BindImage(imagePath);          // Use the image file
            imgStamp.IsBackground = true;           // Render behind page content
            imgStamp.Opacity = 0.5f;                // 50 % opacity
            imgStamp.SetOrigin(100, 400);           // Position on the page (adjust as needed)
            imgStamp.SetImageSize(200, 200);        // Scale the image (width, height)
            fileStamp.AddStamp(imgStamp);           // Add the image stamp

            // ---------- Text stamp (semi‑transparent, rendered over the image) ----------
            // Use Facades.FormattedText with System.Drawing.Color and a float font size
            FormattedText ft = new FormattedText(
                watermarkText,                     // Text to display
                System.Drawing.Color.Red,          // Text color (System.Drawing.Color)
                "Helvetica",                     // Font name
                EncodingType.Winansi,              // Encoding (Facades.EncodingType)
                false,                             // IsEmbedded (false = use system font)
                48f);                              // Font size (float)

            Aspose.Pdf.Facades.Stamp txtStamp = new Aspose.Pdf.Facades.Stamp();
            txtStamp.BindLogo(ft);                  // Bind the formatted text
            txtStamp.IsBackground = false;          // Render over the image stamp (foreground)
            txtStamp.Opacity = 0.5f;                // 50 % opacity for the text
            txtStamp.SetOrigin(120, 420);           // Position (adjust to align with image)
            fileStamp.AddStamp(txtStamp);           // Add the text stamp

            // Save the resulting PDF
            fileStamp.Save(outputPdfPath);
            // fileStamp.Close(); // Not required – using statement disposes it
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdfPath}'.");
    }
}
