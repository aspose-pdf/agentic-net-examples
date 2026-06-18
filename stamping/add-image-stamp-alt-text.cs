using System;
using Aspose.Pdf;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Create sample image for stamp
        string imagePath = "stamp.png";
        using (Bitmap bitmap = new Bitmap(100, 50))
        {
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                // Use System.Drawing.Color for GDI+ drawing
                graphics.Clear(System.Drawing.Color.LightGray);
                using (Font font = new Font("Arial", 12))
                {
                    graphics.DrawString("Logo", font, Brushes.Black, new PointF(10, 10));
                }
            }
            bitmap.Save(imagePath, ImageFormat.Png);
        }

        // Create a sample PDF with three pages (evaluation mode limit: max 4 pages)
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Pages.Add();
            doc.Pages.Add();
            doc.Save("input.pdf");
        }

        // Open the PDF and add image stamp with alternative text on page 3
        using (Document pdfDoc = new Document("input.pdf"))
        {
            ImageStamp stamp = new ImageStamp(imagePath);
            stamp.AlternativeText = "Company logo";
            stamp.XIndent = 100;
            stamp.YIndent = 100;
            // Add stamp to page 3 (1‑based indexing)
            pdfDoc.Pages[3].AddStamp(stamp);
            pdfDoc.Save("output.pdf");
        }
    }
}
