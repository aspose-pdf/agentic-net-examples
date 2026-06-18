using System;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

namespace WatermarkExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PNG logo file
            using (Bitmap bitmap = new Bitmap(100, 100))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    // Use System.Drawing.Color for transparency
                    graphics.Clear(System.Drawing.Color.Transparent);
                    graphics.FillEllipse(Brushes.Blue, 10, 10, 80, 80);
                }
                bitmap.Save("logo.png", ImageFormat.Png);
            }

            // Create a sample PDF document (self‑contained example)
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save("input.pdf");
            }

            // Open the sample PDF and add a watermark annotation
            using (Document doc = new Document("input.pdf"))
            {
                Page page = doc.Pages[1];

                // Create a watermark artifact with the PNG logo
                WatermarkArtifact watermark = new WatermarkArtifact();
                watermark.SetImage("logo.png");
                watermark.Opacity = 0.5f; // semi‑transparent
                watermark.Rotation = 30.0; // rotate 30 degrees clockwise

                // Define the rectangle where the watermark will be placed
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

                // Create the watermark annotation (optional visual annotation)
                WatermarkAnnotation annotation = new WatermarkAnnotation(page, rect);
                annotation.Opacity = 0.5f;

                // Add the artifact to the page (background) and the annotation to the page
                page.Artifacts.Add(watermark);
                page.Annotations.Add(annotation);

                doc.Save("output.pdf");
            }
        }
    }
}
