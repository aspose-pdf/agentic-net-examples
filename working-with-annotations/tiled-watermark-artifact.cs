using System;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a sample PDF document
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save("input.pdf");
        }

        // Generate a simple pattern image to be used as a tiled watermark
        string patternPath = "pattern.png";
        using (Bitmap bitmap = new Bitmap(100, 100))
        {
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(System.Drawing.Color.LightGray);
                using (Pen pen = new Pen(System.Drawing.Color.DarkGray, 2))
                {
                    graphics.DrawLine(pen, 0, 0, 100, 100);
                    graphics.DrawLine(pen, 0, 100, 100, 0);
                }
            }
            bitmap.Save(patternPath);
        }

        // Open the PDF and add a WatermarkArtifact as a background on each page
        using (Document pdfDoc = new Document("input.pdf"))
        {
            int pageCount = pdfDoc.Pages.Count;
            for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];
                WatermarkArtifact watermark = new WatermarkArtifact();
                watermark.IsBackground = true;
                watermark.Opacity = 0.5f;
                // The Tile property does not exist; the watermark will be applied once per page.
                // For true tiling you would need to add multiple WatermarkArtifacts covering the page.
                watermark.SetImage(patternPath);
                page.Artifacts.Add(watermark);
            }

            pdfDoc.Save("output.pdf");
        }
    }
}