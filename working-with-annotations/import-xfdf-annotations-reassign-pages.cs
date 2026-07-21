using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";   // source PDF
        const string xfdfPath  = "annotations.xfdf"; // XFDF file containing annotations
        const string outputPdf = "output.pdf";

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

        // Mapping of original page numbers (as stored in XFDF) to target page numbers in the PDF.
        // Example: annotations originally on page 1 should be moved to page 2, etc.
        var pageMapping = new Dictionary<int, int>
        {
            { 1, 2 },
            { 2, 3 },
            // add more mappings as required
        };

        try
        {
            // Load the PDF document.
            using (Document doc = new Document(pdfPath))
            {
                // Import all annotations from the XFDF file into the document.
                // This method adds the annotations to the pages indicated inside the XFDF.
                doc.ImportAnnotationsFromXfdf(xfdfPath);

                // Re‑assign annotations according to the mapping dictionary.
                foreach (var kvp in pageMapping)
                {
                    int sourcePageNumber = kvp.Key;
                    int targetPageNumber = kvp.Value;

                    // Validate page numbers.
                    if (sourcePageNumber < 1 || sourcePageNumber > doc.Pages.Count ||
                        targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
                    {
                        Console.WriteLine($"Skipping invalid mapping: {sourcePageNumber} -> {targetPageNumber}");
                        continue;
                    }

                    Page sourcePage = doc.Pages[sourcePageNumber];
                    Page targetPage = doc.Pages[targetPageNumber];

                    // Move each annotation from the source page to the target page.
                    // AnnotationCollection uses 1‑based indexing, so iterate backwards when deleting.
                    for (int i = sourcePage.Annotations.Count; i >= 1; i--)
                    {
                        var annotation = sourcePage.Annotations[i];
                        // Add to target page (preserves rectangle and other properties).
                        targetPage.Annotations.Add(annotation);
                        // Remove from source page.
                        sourcePage.Annotations.Delete(i);
                    }
                }

                // Save the modified PDF.
                doc.Save(outputPdf);
                Console.WriteLine($"Annotations imported and reassigned. Saved to '{outputPdf}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}