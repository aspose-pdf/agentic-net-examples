using System;
using System.IO;
using System.Drawing;                     // For System.Drawing.Color
using Aspose.Pdf.Facades;                // Facade classes: PdfFileStamp, Stamp, FormattedText, EncodingType

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileStamp facade (no using – it does not implement IDisposable)
        PdfFileStamp fileStamp = new PdfFileStamp();

        // Load the source PDF
        fileStamp.BindPdf(inputPath);

        // Create a stamp object
        Stamp stamp = new Stamp();

        // Make the stamp a background watermark
        stamp.IsBackground = true;

        // Apply the stamp only to pages 2 through 5 (1‑based indexing)
        stamp.Pages = new int[] { 2, 3, 4, 5 };

        // Prepare formatted text for the watermark
        FormattedText ft = new FormattedText(
            "CONFIDENTIAL",                 // Text to display
            Color.LightGray,                // Text color (System.Drawing.Color)
            "Helvetica",                    // Font name
            EncodingType.Winansi,           // Text encoding
            false,                          // Do not embed the font
            72);                            // Font size

        // Bind the formatted text to the stamp
        stamp.BindLogo(ft);

        // Add the configured stamp to the PDF
        fileStamp.AddStamp(stamp);

        // Save the resulting PDF
        fileStamp.Save(outputPath);

        // Release resources
        fileStamp.Close();

        Console.WriteLine($"Background watermark applied to pages 2‑5 and saved as '{outputPath}'.");
    }
}