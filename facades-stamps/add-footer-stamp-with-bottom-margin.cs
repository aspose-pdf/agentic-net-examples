using System;
using System.IO;
using System.Drawing; // for Color
using Aspose.Pdf.Facades;
using Aspose.Pdf.Facades; // for FormattedText, EncodingType

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

        // Create formatted text for the footer (text, color, font, encoding, embed flag, font size)
        var footerText = new FormattedText(
            "Confidential Footer",          // text
            System.Drawing.Color.Black,      // text color (System.Drawing.Color)
            "Helvetica",                    // font name
            EncodingType.Winansi,            // encoding
            false,                           // embed font
            10f);                            // font size (float)

        // Initialize PdfFileStamp, bind the source PDF, add footer with 10‑point bottom margin, save and close.
        var fileStamp = new PdfFileStamp();
        try
        {
            fileStamp.BindPdf(inputPath);          // load source PDF
            fileStamp.AddFooter(footerText, 10f);  // 10 points above bottom edge
            fileStamp.Save(outputPath);            // write result
        }
        finally
        {
            fileStamp.Close();                     // release resources
        }

        Console.WriteLine($"Footer stamp added. Output saved to '{outputPath}'.");
    }
}
