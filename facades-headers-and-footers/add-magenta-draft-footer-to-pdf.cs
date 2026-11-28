using System;
using System.IO;
using System.Drawing;               // for Color
using Aspose.Pdf.Facades;          // PdfFileStamp, FormattedText, EncodingType

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a FormattedText that represents the footer.
        // Parameters: text, color (System.Drawing.Color), font name, encoding, isBold, font size.
        FormattedText footer = new FormattedText(
            "Draft",
            Color.Magenta,
            "Arial",
            EncodingType.Winansi,
            false,
            12);

        // Bind the PDF and replace its footer.
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(inputPath);
            // AddFooter overload that accepts a bottom margin (in points).
            fileStamp.AddFooter(footer, bottomMargin: 5);
            fileStamp.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
