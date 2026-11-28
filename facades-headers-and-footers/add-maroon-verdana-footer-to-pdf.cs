using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

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

        // Create formatted text for the footer using the constructor that sets text, color, font, encoding, embed flag and size
        FormattedText footer = new FormattedText(
            "Maroon Verdana Footer",
            System.Drawing.Color.FromArgb(128, 0, 0), // maroon color
            "Verdana",
            EncodingType.Winansi,
            false,
            12);

        // Stamp the PDF with the footer. Use the AddFooter overload that accepts a bottom margin.
        using (PdfFileStamp stamp = new PdfFileStamp())
        {
            stamp.BindPdf(inputPath);
            stamp.AddFooter(footer, 22f); // 22 points bottom margin
            stamp.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}