using System;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";
        const int startPage = 1; // first page to compare (1‑based)
        const int endPage   = 3; // last page to compare (inclusive)

        if (!System.IO.File.Exists(pdfPath1))
        {
            Console.Error.WriteLine($"File not found: {pdfPath1}");
            return;
        }
        if (!System.IO.File.Exists(pdfPath2))
        {
            Console.Error.WriteLine($"File not found: {pdfPath2}");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Prepare comparison options (no page‑range properties exist on this class)
            ComparisonOptions options = new ComparisonOptions();

            int maxPage = Math.Min(Math.Min(doc1.Pages.Count, doc2.Pages.Count), endPage);
            for (int pageNum = startPage; pageNum <= maxPage; pageNum++)
            {
                // Compare corresponding pages
                var diffs = TextPdfComparer.ComparePages(doc1.Pages[pageNum], doc2.Pages[pageNum], options);
                Console.WriteLine($"Differences on page {pageNum}:");
                if (diffs.Count == 0)
                {
                    Console.WriteLine("  No differences found.");
                }
                else
                {
                    foreach (var diff in diffs)
                    {
                        // DiffOperation provides a useful ToString implementation
                        Console.WriteLine($"  {diff}");
                    }
                }
            }
        }
    }
}