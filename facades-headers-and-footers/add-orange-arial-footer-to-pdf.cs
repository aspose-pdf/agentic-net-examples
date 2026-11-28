using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and prepare for stamping
        using (PdfFileStamp stamp = new PdfFileStamp())
        {
            stamp.BindPdf(inputPath);

            // Create an orange Arial footer (font size 12) using FormattedText constructor
            FormattedText footer = new FormattedText(
                "Footer text",                 // text
                Color.Orange,                   // text color (System.Drawing.Color)
                "Arial",                       // font name
                EncodingType.Winansi,           // encoding
                false,                          // embed flag
                12f);                           // font size

            // Add the footer to all pages with a bottom margin of 8 points
            stamp.AddFooter(footer, 8f);

            // Save the stamped PDF
            stamp.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
