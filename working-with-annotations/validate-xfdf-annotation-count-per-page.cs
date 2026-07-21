using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the XFDF file containing annotations.
        const string pdfPath   = "source.pdf";
        const string xfdfPath  = "annotations.xfdf";

        // Expected annotation count per page (1‑based page index).
        // Adjust the values according to your test case.
        var expectedCounts = new Dictionary<int, int>
        {
            { 1, 3 },   // Page 1 should have 3 annotations after import
            { 2, 1 },   // Page 2 should have 1 annotation after import
            // Add more entries as needed
        };

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(pdfPath))
        {
            // Import annotations from the XFDF file into the document.
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Validate annotation counts page by page.
            foreach (var kvp in expectedCounts)
            {
                int pageNumber = kvp.Key;
                int expected   = kvp.Value;

                // Ensure the requested page exists (Aspose.Pdf uses 1‑based indexing).
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                {
                    Console.Error.WriteLine($"Page {pageNumber} is out of range (1‑{doc.Pages.Count}).");
                    continue;
                }

                Page page = doc.Pages[pageNumber];
                AnnotationCollection annotations = page.Annotations;
                int actualCount = annotations.Count;

                if (actualCount == expected)
                {
                    Console.WriteLine($"Page {pageNumber}: annotation count matches expected ({expected}).");
                }
                else
                {
                    Console.WriteLine($"Page {pageNumber}: expected {expected} annotations, but found {actualCount}.");
                }
            }

            // Optionally, save the document with imported annotations.
            // The save call is placed inside the using block so the document remains alive.
            doc.Save("output_with_annotations.pdf");
        }
    }
}