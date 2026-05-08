using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Input PDF file paths
        const string pdfPath1 = "input1.pdf";
        const string pdfPath2 = "input2.pdf";

        // Verify that both files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Define the page range (1‑based indexing)
        const int startPage = 2;
        const int endPage   = 5;

        // Load the two PDF documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Ensure the requested range is within the bounds of both documents
            int maxPage = Math.Min(Math.Min(doc1.Pages.Count, doc2.Pages.Count), endPage);
            if (startPage > maxPage)
            {
                Console.Error.WriteLine("The specified start page is beyond the number of pages in the documents.");
                return;
            }

            // Prepare comparison options (no page range properties exist on ComparisonOptions)
            ComparisonOptions options = new ComparisonOptions();

            // Collect differences for each page in the range
            List<List<DiffOperation>> differences = new List<List<DiffOperation>>();
            for (int pageNumber = startPage; pageNumber <= maxPage; pageNumber++)
            {
                // Compare the corresponding pages from both documents
                List<DiffOperation> pageDiffs = TextPdfComparer.ComparePages(doc1.Pages[pageNumber], doc2.Pages[pageNumber], options);
                differences.Add(pageDiffs);
            }

            // Simple console output: number of differences per compared page
            for (int i = 0; i < differences.Count; i++)
            {
                int pageIdx = startPage + i; // actual page number
                Console.WriteLine($"Page {pageIdx}: {differences[i].Count} change(s) detected.");
            }
        }
    }
}
