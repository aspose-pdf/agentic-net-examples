using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing; // for System.Drawing.Color

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

        // Initialize the PdfFileStamp facade using the modern API.
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath); // replaces the obsolete InputFile property

        // Create a stamp that will be used as a background watermark.
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // Build the formatted text for the watermark.
        // NOTE: Use System.Drawing.Color for the color argument and a float for the font size.
        Aspose.Pdf.Facades.FormattedText ft = new Aspose.Pdf.Facades.FormattedText(
            "CONFIDENTIAL",               // text
            System.Drawing.Color.Gray,    // text color (System.Drawing.Color)
            "Helvetica",                 // font name
            EncodingType.Winansi,         // encoding
            false,                        // embed font?
            48f);                         // font size (float)

        stamp.BindLogo(ft);               // bind the formatted text to the stamp
        stamp.IsBackground = true;        // place the stamp behind existing page content

        // Apply the stamp only to pages 2 through 5 (1‑based indexing).
        stamp.Pages = new int[] { 2, 3, 4, 5 };

        // Add the stamp to the document.
        fileStamp.AddStamp(stamp);

        // Save the modified PDF using the modern API.
        fileStamp.Save(outputPath); // replaces the obsolete OutputFile property
        fileStamp.Close();

        Console.WriteLine($"Background watermark applied to pages 2‑5 and saved as '{outputPath}'.");
    }
}
