using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Input PDF files
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string resultPath    = "result.pdf"; // file that will contain visual diff (not used in this example)

        // Ensure files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input files are missing.");
            return;
        }

        // Define the pages to compare (1‑based indexing)
        int[] pagesToCompare = new int[] { 1, 3, 5 };

        // Prepare comparison options (customize as needed)
        ComparisonOptions options = new ComparisonOptions();
        // Text comparison is enabled by default; no need to set EnableTextComparison

        // Load both documents inside using blocks for proper disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Verify that the requested pages exist in both documents
            int maxPage = Math.Min(doc1.Pages.Count, doc2.Pages.Count);
            var validPages = new List<int>();
            foreach (int pageNum in pagesToCompare)
            {
                if (pageNum >= 1 && pageNum <= maxPage)
                    validPages.Add(pageNum);
                else
                    Console.Error.WriteLine($"Page number {pageNum} is out of range and will be ignored.");
            }

            if (validPages.Count == 0)
            {
                Console.WriteLine("No valid pages to compare.");
                return;
            }

            // Perform comparison for the selected pages using TextPdfComparer (DocumentComparer is not available in this version)
            var allDiffs = new List<DiffOperation>();
            foreach (int pageNum in validPages)
            {
                Page page1 = doc1.Pages[pageNum];
                Page page2 = doc2.Pages[pageNum];

                // Compare the two pages; this returns a list of differences for the current page
                List<DiffOperation> pageDiffs = TextPdfComparer.ComparePages(page1, page2, options);
                if (pageDiffs != null && pageDiffs.Count > 0)
                {
                    // Optionally, you could annotate the result PDF here. For simplicity we just collect the diffs.
                    allDiffs.AddRange(pageDiffs);
                }
            }

            // Output a simple summary of differences
            Console.WriteLine($"Comparison completed. Total differences detected: {allDiffs?.Count ?? 0}");
            if (allDiffs != null)
            {
                foreach (DiffOperation diff in allDiffs)
                {
                    // Use ToString() or the available properties (Operation, Text)
                    Console.WriteLine(diff.ToString());
                }
            }
        }
    }
}
