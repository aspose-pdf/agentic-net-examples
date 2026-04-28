using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";

        if (!System.IO.File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"File not found: {firstPdfPath}");
            return;
        }

        if (!System.IO.File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        // Load the two documents inside using blocks to ensure proper disposal.
        using (Document firstDoc = new Document(firstPdfPath))
        using (Document secondDoc = new Document(secondPdfPath))
        {
            // ComparisonOptions can be customized if needed (e.g., exclude tables, set areas, etc.).
            ComparisonOptions options = new ComparisonOptions();

            // Perform a page‑by‑page text comparison. The method returns a list of diff
            // operations for each page.
            List<List<DiffOperation>> diffsByPage = TextPdfComparer.CompareDocumentsPageByPage(
                firstDoc, secondDoc, options);

            // Iterate through the pages and output the diff operations.
            for (int pageIndex = 0; pageIndex < diffsByPage.Count; pageIndex++)
            {
                List<DiffOperation> pageDiffs = diffsByPage[pageIndex];
                Console.WriteLine($"--- Differences on page {pageIndex + 1} ---");

                foreach (DiffOperation diff in pageDiffs)
                {
                    // The DiffOperation class provides an Operation enum (e.g., Insert, Delete,
                    // FontChanged, etc.) and the affected text. Adjust the filter below if you
                    // want to isolate only font‑related changes.
                    Console.WriteLine($"{diff.Operation}: \"{diff.Text}\"");
                }

                Console.WriteLine();
            }
        }
    }
}