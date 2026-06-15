using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing; // for System.Drawing.Color

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string promoText = "Special Offer: 20% OFF!";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create formatted text for the promotional message using System.Drawing.Color and a float font size
        FormattedText formatted = new FormattedText(
            promoText,                 // text
            System.Drawing.Color.Red, // text color
            "Helvetica",             // font name
            EncodingType.Winansi,     // encoding
            false,                    // embed font flag
            24f);                     // font size (float)

        // Pages on which the text should appear (1‑based indexing)
        int[] pages = { 3, 5, 7 };

        // Use PdfFileMend to add the text to the specified pages
        PdfFileMend mend = new PdfFileMend();
        mend.BindPdf(inputPath);
        // AddText parameters: formatted text, page numbers, lower‑left X/Y, upper‑right X/Y
        mend.AddText(formatted, pages, 100f, 500f, 300f, 550f);
        mend.Save(outputPath);
        mend.Close(); // releases resources

        Console.WriteLine($"Promotional message added to pages 3, 5, and 7. Saved as '{outputPath}'.");
    }
}
