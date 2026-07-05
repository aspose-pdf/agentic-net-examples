using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string diffPdfPath   = "diff.pdf";

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

        // Load the two PDF documents
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Default comparison options
            ComparisonOptions options = new ComparisonOptions();

            // Perform page‑by‑page comparison and save the diff PDF
            List<List<DiffOperation>> diffs = TextPdfComparer.CompareDocumentsPageByPage(
                doc1, doc2, options, diffPdfPath);

            // Optional: report the number of pages compared and changes found
            Console.WriteLine($"Compared {doc1.Pages.Count} pages with {doc2.Pages.Count} pages.");
            Console.WriteLine($"Differences saved to: {diffPdfPath}");
            Console.WriteLine($"Total pages with changes: {diffs.Count}");
        }
    }
}