using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing;

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

        // Bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);

        // Create a stamp object (fully qualified to avoid ambiguity)
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

        // Build formatted text via constructor (font, size, color, encoding)
        // Use an extra blank line ("\n\n") to simulate 1.5 line spacing between the two lines.
        Aspose.Pdf.Facades.FormattedText formattedText = new Aspose.Pdf.Facades.FormattedText(
            "CONFIDENTIAL\n\nDO NOT DISTRIBUTE",
            System.Drawing.Color.Red,
            "Helvetica",
            Aspose.Pdf.Facades.EncodingType.Winansi,
            false,
            48);

        // Bind the formatted text to the stamp
        stamp.BindLogo(formattedText);

        // Configure stamp appearance
        stamp.IsBackground = true;   // place behind page content
        stamp.Opacity = 0.5f;        // semi‑transparent

        // Apply the stamp to all pages
        fileStamp.AddStamp(stamp);

        // Save the result
        fileStamp.Save(outputPath);
        fileStamp.Close();

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
