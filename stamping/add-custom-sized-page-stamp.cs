using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API (Document, Page, PdfPageStamp, etc.)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // Create a PdfPageStamp using the first page of the same document.
            // The stamp will be placed on the second page (or any target page).
            // -----------------------------------------------------------------
            PdfPageStamp stamp = new PdfPageStamp(doc.Pages[1]);

            // Set custom size of the stamp (in points; 1 point = 1/72 inch)
            stamp.Width  = 200;   // desired width
            stamp.Height = 100;   // desired height

            // Position the stamp within the target page.
            // XIndent/YIndent are measured from the lower‑left corner of the page.
            stamp.XIndent = 50;   // distance from the left edge
            stamp.YIndent = 400;  // distance from the bottom edge

            // Optional: make the stamp appear behind the page content
            stamp.Background = false; // true = background, false = foreground

            // -----------------------------------------------------------------
            // Apply the stamp to a specific page (e.g., page 2).
            // Page collection uses 1‑based indexing.
            // -----------------------------------------------------------------
            if (doc.Pages.Count >= 2)
            {
                Page targetPage = doc.Pages[2];
                targetPage.AddStamp(stamp);
            }
            else
            {
                Console.Error.WriteLine("The document does not contain a second page to stamp.");
            }

            // Save the modified PDF. No SaveOptions needed for PDF output.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}