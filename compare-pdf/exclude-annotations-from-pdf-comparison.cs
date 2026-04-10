using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Input PDF files
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        // Output comparison result
        const string resultPdfPath = "comparison_result.pdf";

        // Load both documents inside using blocks for deterministic disposal
        using (Document firstDoc  = new Document(firstPdfPath))
        using (Document secondDoc = new Document(secondPdfPath))
        {
            // ---------- Collect annotation rectangles from the first document ----------
            List<Aspose.Pdf.Rectangle> excludeAreasFirst = new List<Aspose.Pdf.Rectangle>();
            for (int pageIndex = 1; pageIndex <= firstDoc.Pages.Count; pageIndex++)
            {
                Page page = firstDoc.Pages[pageIndex];
                // Annotations collection is 1‑based as well
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation annotation = page.Annotations[annIndex];
                    if (annotation?.Rect != null)
                    {
                        excludeAreasFirst.Add(annotation.Rect);
                    }
                }
            }

            // ---------- Collect annotation rectangles from the second document ----------
            List<Aspose.Pdf.Rectangle> excludeAreasSecond = new List<Aspose.Pdf.Rectangle>();
            for (int pageIndex = 1; pageIndex <= secondDoc.Pages.Count; pageIndex++)
            {
                Page page = secondDoc.Pages[pageIndex];
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation annotation = page.Annotations[annIndex];
                    if (annotation?.Rect != null)
                    {
                        excludeAreasSecond.Add(annotation.Rect);
                    }
                }
            }

            // ---------- Configure comparison options ----------
            // The correct property names are ExcludeAreas1 and ExcludeAreas2.
            SideBySideComparisonOptions compareOptions = new SideBySideComparisonOptions
            {
                ExcludeAreas1 = excludeAreasFirst.ToArray(),
                ExcludeAreas2 = excludeAreasSecond.ToArray()
            };

            // ---------- Perform side‑by‑side comparison ----------
            // The result PDF will contain the two documents side by side,
            // with the specified annotation rectangles excluded from the diff.
            SideBySidePdfComparer.Compare(firstDoc, secondDoc, resultPdfPath, compareOptions);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
    }
}