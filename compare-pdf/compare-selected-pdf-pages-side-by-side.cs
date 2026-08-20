using System;
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

        // Output PDF that will contain the side‑by‑side comparison result
        const string resultPdfPath = "comparison_result.pdf";

        // Verify that both source files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two documents
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // -----------------------------------------------------------------
            // Define the pages you want to compare.
            // Aspose.Pdf uses 1‑based indexing for pages.
            // -----------------------------------------------------------------
            int[] pagesToCompare = new int[] { 1, 3, 5 }; // example page numbers

            // -----------------------------------------------------------------
            // Prepare comparison options (default settings are fine for a basic run)
            // -----------------------------------------------------------------
            ComparisonOptions textOptions = new ComparisonOptions();

            // -----------------------------------------------------------------
            // Create temporary documents that contain only the selected pages.
            // This allows the SideBySidePdfComparer to work on the exact subset.
            // -----------------------------------------------------------------
            Document tempDoc1 = new Document();
            Document tempDoc2 = new Document();

            foreach (int pageNumber in pagesToCompare)
            {
                // Ensure the requested page exists in both documents
                if (pageNumber <= doc1.Pages.Count && pageNumber <= doc2.Pages.Count)
                {
                    // Add the page from each source document to the temporary document
                    tempDoc1.Pages.Add(doc1.Pages[pageNumber]);
                    tempDoc2.Pages.Add(doc2.Pages[pageNumber]);
                }
                else
                {
                    Console.WriteLine($"Page {pageNumber} is out of range and will be skipped.");
                }
            }

            // -----------------------------------------------------------------
            // Perform a visual side‑by‑side comparison and save the result.
            // -----------------------------------------------------------------
            SideBySideComparisonOptions sbOptions = new SideBySideComparisonOptions();
            SideBySidePdfComparer.Compare(tempDoc1, tempDoc2, resultPdfPath, sbOptions);

            // -----------------------------------------------------------------
            // (Optional) Retrieve textual differences for each compared page.
            // -----------------------------------------------------------------
            for (int i = 0; i < pagesToCompare.Length; i++)
            {
                int pageNum = pagesToCompare[i];
                if (pageNum <= doc1.Pages.Count && pageNum <= doc2.Pages.Count)
                {
                    var diffs = TextPdfComparer.ComparePages(doc1.Pages[pageNum], doc2.Pages[pageNum], textOptions);
                    Console.WriteLine($"Page {pageNum}: {diffs.Count} textual differences found.");
                }
            }

            // Clean up the temporary documents
            tempDoc1.Dispose();
            tempDoc2.Dispose();
        }

        Console.WriteLine($"Side‑by‑side comparison PDF saved to '{resultPdfPath}'.");
    }
}