using System;
using System.IO;
using Aspose.Pdf;   // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath        = "input.pdf";          // PDF to be stamped
        const string stampSourcePath  = "stampSource.pdf";    // PDF containing the page used as stamp
        const string outputPath       = "output.pdf";

        // Verify source files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(stampSourcePath))
        {
            Console.Error.WriteLine($"Stamp source PDF not found: {stampSourcePath}");
            return;
        }

        // Load the target document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageStamp from page 1 of the stamp source PDF
            // Constructor: PdfPageStamp(string fileName, int pageIndex)
            PdfPageStamp stamp = new PdfPageStamp(stampSourcePath, 1);

            // Set custom dimensions for the stamp (points)
            stamp.Width  = 200;   // Desired width
            stamp.Height = 100;   // Desired height

            // Position the stamp on the target page
            // XIndent/YIndent are measured from the left/bottom edges of the page
            stamp.XIndent = 50;    // 50 points from the left margin
            stamp.YIndent = 700;   // 700 points from the bottom margin

            // Ensure the stamp is drawn on top of existing content
            stamp.Background = false;

            // Apply the stamp to a specific page (e.g., page 2)
            // Aspose.Pdf uses 1‑based page indexing
            if (doc.Pages.Count >= 2)
            {
                doc.Pages[2].AddStamp(stamp);
            }
            else
            {
                Console.Error.WriteLine("Target document does not contain page 2.");
            }

            // Save the modified PDF (Document.Save writes PDF regardless of extension)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}