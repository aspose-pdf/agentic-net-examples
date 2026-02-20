using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Entry point
    static void Main(string[] args)
    {
        // Input PDF file – replace with your actual file path
        const string sourcePdfPath = "input.pdf";

        // Verify that the source file exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Error: Source PDF not found at '{sourcePdfPath}'.");
            return;
        }

        // Define the page ranges you want to extract.
        // Each tuple contains (StartPage, EndPage) – both inclusive and 1‑based.
        var ranges = new List<(int Start, int End)>
        {
            (1, 3),   // pages 1‑3
            (4, 5)    // pages 4‑5
            // Add more ranges as needed
        };

        // Load the source PDF using a Facade (PdfFileInfo) to obtain the underlying Document.
        using (var pdfInfo = new PdfFileInfo(sourcePdfPath))
        {
            Document sourceDoc = pdfInfo.Document;

            // Validate that the source document has pages
            if (sourceDoc.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: Source PDF contains no pages.");
                return;
            }

            // Process each range
            int rangeIndex = 1;
            foreach (var (start, end) in ranges)
            {
                // Basic validation of the range
                if (start < 1 || end > sourceDoc.Pages.Count || start > end)
                {
                    Console.Error.WriteLine($"Warning: Invalid range [{start}-{end}] – skipped.");
                    continue;
                }

                // Create a new empty PDF document
                Document rangeDoc = new Document();

                // Copy pages from the source document into the new document.
                // Aspose.Pdf uses 1‑based indexing for pages.
                for (int pageNum = start; pageNum <= end; pageNum++)
                {
                    // Add a copy of the page to the new document.
                    // The Add method clones the page, preserving its content.
                    rangeDoc.Pages.Add(sourceDoc.Pages[pageNum]);
                }

                // Define output file name for this range
                string outputPath = $"output_range_{rangeIndex}.pdf";

                // Save the new document
                rangeDoc.Save(outputPath);
                Console.WriteLine($"Range [{start}-{end}] saved to '{outputPath}'.");

                // Clean up the temporary document
                rangeDoc.Dispose();

                rangeIndex++;
            }
        }
    }
}