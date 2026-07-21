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
        const string pdfPath1 = "input1.pdf";
        const string pdfPath2 = "input2.pdf";

        // Define the page range to compare (1‑based indexing)
        int startPage = 2;
        int endPage   = 4;

        // Verify that both files exist
        if (!File.Exists(pdfPath1))
        {
            Console.Error.WriteLine($"File not found: {pdfPath1}");
            return;
        }
        if (!File.Exists(pdfPath2))
        {
            Console.Error.WriteLine($"File not found: {pdfPath2}");
            return;
        }

        // Load the documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Adjust the page range to the actual number of pages in both documents
            int maxPage = Math.Min(doc1.Pages.Count, doc2.Pages.Count);
            if (endPage > maxPage) endPage = maxPage;
            if (startPage < 1) startPage = 1;
            if (startPage > endPage)
            {
                Console.WriteLine("Invalid page range after adjustment.");
                return;
            }

            // Comparison options – use defaults (no StartPage/EndPage properties exist)
            ComparisonOptions options = new ComparisonOptions();

            // Compare each page in the selected range
            for (int pageNumber = startPage; pageNumber <= endPage; pageNumber++)
            {
                Page page1 = doc1.Pages[pageNumber];
                Page page2 = doc2.Pages[pageNumber];

                // Perform text‑based comparison for the two pages
                List<DiffOperation> diffs = TextPdfComparer.ComparePages(page1, page2, options);

                // Output simple statistics
                Console.WriteLine($"Page {pageNumber}: {diffs.Count} difference(s) found.");
            }
        }
    }
}