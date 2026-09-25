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
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string outputPath = "comparison_result.pdf";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input files are missing.");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal.
        using (Document firstDoc = new Document(firstPdfPath))
        using (Document secondDoc = new Document(secondPdfPath))
        {
            // Prepare comparison options.
            SideBySideComparisonOptions options = new SideBySideComparisonOptions();

            // Collect annotation bounding rectangles from the first document.
            List<Rectangle> firstRects = new List<Rectangle>();
            foreach (Page page in firstDoc.Pages)
            {
                foreach (Annotation annotation in page.Annotations)
                {
                    // The Rect property returns an Aspose.Pdf.Rectangle representing the annotation bounds.
                    firstRects.Add(annotation.Rect);
                }
            }

            // Collect annotation bounding rectangles from the second document.
            List<Rectangle> secondRects = new List<Rectangle>();
            foreach (Page page in secondDoc.Pages)
            {
                foreach (Annotation annotation in page.Annotations)
                {
                    secondRects.Add(annotation.Rect);
                }
            }

            // Assign the excluded areas to the comparison options.
            options.ExcludeAreas1 = firstRects.ToArray();
            options.ExcludeAreas2 = secondRects.ToArray();

            // Perform side‑by‑side comparison, applying the excluded areas.
            // SideBySidePdfComparer is a static class; use its static Compare method.
            SideBySidePdfComparer.Compare(firstDoc, secondDoc, outputPath, options);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{outputPath}'.");
    }
}
