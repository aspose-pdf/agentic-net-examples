using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string resultPdfPath = "comparison_result.pdf";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input files are missing.");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Collect annotation rectangles from the first document
            List<Aspose.Pdf.Rectangle> excludeAreas1 = new List<Aspose.Pdf.Rectangle>();
            for (int pageIndex = 1; pageIndex <= doc1.Pages.Count; pageIndex++)
            {
                Page page = doc1.Pages[pageIndex];
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation annotation = page.Annotations[annIndex];
                    // Add the annotation's bounding rectangle to the exclusion list
                    excludeAreas1.Add(annotation.Rect);
                }
            }

            // Collect annotation rectangles from the second document
            List<Aspose.Pdf.Rectangle> excludeAreas2 = new List<Aspose.Pdf.Rectangle>();
            for (int pageIndex = 1; pageIndex <= doc2.Pages.Count; pageIndex++)
            {
                Page page = doc2.Pages[pageIndex];
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation annotation = page.Annotations[annIndex];
                    excludeAreas2.Add(annotation.Rect);
                }
            }

            // Configure comparison options to exclude the collected annotation areas
            SideBySideComparisonOptions options = new SideBySideComparisonOptions
            {
                ExcludeAreas1 = excludeAreas1.ToArray(),
                ExcludeAreas2 = excludeAreas2.ToArray()
                // Other options (e.g., ExcludeTables) can be set here if needed
            };

            // Perform side‑by‑side comparison; the result is saved to resultPdfPath
            SideBySidePdfComparer.Compare(doc1, doc2, resultPdfPath, options);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
    }
}