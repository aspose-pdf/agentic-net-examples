using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Facades; // Facades namespace is included as requested

class Program
{
    static void Main()
    {
        // Input PDF files
        const string doc1Path = "doc1.pdf";
        const string doc2Path = "doc2.pdf";

        // Output PDF that will contain the visual comparison result
        const string resultPath = "comparisonResult.pdf";

        // Verify that both source files exist
        if (!File.Exists(doc1Path) || !File.Exists(doc2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Define the pages to compare (1‑based indexing)
        int[] pagesToCompare = new int[] { 1, 2, 3 };

        // Comparison options – default settings are sufficient for a basic text comparison
        ComparisonOptions options = new ComparisonOptions();

        // This list will hold the diff operations for each compared page
        var allPageDiffs = new System.Collections.Generic.List<System.Collections.Generic.List<DiffOperation>>();

        // Load both documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(doc1Path))
        using (Document doc2 = new Document(doc2Path))
        {
            foreach (int pageNumber in pagesToCompare)
            {
                // Ensure the requested page exists in both documents
                if (pageNumber > doc1.Pages.Count || pageNumber > doc2.Pages.Count)
                {
                    Console.WriteLine($"Page {pageNumber} is out of range for one of the documents – skipping.");
                    continue;
                }

                // Retrieve the pages (1‑based)
                Page page1 = doc1.Pages[pageNumber];
                Page page2 = doc2.Pages[pageNumber];

                // Perform the comparison for the current pair of pages
                var diffs = TextPdfComparer.ComparePages(page1, page2, options);
                allPageDiffs.Add(diffs);
            }

            // If we have any differences, generate a PDF that visualises them
            if (allPageDiffs.Count > 0)
            {
                // PdfOutputGenerator creates a PDF that highlights the changes
                var pdfGenerator = new PdfOutputGenerator();
                pdfGenerator.GenerateOutput(allPageDiffs, resultPath);
                Console.WriteLine($"Comparison result saved to '{resultPath}'.");
            }
            else
            {
                Console.WriteLine("No differences were found on the specified pages.");
            }
        }
    }
}