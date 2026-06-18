using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

namespace AddWatermarkAnnotationFooter
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample PDF with three pages
            using (Document sampleDoc = new Document())
            {
                for (int i = 1; i <= 3; i++)
                {
                    Page page = sampleDoc.Pages.Add();
                    TextFragment tf = new TextFragment("Sample page " + i);
                    tf.TextState.FontSize = 12;
                    page.Paragraphs.Add(tf);
                }
                sampleDoc.Save("input.pdf");
            }

            // Step 2: Load the PDF and add watermark annotations to the footer
            using (Document pdfDoc = new Document("input.pdf"))
            {
                int pageCount = pdfDoc.Pages.Count;
                for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
                {
                    Page page = pdfDoc.Pages[pageIndex];

                    // Define rectangle at the bottom of the page (0,0) to (200,30)
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 200, 30);

                    // Create watermark annotation
                    WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect);
                    watermark.Contents = "Page " + pageIndex;
                    watermark.Color = Aspose.Pdf.Color.Gray;
                    watermark.Opacity = 0.5f;

                    // Add annotation to the page
                    page.Annotations.Add(watermark);
                }

                pdfDoc.Save("output.pdf");
            }
        }
    }
}
