using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdf = "first.pdf";
        const string secondPdf = "second.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Define the page range to compare (inclusive)
        const int startPage = 2;
        const int endPage   = 4;

        bool rangeIsEqual = true;
        var allDifferences = new List<DiffOperation>();

        // Load the documents
        using (var doc1 = new Document(firstPdf))
        using (var doc2 = new Document(secondPdf))
        {
            // Ensure we do not exceed the smallest page count
            int maxPage = Math.Min(Math.Min(doc1.Pages.Count, doc2.Pages.Count), endPage);

            // Iterate over the selected page range and compare each page individually
            for (int p = startPage; p <= maxPage; p++)
            {
                IList<DiffOperation> diffs = TextPdfComparer.ComparePages(
                    doc1.Pages[p],
                    doc2.Pages[p],
                    new ComparisonOptions()); // default options – no OutputPath, no StartPage/EndPage

                if (diffs != null && diffs.Count > 0)
                {
                    rangeIsEqual = false;
                    allDifferences.AddRange(diffs);
                }
            }
        }

        // Output the result
        Console.WriteLine($"Compared pages {startPage} to {endPage}.");
        Console.WriteLine($"Documents are equal in the selected range: {rangeIsEqual}");
        if (!rangeIsEqual)
        {
            Console.WriteLine($"Total differences found: {allDifferences.Count}");
        }
    }
}
