using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a sample PDF with a watermark annotation on the first page
        using (Document doc = new Document())
        {
            // Add first page
            Page firstPage = doc.Pages.Add();
            // Define rectangle for the watermark (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            Rectangle rect = new Rectangle(100, 500, 400, 550);
            // Create the watermark annotation
            WatermarkAnnotation watermark = new WatermarkAnnotation(firstPage, rect);
            watermark.Opacity = 0.5f;
            watermark.Color = Color.Gray;
            watermark.Contents = "Sample Watermark";
            // Add the annotation to the first page
            firstPage.Annotations.Add(watermark);

            // Add a second page (without any annotation yet)
            doc.Pages.Add();

            // Save the sample PDF
            doc.Save("input.pdf");
        }

        // Open the PDF and duplicate the watermark annotations to all other pages
        using (Document doc = new Document("input.pdf"))
        {
            // Get the first page (source of the watermarks)
            Page sourcePage = doc.Pages[1];
            // Iterate through annotations on the first page
            int annotationCount = sourcePage.Annotations.Count;
            for (int i = 1; i <= annotationCount; i++)
            {
                Annotation srcAnnotation = sourcePage.Annotations[i];
                WatermarkAnnotation srcWatermark = srcAnnotation as WatermarkAnnotation;
                if (srcWatermark == null)
                {
                    continue; // Skip non‑watermark annotations
                }

                // Duplicate this watermark to each subsequent page
                for (int pageIndex = 2; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page targetPage = doc.Pages[pageIndex];
                    WatermarkAnnotation newWatermark = new WatermarkAnnotation(targetPage, srcWatermark.Rect);
                    newWatermark.Opacity = srcWatermark.Opacity;
                    newWatermark.Color = srcWatermark.Color;
                    newWatermark.Contents = srcWatermark.Contents;
                    newWatermark.Name = srcWatermark.Name;
                    targetPage.Annotations.Add(newWatermark);
                }
            }

            // Save the resulting PDF
            doc.Save("output.pdf");
        }
    }
}