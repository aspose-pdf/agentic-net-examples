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
            Console.Error.WriteLine("One or both input files not found.");
            return;
        }

        // Load the two documents
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Collect annotation rectangles from the first document
            List<Aspose.Pdf.Rectangle> excludeAreas1 = new List<Aspose.Pdf.Rectangle>();
            foreach (Page page in doc1.Pages)
            {
                foreach (Annotation annot in page.Annotations)
                {
                    // The bounding rectangle of an annotation is available via the Rect property
                    if (annot != null && annot.Rect != null)
                        excludeAreas1.Add(annot.Rect);
                }
            }

            // Collect annotation rectangles from the second document
            List<Aspose.Pdf.Rectangle> excludeAreas2 = new List<Aspose.Pdf.Rectangle>();
            foreach (Page page in doc2.Pages)
            {
                foreach (Annotation annot in page.Annotations)
                {
                    if (annot != null && annot.Rect != null)
                        excludeAreas2.Add(annot.Rect);
                }
            }

            // Configure comparison options to exclude the collected areas
            SideBySideComparisonOptions options = new SideBySideComparisonOptions
            {
                ExcludeAreas1 = excludeAreas1.ToArray(),
                ExcludeAreas2 = excludeAreas2.ToArray()
                // Other options can be set here if needed (e.g., ExcludeTables = true)
            };

            // Perform side‑by‑side comparison and save the result
            SideBySidePdfComparer.Compare(doc1, doc2, resultPdfPath, options);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
    }
}