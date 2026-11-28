using System;
using System.IO;
using System.Drawing;                     // for System.Drawing.Color
using Aspose.Pdf;                         // for Document
using Aspose.Pdf.Facades;                 // for PdfFileStamp, FormattedText, EncodingType

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Initialise the stamp facade with the loaded document
            using (PdfFileStamp stamp = new PdfFileStamp(pdfDoc))
            {
                // Create formatted text for the header using the string‑font overload.
                // System.Drawing.Color is fully qualified to avoid the ambiguous reference
                // with Aspose.Pdf.Color.
                FormattedText header = new FormattedText(
                    "Document Header",          // text
                    System.Drawing.Color.Green,   // text colour (System.Drawing.Color)
                    "Times New Roman",          // font name (string overload)
                    EncodingType.Winansi,        // encoding
                    false,                       // embed the font?
                    12);                         // font size

                // Add the header with a top margin of 12 points
                stamp.AddHeader(header, 12f);

                // Save the modified PDF
                stamp.Save(outputPath);
            }
        }

        Console.WriteLine($"Header added and saved to '{outputPath}'.");
    }
}
