using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string xfdfFile  = "annotations.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPdf))
        {
            // Count total annotations before export
            int originalCount = doc.Pages.Cast<Page>()
                                         .Sum(p => p.Annotations.Count);
            Console.WriteLine($"Original annotation count: {originalCount}");

            // Export all annotations to XFDF file
            doc.ExportAnnotationsToXfdf(xfdfFile);
            Console.WriteLine($"Annotations exported to: {xfdfFile}");

            // Remove all existing annotations from the document
            foreach (Page page in doc.Pages)
            {
                // Annotations collection uses 1‑based indexing
                while (page.Annotations.Count > 0)
                {
                    page.Annotations.Delete(1);
                }
            }

            // Verify that annotations have been removed
            int clearedCount = doc.Pages.Cast<Page>()
                                        .Sum(p => p.Annotations.Count);
            Console.WriteLine($"Annotations after clearing: {clearedCount}");

            // Import annotations back from the XFDF file
            doc.ImportAnnotationsFromXfdf(xfdfFile);
            Console.WriteLine("Annotations re‑imported from XFDF.");

            // Count annotations after import
            int roundTripCount = doc.Pages.Cast<Page>()
                                          .Sum(p => p.Annotations.Count);
            Console.WriteLine($"Annotation count after round‑trip: {roundTripCount}");

            // Simple integrity check
            if (originalCount == roundTripCount)
                Console.WriteLine("Round‑trip verification succeeded: counts match.");
            else
                Console.WriteLine("Round‑trip verification failed: counts differ.");

            // Save the resulting PDF (optional)
            const string outputPdf = "output_roundtrip.pdf";
            doc.Save(outputPdf);
            Console.WriteLine($"Resulting PDF saved to: {outputPdf}");
        }
    }
}