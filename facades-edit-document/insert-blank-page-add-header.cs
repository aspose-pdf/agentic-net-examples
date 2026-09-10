using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // for FormattedText, EncodingType

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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Insert a blank page at position 3 (pages are 1‑based)
            // If the document has fewer than 2 pages, Insert will add at the end.
            doc.Pages.Insert(3);

            // Prepare header text using FormattedText (requires System.Drawing.Color)
            FormattedText header = new FormattedText(
                "Document Header",                     // text
                System.Drawing.Color.DarkBlue,         // text color
                "Helvetica",                           // font name
                EncodingType.Winansi,                  // encoding
                false,                                 // embed font?
                14);                                   // font size

            // Use PdfFileStamp facade to add the header to all pages
            // (constructor with Document follows the provided creation pattern)
            PdfFileStamp stamp = new PdfFileStamp(doc);
            // Add header with a top margin of 20 points
            stamp.AddHeader(header, 20);
            // Save the modified PDF (facade handles saving)
            stamp.Save(outputPath);
            // Close the facade (releases internal resources)
            stamp.Close();
        }

        Console.WriteLine($"PDF saved with new page and header: '{outputPath}'.");
    }
}