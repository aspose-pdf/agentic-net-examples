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

        // Create formatted text containing the page count placeholder.
        // Note: FormattedText expects a System.Drawing.Color and a float for font size.
        FormattedText footerText = new FormattedText(
            "{page_count}",
            System.Drawing.Color.Black,
            "Helvetica",
            EncodingType.Winansi,
            false,
            12f);

        // Initialize the PdfFileStamp facade and bind the source PDF.
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);

        // Add the footer with a bottom margin of 10 points.
        fileStamp.AddFooter(footerText, 10f);

        // Save the stamped PDF.
        fileStamp.Save(outputPath);
        fileStamp.Close();

        Console.WriteLine($"Footer with page count added and saved to '{outputPath}'.");
    }
}
