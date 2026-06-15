using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";

        // Expected annotation counts per page (1‑based indexing)
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

        // Load PDF document (using rule: document disposal with using)
        using (Document doc = new Document(pdfPath))
        {
            // Import annotations from XFDF file
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Validate annotation count on each page
            foreach (var kvp in expectedCounts)
            {
                int pageNumber = kvp.Key;
                int expected = kvp.Value;

                // Ensure the page exists (pages are 1‑based)
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                {
                    Console.WriteLine($"Page {pageNumber} does not exist in the document.");
                    continue;
                }

                Page page = doc.Pages[pageNumber];
                int actual = page.Annotations.Count;

                if (actual == expected)
                {
                    Console.WriteLine($"Page {pageNumber}: annotation count matches ({actual}).");
                }
                else
                {
                    Console.WriteLine($"Page {pageNumber}: expected {expected} annotations, found {actual}.");
                }
            }

            // Save the document after importing annotations (PDF format)
            doc.Save("output_with_annotations.pdf");
        }
    }
}