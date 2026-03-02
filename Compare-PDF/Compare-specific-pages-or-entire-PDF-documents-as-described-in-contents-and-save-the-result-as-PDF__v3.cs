using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the source PDF files
        const string firstPdfPath  = "doc1.pdf";
        const string secondPdfPath = "doc2.pdf";

        // Result PDF for whole‑document comparison
        const string wholeResultPath = "whole_comparison.pdf";

        // Result PDF for specific‑page comparison
        const string pageResultPath = "page_comparison.pdf";

        // Compare the entire documents and save the diff PDF
        CompareWholeDocuments(firstPdfPath, secondPdfPath, wholeResultPath);

        // Compare page 1 of each document and save the side‑by‑side diff PDF
        CompareSpecificPages(firstPdfPath, secondPdfPath, 1, 1, pageResultPath);
    }

    // Compares two complete PDF documents and writes the result to a PDF file.
    static void CompareWholeDocuments(string docPath1, string docPath2, string resultPdfPath)
    {
        if (!File.Exists(docPath1) || !File.Exists(docPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal.
        using (Document doc1 = new Document(docPath1))
        using (Document doc2 = new Document(docPath2))
        {
            // Create default comparison options (customize if needed).
            ComparisonOptions options = new ComparisonOptions();

            // This overload saves the comparison result directly to the specified PDF file.
            TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPdfPath);
        }

        Console.WriteLine($"Whole‑document comparison saved to '{resultPdfPath}'.");
    }

    // Compares two individual pages (specified by 1‑based indices) and writes the result to a PDF file.
    static void CompareSpecificPages(string docPath1, string docPath2,
                                     int pageIndex1, int pageIndex2,
                                     string resultPdfPath)
    {
        if (!File.Exists(docPath1) || !File.Exists(docPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        using (Document doc1 = new Document(docPath1))
        using (Document doc2 = new Document(docPath2))
        {
            // Validate page numbers (Aspose.Pdf uses 1‑based indexing).
            if (pageIndex1 < 1 || pageIndex1 > doc1.Pages.Count ||
                pageIndex2 < 1 || pageIndex2 > doc2.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page numbers supplied.");
                return;
            }

            Page page1 = doc1.Pages[pageIndex1];
            Page page2 = doc2.Pages[pageIndex2];

            // Default side‑by‑side options (customize if needed).
            SideBySideComparisonOptions options = new SideBySideComparisonOptions();

            // This method saves a side‑by‑side PDF showing differences between the two pages.
            SideBySidePdfComparer.Compare(page1, page2, resultPdfPath, options);
        }

        Console.WriteLine($"Page {pageIndex1} vs page {pageIndex2} comparison saved to '{resultPdfPath}'.");
    }
}