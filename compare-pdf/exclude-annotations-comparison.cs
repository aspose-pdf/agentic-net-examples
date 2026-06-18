using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Create first sample PDF with a text annotation
        using (Document doc1 = new Document())
        {
            Page page1 = doc1.Pages.Add();
            TextAnnotation textAnnot = new TextAnnotation(doc1);
            textAnnot.Rect = new Rectangle(100, 700, 200, 750);
            textAnnot.Title = "Note1";
            textAnnot.Contents = "Sample annotation 1";
            page1.Annotations.Add(textAnnot);
            doc1.Save("doc1.pdf");
        }

        // Create second sample PDF with a text annotation
        using (Document doc2 = new Document())
        {
            Page page2 = doc2.Pages.Add();
            TextAnnotation textAnnot2 = new TextAnnotation(doc2);
            textAnnot2.Rect = new Rectangle(120, 710, 220, 760);
            textAnnot2.Title = "Note2";
            textAnnot2.Contents = "Sample annotation 2";
            page2.Annotations.Add(textAnnot2);
            doc2.Save("doc2.pdf");
        }

        // Load the PDFs for comparison
        using (Document firstDoc = new Document("doc1.pdf"))
        using (Document secondDoc = new Document("doc2.pdf"))
        {
            // Collect annotation rectangles from the first document
            List<Rectangle> excludeFirst = new List<Rectangle>();
            for (int i = 1; i <= firstDoc.Pages.Count; i++)
            {
                Page page = firstDoc.Pages[i];
                for (int j = 1; j <= page.Annotations.Count; j++)
                {
                    Annotation annot = page.Annotations[j];
                    excludeFirst.Add(annot.Rect);
                }
            }

            // Collect annotation rectangles from the second document
            List<Rectangle> excludeSecond = new List<Rectangle>();
            for (int i = 1; i <= secondDoc.Pages.Count; i++)
            {
                Page page = secondDoc.Pages[i];
                for (int j = 1; j <= page.Annotations.Count; j++)
                {
                    Annotation annot = page.Annotations[j];
                    excludeSecond.Add(annot.Rect);
                }
            }

            // Prepare comparison options with excluded areas
            SideBySideComparisonOptions options = new SideBySideComparisonOptions();
            options.ExcludeAreas1 = excludeFirst.ToArray();
            options.ExcludeAreas2 = excludeSecond.ToArray();

            // Perform side‑by‑side visual comparison (static method)
            SideBySidePdfComparer.Compare(firstDoc, secondDoc, "comparison_result.pdf", options);
        }
    }
}
