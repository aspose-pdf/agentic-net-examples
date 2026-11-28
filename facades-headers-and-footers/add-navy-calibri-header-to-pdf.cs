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

        // Create a FormattedText object for the header.
        // NOTE: Use fully‑qualified System.Drawing.Color to avoid ambiguity with Aspose.Pdf.Color.
        //       Use Aspose.Pdf.Facades.FontStyle enum (not a string) for the font style argument.
        FormattedText header = new FormattedText(
            "Navy Calibri Header",                 // text
            System.Drawing.Color.Navy,              // text color (System.Drawing.Color)
            Aspose.Pdf.Facades.FontStyle.Helvetica, // font style enum (Aspose.Pdf.Facades.FontStyle)
            EncodingType.Winansi,                   // encoding
            false,                                   // embed font flag
            12);                                     // font size

        // Use PdfFileStamp to add the header and save the PDF.
        using (PdfFileStamp stamp = new PdfFileStamp())
        {
            stamp.BindPdf(inputPath);   // Load the PDF
            stamp.AddHeader(header, 18); // Add header with top margin of 18 points
            stamp.Save(outputPath);      // Save the modified PDF
        }

        Console.WriteLine($"Header added and saved to '{outputPath}'.");
    }
}
