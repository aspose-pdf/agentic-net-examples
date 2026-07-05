using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string resultPdfPath = "comparison_result.pdf";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input files not found.");
            return;
        }

        // Load the two documents inside using blocks for proper disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Collect annotation rectangles from the first document
            List<Aspose.Pdf.Rectangle> excludeAreas1 = new List<Aspose.Pdf.Rectangle>();
            for (int i = 1; i <= doc1.Pages.Count; i++)
            {
                Page page = doc1.Pages[i];
                foreach (Annotation annot in page.Annotations)
                {
                    // The bounding rectangle of the annotation
                    excludeAreas1.Add(annot.Rect);
                }
            }

            // Collect annotation rectangles from the second document
            List<Aspose.Pdf.Rectangle> excludeAreas2 = new List<Aspose.Pdf.Rectangle>();
            for (int i = 1; i <= doc2.Pages.Count; i++)
            {
                Page page = doc2.Pages[i];
                foreach (Annotation annot in page.Annotations)
                {
                    excludeAreas2.Add(annot.Rect);
                }
            }

            // Set up comparison options to exclude the collected areas
            ComparisonOptions options = new ComparisonOptions
            {
                ExcludeAreas1 = excludeAreas1.ToArray(),
                ExcludeAreas2 = excludeAreas2.ToArray()
            };

            // Perform page‑by‑page text comparison and save the result
            TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPdfPath);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
    }
}