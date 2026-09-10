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
        const string pdfPath  = "source.pdf";
        const string xfdfPath = "annotations.xfdf";

        // Define the expected number of annotations for each page (1‑based indexing).
        // If a page is not present in the dictionary, the expected count is assumed to be 0.
        var expectedCounts = new Dictionary<int, int>
        {
            { 1, 2 }, // Page 1 should have 2 annotations
            { 2, 1 }, // Page 2 should have 1 annotation
            { 3, 0 }  // Page 3 should have no annotations
            // Add more entries as needed.
        };

        // Verify that the input files exist before proceeding.
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

        // Load the PDF document and import annotations from the XFDF file.
        using (Document doc = new Document(pdfPath))
        {
            // Import all annotations defined in the XFDF file into the document.
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Iterate through each page and compare the actual annotation count
            // with the expected count defined in the dictionary.
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                Page page = doc.Pages[pageNumber];
                int actualCount   = page.Annotations.Count;
                int expectedCount = expectedCounts.TryGetValue(pageNumber, out int val) ? val : 0;

                if (actualCount != expectedCount)
                {
                    Console.WriteLine($"Page {pageNumber}: MISMATCH – expected {expectedCount}, found {actualCount}.");
                }
                else
                {
                    Console.WriteLine($"Page {pageNumber}: OK – annotation count matches ({actualCount}).");
                }
            }
        }
    }
}