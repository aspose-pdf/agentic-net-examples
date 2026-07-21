using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing; // Required for System.Drawing.Color

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

        // Load the source PDF and insert a blank page at position 3 (1‑based index)
        using (Document doc = new Document(inputPath))
        {
            doc.Pages.Insert(3);

            // Prepare header text using Facades.FormattedText (System.Drawing.Color, float size)
            Aspose.Pdf.Facades.FormattedText headerText = new Aspose.Pdf.Facades.FormattedText(
                "Document Header",          // text
                System.Drawing.Color.Black, // text color (System.Drawing.Color)
                "Helvetica",                // font name
                Aspose.Pdf.Facades.EncodingType.Winansi, // encoding
                false,                       // embed font?
                12f);                        // font size (float)

            // Use PdfFileStamp to add the header to all pages
            using (PdfFileStamp stamp = new PdfFileStamp())
            {
                stamp.BindPdf(doc);
                stamp.AddHeader(headerText, 20); // top margin of 20 points
                stamp.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF processed successfully. Output saved to '{outputPath}'.");
    }
}
