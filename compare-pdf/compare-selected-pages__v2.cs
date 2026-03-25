using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const int startPage = 2; // inclusive, 1‑based index
        const int endPage = 4;   // inclusive

        if (!System.IO.File.Exists(firstPdfPath) || !System.IO.File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Validate page range
            int maxPage = Math.Min(doc1.Pages.Count, doc2.Pages.Count);
            if (startPage < 1 || endPage > maxPage || startPage > endPage)
            {
                Console.Error.WriteLine("Invalid page range specified.");
                return;
            }

            // Comparison options – default configuration
            ComparisonOptions options = new ComparisonOptions();

            // Compare each page in the selected range
            for (int i = startPage; i <= endPage; i++)
            {
                Page page1 = doc1.Pages[i];
                Page page2 = doc2.Pages[i];

                List<DiffOperation> diffs = TextPdfComparer.ComparePages(page1, page2, options);
                Console.WriteLine($"Page {i}: {diffs.Count} difference(s) found.");
                foreach (var diff in diffs)
                {
                    Console.WriteLine($"  {diff}");
                }
            }
        }
    }
}
