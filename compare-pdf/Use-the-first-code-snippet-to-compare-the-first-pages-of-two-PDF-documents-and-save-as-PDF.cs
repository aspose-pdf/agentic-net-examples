using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison; // Provides GraphicalPdfComparer

class Program
{
    static void Main()
    {
        // Input PDF file paths
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        // Output PDF path for the comparison result
        const string resultPdfPath = "comparison_result.pdf";

        // Verify that input files exist
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"File not found: {firstPdfPath}");
            return;
        }
        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing; get the first page of each document
            Page page1 = doc1.Pages[1];
            Page page2 = doc2.Pages[1];

            // Create the comparer and compare the two pages, saving the visual diff to a PDF
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();
            comparer.ComparePagesToPdf(page1, page2, resultPdfPath);
        }

        Console.WriteLine($"Comparison PDF saved to '{resultPdfPath}'.");
    }
}