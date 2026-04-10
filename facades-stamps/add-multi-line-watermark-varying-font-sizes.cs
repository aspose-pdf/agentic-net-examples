using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileStamp does not implement IDisposable, so we manage it manually.
        Aspose.Pdf.Facades.PdfFileStamp stamp = new Aspose.Pdf.Facades.PdfFileStamp();
        stamp.BindPdf(inputPath);

        // Each line of the watermark gets its own font size and vertical offset.
        var watermarkLines = new[]
        {
            new { Text = "CONFIDENTIAL",      FontSize = 48f, TopMargin = 100f },
            new { Text = "Do Not Distribute", FontSize = 36f, TopMargin = 150f },
            new { Text = "Company XYZ",       FontSize = 24f, TopMargin = 200f }
        };

        foreach (var line in watermarkLines)
        {
            // Fully‑qualified types avoid ambiguity between System.Drawing and Aspose.Pdf namespaces.
            Aspose.Pdf.Facades.FormattedText ft = new Aspose.Pdf.Facades.FormattedText(
                line.Text,
                System.Drawing.Color.Red,               // colour (System.Drawing)
                "Helvetica",                           // font name
                Aspose.Pdf.Facades.EncodingType.Winansi,
                false,                                   // do not embed the font
                line.FontSize);                          // specific font size for this line

            // AddHeader adds the text as a header (centered horizontally) and works well as a watermark.
            stamp.AddHeader(ft, line.TopMargin);
        }

        // Persist the watermarked PDF.
        stamp.Save(outputPath);
        stamp.Close(); // Release native resources.

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
