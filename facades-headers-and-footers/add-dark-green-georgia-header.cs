using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // FormattedText, EncodingType

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

        // Load the PDF with PdfFileStamp facade
        using (PdfFileStamp stamp = new PdfFileStamp())
        {
            stamp.BindPdf(inputPath);

            // Build formatted text for the header (dark‑green Georgia, size 12)
            FormattedText header = new FormattedText(
                "Document Header",                 // text
                System.Drawing.Color.DarkGreen,    // foreground color (System.Drawing.Color)
                "Georgia",                        // font family name
                EncodingType.Winansi,              // encoding
                false,                             // embed the font? false = use system font
                12);                               // font size

            // Add the header with a top margin of 14 points
            stamp.AddHeader(header, 14);

            // Save the stamped PDF
            stamp.Save(outputPath);
        }

        Console.WriteLine($"Header added and saved to '{outputPath}'.");
    }
}
