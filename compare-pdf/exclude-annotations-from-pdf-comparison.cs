using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string firstPath = "first.pdf";
        const string secondPath = "second.pdf";
        const string resultPath = "comparison_result.pdf";

        if (!File.Exists(firstPath) || !File.Exists(secondPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load both PDFs inside using blocks for deterministic disposal
        using (Document firstDoc = new Document(firstPath))
        using (Document secondDoc = new Document(secondPath))
        {
            // Prepare comparison options
            var options = new SideBySideComparisonOptions();

            // -------------------------------------------------
            // Collect annotation bounding rectangles from the first document
            // -------------------------------------------------
            var firstDocRects = new List<Aspose.Pdf.Rectangle>();
            foreach (Page page in firstDoc.Pages) // 1‑based page indexing
            {
                if (page.Annotations != null)
                {
                    foreach (Annotation annotation in page.Annotations)
                    {
                        firstDocRects.Add(annotation.Rect);
                    }
                }
            }

            // -------------------------------------------------
            // Collect annotation bounding rectangles from the second document
            // -------------------------------------------------
            var secondDocRects = new List<Aspose.Pdf.Rectangle>();
            foreach (Page page in secondDoc.Pages)
            {
                if (page.Annotations != null)
                {
                    foreach (Annotation annotation in page.Annotations)
                    {
                        secondDocRects.Add(annotation.Rect);
                    }
                }
            }

            // Assign the collected rectangles to the comparison options
            options.ExcludeAreas1 = firstDocRects.ToArray();
            options.ExcludeAreas2 = secondDocRects.ToArray();

            // Perform side‑by‑side visual comparison (static class)
            SideBySidePdfComparer.Compare(firstDoc, secondDoc, resultPath, options);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPath}'.");
    }
}
