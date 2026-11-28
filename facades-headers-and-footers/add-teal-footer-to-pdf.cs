using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // FormattedText, EncodingType

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_footer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileStamp implements IDisposable – wrap it in a using block.
        using (PdfFileStamp stamp = new PdfFileStamp())
        {
            // Load the source PDF.
            stamp.BindPdf(inputPath);

            // Create formatted text for the footer (teal, Courier, size 12).
            var footer = new FormattedText(
                "This is a teal footer.",
                System.Drawing.Color.FromArgb(0, 128, 128), // fully‑qualified System.Drawing.Color
                Aspose.Pdf.Facades.FontStyle.Courier,          // FontStyle enum, not a string
                EncodingType.Winansi,
                false,
                12f);

            // Add the footer to every page. Bottom margin = 9 points, left/right margins = 0.
            stamp.AddFooter(footer, 9f, 0f, 0f);

            // Save the modified PDF.
            stamp.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with footer to '{outputPath}'.");
    }
}
