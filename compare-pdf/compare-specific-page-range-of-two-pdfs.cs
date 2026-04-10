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
        const string resultPath    = "comparison_result.txt";

        // Define the page range to compare (1‑based indexing)
        int startPage = 2; // inclusive
        int endPage   = 5; // inclusive

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input files are missing.");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Ensure the requested range does not exceed the page count of either document
            int maxPage = Math.Min(Math.Min(doc1.Pages.Count, doc2.Pages.Count), endPage);
            if (startPage < 1 || startPage > maxPage)
            {
                Console.Error.WriteLine("Invalid start page number.");
                return;
            }

            // Prepare comparison options (you can adjust properties if needed)
            ComparisonOptions options = new ComparisonOptions
            {
                // Example: ignore spaces during comparison
                // ComparisonMode = ComparisonMode.IgnoreSpaces
            };

            // Collect differences for each page in the range
            List<string> diffSummaries = new List<string>();

            for (int i = startPage; i <= maxPage; i++)
            {
                // Compare the corresponding pages
                List<DiffOperation> diffs = TextPdfComparer.ComparePages(doc1.Pages[i], doc2.Pages[i], options);

                // Summarize the result for this page
                diffSummaries.Add($"Page {i}: {diffs.Count} change(s) detected.");
                // Optionally, process each DiffOperation here
                // foreach (var diff in diffs) { /* inspect diff */ }
            }

            // Write a simple textual report
            File.WriteAllLines(resultPath, diffSummaries);
            Console.WriteLine($"Comparison completed. Report saved to '{resultPath}'.");
        }
    }
}