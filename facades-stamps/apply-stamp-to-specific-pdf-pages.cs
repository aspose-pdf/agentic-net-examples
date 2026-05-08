using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create formatted text for the stamp – use System.Drawing.Color and a float font size
        var ft = new FormattedText(
            "Sample Aspose.Pdf.Facades.Stamp", // text to display
            Color.Red,                         // System.Drawing.Color (fully qualified via using)
            "Helvetica",                      // font name
            EncodingType.Winansi,              // encoding
            false,                             // embed font flag
            36f);                              // font size as float

        // Pages that should receive the stamp (1‑based indices)
        int[] pages = { 1, 5, 10 };

        // Use PdfFileMend to add the text to the selected pages
        var mend = new PdfFileMend();
        mend.BindPdf(inputPath);

        // Position the stamp – lower‑left (100,500) to upper‑right (300,550)
        mend.AddText(ft, pages, 100f, 500f, 300f, 550f);

        // Save the modified PDF and release resources
        mend.Save(outputPath);
        mend.Close();

        Console.WriteLine($"Stamp applied to pages 1, 5, and 10. Saved as '{outputPath}'.");
    }
}
