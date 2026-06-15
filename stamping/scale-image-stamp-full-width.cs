using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF document (self‑contained example)
        using (Document createDoc = new Document())
        {
            Page page = createDoc.Pages.Add();
            TextFragment tf = new TextFragment("Sample PDF");
            page.Paragraphs.Add(tf);
            createDoc.Save("input.pdf");
        }

        // Create a sample image that will be used as a stamp
        using (Bitmap bmp = new Bitmap(400, 200))
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Graphics.Clear expects a System.Drawing.Color
                g.Clear(System.Drawing.Color.LightBlue);
                System.Drawing.Font sysFont = new System.Drawing.Font("Arial", 24);
                g.DrawString("Stamp Image", sysFont, Brushes.DarkBlue, new PointF(10, 80));
            }
            bmp.Save("stamp.png");
        }

        // Load the PDF and add the image stamp scaled to full page width while preserving aspect ratio
        using (Document doc = new Document("input.pdf"))
        {
            Page targetPage = doc.Pages[1];
            // Fully qualify System.Drawing.Image to avoid ambiguity with Aspose.Pdf.Image
            using (System.Drawing.Image sysImg = System.Drawing.Image.FromFile("stamp.png"))
            {
                double imageWidthPoints = ((double)sysImg.Width / sysImg.HorizontalResolution) * 72.0;
                double imageHeightPoints = ((double)sysImg.Height / sysImg.VerticalResolution) * 72.0;

                double pageWidth = targetPage.PageInfo.Width;
                double scaleFactor = pageWidth / imageWidthPoints;
                double scaledHeight = imageHeightPoints * scaleFactor;

                ImageStamp stamp = new ImageStamp("stamp.png");
                stamp.Width = pageWidth;
                stamp.Height = scaledHeight;
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment = VerticalAlignment.Top;
                stamp.TopMargin = 0;
                stamp.BottomMargin = 0;

                targetPage.AddStamp(stamp);
            }
            doc.Save("output.pdf");
        }
    }
}
