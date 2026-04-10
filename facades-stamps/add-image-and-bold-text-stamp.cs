using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and logo image paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string logoPath  = "logo.png";

        // Verify files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Initialize the facade and bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);

        // Create a stamp instance (fully qualified to avoid ambiguity)
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // Bind the company logo image to the stamp
        stamp.BindImage(logoPath);
        // Position the stamp (example coordinates) and set image size
        stamp.SetOrigin(100, 500);          // X, Y position on the page
        stamp.SetImageSize(100, 100);       // Width, Height of the image

        // Create formatted text for the word "Confidential" in bold
        // Using a bold font (Helvetica-Bold) and red color as an example
        FormattedText confidentialText = new FormattedText(
            "Confidential",                     // Text
            System.Drawing.Color.Red,           // Text color
            "Helvetica-Bold",                   // Bold font name
            EncodingType.Winansi,               // Encoding
            false,                              // Not embedded
            36);                                // Font size

        // Bind the formatted text to the same stamp
        stamp.BindLogo(confidentialText);

        // Optional visual settings
        stamp.IsBackground = true;   // Place stamp behind page content
        stamp.Opacity = 0.7f;        // Semi‑transparent

        // Add the combined image‑and‑text stamp to the PDF
        fileStamp.AddStamp(stamp);

        // Save the result and release resources
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}