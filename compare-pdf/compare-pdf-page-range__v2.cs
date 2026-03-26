using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPath = "first.pdf";
        const string secondPath = "second.pdf";
        const string jsonReportPath = "diffReport.json"; // JSON diff report for the selected range

        if (!File.Exists(firstPath))
        {
            Console.Error.WriteLine($"File not found: {firstPath}");
            return;
        }
        if (!File.Exists(secondPath))
        {
            Console.Error.WriteLine($"File not found: {secondPath}");
            return;
        }

        // Load the two documents
        using (Document doc1 = new Document(firstPath))
        using (Document doc2 = new Document(secondPath))
        {
            // Desired page range (1‑based inclusive)
            const int startPage = 2;
            const int endPage   = 4;

            // Ensure the range does not exceed the smallest document page count
            int maxPage = Math.Min(Math.Min(doc1.Pages.Count, doc2.Pages.Count), endPage);
            if (startPage > maxPage)
            {
                Console.WriteLine("The specified start page is beyond the number of pages in the documents.");
                return;
            }

            // ComparisonOptions does not expose StartPage/EndPage – iterate manually.
            ComparisonOptions options = new ComparisonOptions();
            List<List<DiffOperation>> allDiffs = new List<List<DiffOperation>>();

            for (int pageNum = startPage; pageNum <= maxPage; pageNum++)
            {
                // Compare the corresponding pages from both documents.
                List<DiffOperation> pageDiffs = TextPdfComparer.ComparePages(doc1.Pages[pageNum], doc2.Pages[pageNum], options);
                allDiffs.Add(pageDiffs);
            }

            // Generate a JSON report (one file that contains diffs for all compared pages)
            JsonDiffOutputGenerator jsonGen = new JsonDiffOutputGenerator();
            jsonGen.GenerateOutput(allDiffs, jsonReportPath);

            // Simple console feedback
            for (int i = 0; i < allDiffs.Count; i++)
            {
                int pageNumber = startPage + i; // because the list starts at StartPage
                Console.WriteLine($"Page {pageNumber}: {allDiffs[i].Count} differences.");
            }

            Console.WriteLine($"Comparison completed for pages {startPage}-{maxPage}. JSON report saved to '{jsonReportPath}'.");
        }
    }
}
