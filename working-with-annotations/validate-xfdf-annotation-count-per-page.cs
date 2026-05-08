using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the XFDF file containing annotations
        const string pdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";

        // Define the expected number of annotations for each page (example values)
        var expectedCounts = new Dictionary<int, int>
        {
            { 1, 2 }, // Page 1 should have 2 annotations
            { 2, 0 }, // Page 2 should have none
            { 3, 5 }  // Page 3 should have 5 annotations
        };

        // Verify that the input files exist
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

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Import annotations from the XFDF file into the document
                doc.ImportAnnotationsFromXfdf(xfdfPath);

                // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Actual number of annotations on the current page
                    int actualCount = page.Annotations.Count;

                    // Expected count (default to 0 if not specified)
                    int expectedCount = expectedCounts.TryGetValue(pageIndex, out int val) ? val : 0;

                    // Compare and output the result
                    if (actualCount == expectedCount)
                    {
                        Console.WriteLine($"Page {pageIndex}: OK (annotations = {actualCount})");
                    }
                    else
                    {
                        Console.WriteLine($"Page {pageIndex}: MISMATCH (expected {expectedCount}, found {actualCount})");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}