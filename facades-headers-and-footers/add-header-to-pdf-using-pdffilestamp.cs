using System;
using System.IO;
using System.Drawing;
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

        // Bind the source PDF to PdfFileStamp
        var stamp = new PdfFileStamp();
        stamp.BindPdf(inputPath);

        // Create formatted text for the first header line using the Facades namespace and System.Drawing.Color
        var ftReport = new FormattedText(
            "Report",               // text
            Color.Black,             // text color (System.Drawing.Color)
            "Helvetica",            // font name
            EncodingType.Winansi,    // encoding
            false,                   // embed font flag
            14f);                    // font size (float)

        // Create formatted text for the second header line
        var ftConfidential = new FormattedText(
            "Confidential",
            Color.Red,
            "Helvetica",
            EncodingType.Winansi,
            false,
            14f);

        // Add the header lines. The first line uses a top margin of 15 points; the second line uses a larger margin so it appears below the first.
        stamp.AddHeader(ftReport, 15f);
        stamp.AddHeader(ftConfidential, 30f);

        // Save the stamped PDF
        stamp.Save(outputPath);
        stamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
