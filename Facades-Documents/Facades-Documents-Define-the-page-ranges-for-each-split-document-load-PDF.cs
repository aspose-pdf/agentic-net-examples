using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF path – adjust as needed
        const string inputPdfPath = "input.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF using the Facades API (PdfFileInfo) to obtain the Document object
            PdfFileInfo pdfInfo = new PdfFileInfo(inputPdfPath);
            Document sourceDoc = pdfInfo.Document;

            // Define the page ranges for each split document (1‑based inclusive)
            // Example: split pages 1‑3 into one file, pages 4‑5 into another, etc.
            var pageRanges = new List<(int start, int end)>
            {
                (1, 3),
                (4, 5),
                (6, sourceDoc.Pages.Count) // remaining pages
            };

            // Process each range
            int partIndex = 1;
            foreach (var range in pageRanges)
            {
                // Ensure the range is within the source document bounds
                int startPage = Math.Max(1, range.start);
                int endPage = Math.Min(sourceDoc.Pages.Count, range.end);
                if (startPage > endPage)
                {
                    Console.WriteLine($"Skipping invalid range {range.start}-{range.end}.");
                    continue;
                }

                // Create a new empty document
                Document splitDoc = new Document();

                // Remove the default empty page that Document constructor adds
                splitDoc.Pages.Clear();

                // Copy the specified pages from the source document
                for (int i = startPage; i <= endPage; i++)
                {
                    splitDoc.Pages.Add(sourceDoc.Pages[i]); // 1‑based indexing
                }

                // Build an output file name that reflects the page range
                string outputPath = $"output_part{partIndex}_pages{startPage}_to_{endPage}.pdf";

                // Save the split document (uses the provided document-save rule)
                splitDoc.Save(outputPath);

                Console.WriteLine($"Saved pages {startPage}-{endPage} to '{outputPath}'.");
                partIndex++;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}