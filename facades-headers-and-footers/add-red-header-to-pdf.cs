using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // EncodingType

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

        // PdfFileStamp implements IDisposable – wrap in using for deterministic disposal
        using (PdfFileStamp stamper = new PdfFileStamp())
        {
            // Bind the source PDF document
            stamper.BindPdf(inputPath);

            // Create formatted text for the header.
            // Constructor: (string text, System.Drawing.Color textColor, FontStyle fontStyle,
            //               EncodingType encoding, bool embedFont, float fontSize, float lineSpacing = 0)
            FormattedText header = new FormattedText(
                "Document Header",
                System.Drawing.Color.Red,                     // fully‑qualified System.Drawing.Color
                Aspose.Pdf.Facades.FontStyle.Helvetica,       // FontStyle enum, not a string
                EncodingType.Winansi,
                false,
                14);

            // Add the header with a top margin of 20 points
            stamper.AddHeader(header, 20);

            // Save the stamped PDF
            stamper.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
