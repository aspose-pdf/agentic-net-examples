using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdf = "first.pdf";
        const string secondPdf = "second.pdf";
        const string resultPdf = "comparison_result.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdf))
        using (Document doc2 = new Document(secondPdf))
        {
            // Define the pages to compare (Aspose.Pdf uses 1‑based indexing)
            int[] pagesToCompare = new int[] { 1, 2, 3 };

            // Set up comparison options – keep defaults (visual output is the default)
            ComparisonOptions options = new ComparisonOptions();

            // Compare all pages and obtain per‑page diff lists; the overload also writes the visual result PDF
            List<List<DiffOperation>> allPageDiffs = TextPdfComparer.CompareDocumentsPageByPage(
                doc1, doc2, options, resultPdf);

            // Select diffs for the requested pages (optional – you can further process them)
            var selectedDiffs = pagesToCompare
                .Where(p => p >= 1 && p <= allPageDiffs.Count)
                .Select(p => allPageDiffs[p - 1])
                .ToList();

            // Example output: number of differences per selected page
            for (int i = 0; i < selectedDiffs.Count; i++)
            {
                Console.WriteLine($"Page {pagesToCompare[i]}: {selectedDiffs[i].Count} differences.");
            }
        }

        Console.WriteLine($"Comparison result saved to '{resultPdf}'.");
    }
}