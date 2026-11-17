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
        const string pdfPath = "source.pdf";
        const string xfdfPath = "annotations.xfdf";

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

        // Expected annotation count per page (1‑based page index)
        var expectedCounts = new Dictionary<int, int>
        {
            { 1, 2 }, // Page 1 should have 2 annotations
            { 2, 3 }, // Page 2 should have 3 annotations
            // add more entries as needed
        };

        // Load the original PDF
        using (Document pdfDoc = new Document(pdfPath))
        // Load the XFDF file as a separate Document (contains only annotations)
        // NOTE: Aspose.Pdf.Xfdf namespace is part of a separate assembly. To avoid the extra dependency we load the XFDF file using the default Document constructor.
        using (Document xfdfDoc = new Document(xfdfPath))
        {
            // Import annotations from the XFDF document into the PDF document
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                // Ensure the XFDF document has a corresponding page
                if (pageNum <= xfdfDoc.Pages.Count)
                {
                    Page pdfPage = pdfDoc.Pages[pageNum];
                    Page xfdfPage = xfdfDoc.Pages[pageNum];

                    // Transfer each annotation from XFDF to the PDF page
                    foreach (Annotation ann in xfdfPage.Annotations)
                    {
                        // Clone the annotation to avoid reference issues
                        Annotation cloned = (Annotation)ann.Clone();
                        pdfPage.Annotations.Add(cloned);
                    }
                }
            }

            // Validate annotation counts per page against the expected values
            bool allMatch = true;
            foreach (var kvp in expectedCounts)
            {
                int pageIndex = kvp.Key; // 1‑based page number
                int expected = kvp.Value;

                // Guard against out‑of‑range page numbers
                if (pageIndex < 1 || pageIndex > pdfDoc.Pages.Count)
                {
                    Console.WriteLine($"Page {pageIndex} does not exist in the PDF.");
                    allMatch = false;
                    continue;
                }

                int actual = pdfDoc.Pages[pageIndex].Annotations.Count;
                if (actual != expected)
                {
                    Console.WriteLine($"Page {pageIndex}: expected {expected} annotations, found {actual}.");
                    allMatch = false;
                }
                else
                {
                    Console.WriteLine($"Page {pageIndex}: annotation count matches expected ({expected}).");
                }
            }

            if (allMatch)
            {
                Console.WriteLine("All annotation counts match the expected values.");
            }
            else
            {
                Console.WriteLine("Some annotation counts do not match the expected values.");
            }

            // Optionally save the PDF with imported annotations
            const string outputPath = "output_with_annotations.pdf";
            pdfDoc.Save(outputPath);
            Console.WriteLine($"PDF with imported annotations saved to '{outputPath}'.");
        }
    }
}
