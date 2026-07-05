using System;
using System.IO;
using System.Drawing;                     // Required for System.Drawing.Color
using Aspose.Pdf.Facades;                // PdfFileMend API
using Aspose.Pdf.Text;                    // FormattedText, EncodingType

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string promoText  = "Special Offer: Get 20% off!";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create formatted text for the promotional message (all styling via constructor)
        FormattedText formatted = new FormattedText(
            promoText,               // text
            Color.Black,            // text color (System.Drawing.Color)
            "Helvetica",           // font name
            EncodingType.Winansi,   // encoding
            false,                  // embed font flag
            12);                    // font size

        // Pages on which the text should appear (1‑based indexing)
        int[] targetPages = new int[] { 3, 5, 7 };

        // Use PdfFileMend (the class that provides AddText with a page array)
        using (PdfFileMend mend = new PdfFileMend())
        {
            mend.BindPdf(inputPath);
            // lowerLeftX, lowerLeftY, upperRightX, upperRightY are in points.
            mend.AddText(formatted, targetPages, 100f, 700f, 500f, 750f);
            mend.Save(outputPath);
        }

        Console.WriteLine($"Promotional message added to pages 3, 5, and 7. Saved as '{outputPath}'.");
    }
}
