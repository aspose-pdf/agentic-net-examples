using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

public class RemoveScreenAnnotationsExample
{
    public static void Main()
    {
        // Create a sample PDF with a ScreenAnnotation
        using (Document sampleDoc = new Document())
        {
            Page page = sampleDoc.Pages.Add();
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 400);
            ScreenAnnotation screenAnn = new ScreenAnnotation(page, rect, "sample.mp4");
            page.Annotations.Add(screenAnn);
            sampleDoc.Save("input.pdf");
        }

        // Open the PDF and remove all ScreenAnnotations
        using (Document pdfDoc = new Document("input.pdf"))
        {
            int pageCount = pdfDoc.Pages.Count;
            for (int i = 1; i <= pageCount; i++)
            {
                Page page = pdfDoc.Pages[i];
                // Collect annotations to delete
                System.Collections.Generic.List<Annotation> toDelete = new System.Collections.Generic.List<Annotation>();
                foreach (Annotation annotation in page.Annotations)
                {
                    if (annotation is ScreenAnnotation)
                    {
                        toDelete.Add(annotation);
                    }
                }
                // Delete collected annotations
                foreach (Annotation annotation in toDelete)
                {
                    page.Annotations.Delete(annotation);
                }
            }
            pdfDoc.Save("output.pdf");
        }
    }
}