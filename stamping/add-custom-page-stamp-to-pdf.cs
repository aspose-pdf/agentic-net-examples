using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (document-disposal-with-using rule)
        using (Document doc = new Document(inputPath))
        {
            // Select the page to stamp (page-indexing-one-based rule)
            Page targetPage = doc.Pages[1];

            // Create a PdfPageStamp using a source page.
            // Here we reuse the same page as the stamp source for simplicity.
            PdfPageStamp stamp = new PdfPageStamp(targetPage);

            // Set custom dimensions for the stamp
            stamp.Width  = 200;   // desired width
            stamp.Height = 100;   // desired height

            // Position the stamp within the page region
            stamp.XIndent = 50;   // distance from the left edge
            stamp.YIndent = 400;  // distance from the bottom edge

            // Optional visual settings
            stamp.Background = false;   // stamp appears on top
            stamp.Opacity   = 0.8f;     // semi‑transparent

            // Apply the stamp to the target page (Page.AddStamp method)
            targetPage.AddStamp(stamp);

            // Save the modified PDF (document-disposal-with-using rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}