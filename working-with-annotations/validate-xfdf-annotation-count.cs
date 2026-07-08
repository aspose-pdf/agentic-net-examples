using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the PDF and the XFDF file containing annotations
        const string pdfPath   = "input.pdf";
        const string xfdfPath  = "annotations.xfdf";

        // Expected annotation counts per page (1‑based page index)
        // Example: page 1 -> 2 annotations, page 2 -> 0, page 3 -> 5, etc.
        var expectedCounts = new Dictionary<int, int>
        {
            { 1, 2 },
            { 2, 0 },
            { 3, 5 }
        };

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF not found: {xfdfPath}");
            return;
        }

        // Load the PDF document and import annotations from the XFDF file
        using (Document doc = new Document(pdfPath))
        {
            // ImportAnnotationsFromXfdf(string) is the official API for this operation
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Validate annotation counts page by page
            bool allMatch = true;
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                int actualCount = doc.Pages[pageIndex].Annotations.Count;
                int expectedCount = 0;
                expectedCounts.TryGetValue(pageIndex, out expectedCount);

                if (actualCount != expectedCount)
                {
                    allMatch = false;
                    Console.WriteLine($"Page {pageIndex}: expected {expectedCount}, found {actualCount} (mismatch)");
                }
                else
                {
                    Console.WriteLine($"Page {pageIndex}: annotation count matches ({actualCount})");
                }
            }

            if (allMatch)
                Console.WriteLine("All pages have the expected number of annotations.");
            else
                Console.WriteLine("Some pages do not match the expected annotation counts.");
        }
    }
}