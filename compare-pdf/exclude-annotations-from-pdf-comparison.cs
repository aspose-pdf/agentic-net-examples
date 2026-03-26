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
        const string firstPath = "first.pdf";
        const string secondPath = "second.pdf";
        const string outputPath = "comparison.pdf";

        if (!File.Exists(firstPath) || !File.Exists(secondPath))
        {
            Console.Error.WriteLine("Input PDF files not found.");
            return;
        }

        using (Document firstDoc = new Document(firstPath))
        using (Document secondDoc = new Document(secondPath))
        {
            // Gather annotation rectangles from the first document
            List<Aspose.Pdf.Rectangle> firstRects = new List<Aspose.Pdf.Rectangle>();
            foreach (Page page in firstDoc.Pages)
            {
                foreach (Annotation annot in page.Annotations)
                {
                    firstRects.Add(annot.Rect);
                }
            }

            // Gather annotation rectangles from the second document
            List<Aspose.Pdf.Rectangle> secondRects = new List<Aspose.Pdf.Rectangle>();
            foreach (Page page in secondDoc.Pages)
            {
                foreach (Annotation annot in page.Annotations)
                {
                    secondRects.Add(annot.Rect);
                }
            }

            SideBySideComparisonOptions options = new SideBySideComparisonOptions();
            options.ExcludeAreas1 = firstRects.ToArray();
            options.ExcludeAreas2 = secondRects.ToArray();

            // Perform side‑by‑side comparison while excluding annotation areas
            SideBySidePdfComparer.Compare(firstDoc, secondDoc, outputPath, options);
        }

        Console.WriteLine("Comparison completed: " + outputPath);
    }
}