using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string doc1Path = "doc1.pdf";
        const string doc2Path = "doc2.pdf";
        const string wholeResultPath = "whole_comparison.pdf";
        const string pageResultPath = "page_comparison.pdf";

        if (!File.Exists(doc1Path) || !File.Exists(doc2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Compare the entire documents and save the side‑by‑side result.
        CompareWholeDocuments(doc1Path, doc2Path, wholeResultPath);

        // Example: compare specific pages (page 2 of each document) and save the result.
        CompareSpecificPages(doc1Path, 2, doc2Path, 2, pageResultPath);
    }

    static void CompareWholeDocuments(string path1, string path2, string resultPath)
    {
        // Load both PDFs inside using blocks for deterministic disposal.
        using (Document doc1 = new Document(path1))
        using (Document doc2 = new Document(path2))
        {
            // Default options; can be customized as needed.
            SideBySideComparisonOptions options = new SideBySideComparisonOptions();

            // Perform side‑by‑side comparison; the result is written to resultPath.
            SideBySidePdfComparer.Compare(doc1, doc2, resultPath, options);
        }

        Console.WriteLine($"Whole‑document comparison saved to '{resultPath}'.");
    }

    static void CompareSpecificPages(string path1, int pageIndex1, string path2, int pageIndex2, string resultPath)
    {
        // Aspose.Pdf uses 1‑based page indexing; validate the supplied indices.
        using (Document doc1 = new Document(path1))
        using (Document doc2 = new Document(path2))
        {
            if (pageIndex1 < 1 || pageIndex1 > doc1.Pages.Count ||
                pageIndex2 < 1 || pageIndex2 > doc2.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page numbers supplied for comparison.");
                return;
            }

            Page page1 = doc1.Pages[pageIndex1];
            Page page2 = doc2.Pages[pageIndex2];

            SideBySideComparisonOptions options = new SideBySideComparisonOptions();

            // Compare the two pages; the result PDF contains page1 on the left and page2 on the right.
            SideBySidePdfComparer.Compare(page1, page2, resultPath, options);
        }

        Console.WriteLine($"Page‑by‑page comparison saved to '{resultPath}'.");
    }
}