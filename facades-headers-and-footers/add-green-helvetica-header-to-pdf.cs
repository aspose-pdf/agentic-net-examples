using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Color
using Aspose.Pdf;
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

        // Load the PDF with PdfFileStamp (facade API)
        using (PdfFileStamp stamper = new PdfFileStamp())
        {
            // Bind the source PDF file
            stamper.BindPdf(inputPath);

            // Create formatted text for the header – use fully‑qualified System.Drawing.Color
            // and the correct FontStyle enum (Aspose.Pdf.Facades.FontStyle)
            FormattedText header = new FormattedText(
                "Document Header",                     // text
                System.Drawing.Color.Green,             // text colour (System.Drawing.Color)
                Aspose.Pdf.Facades.FontStyle.Helvetica, // font style enum, not a string
                Aspose.Pdf.Facades.EncodingType.Winansi,
                false,                                   // embed the font?
                12);                                      // font size

            // Add the header with a top margin of 10 points
            stamper.AddHeader(header, 10);

            // Save the stamped PDF
            stamper.Save(outputPath);
        }

        Console.WriteLine($"Header added and saved to '{outputPath}'.");
    }
}
