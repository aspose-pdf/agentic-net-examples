using System;
using System.IO;
using Aspose.Pdf;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageStampExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF with two pages (self‑contained example)
            using (Document sourceDoc = new Document())
            {
                sourceDoc.Pages.Add();
                sourceDoc.Pages.Add();
                sourceDoc.Save("input.pdf");
            }

            // Reopen the PDF and add a fixed‑size image stamp to each page
            using (Document pdfDoc = new Document("input.pdf"))
            {
                // Create a temporary PNG image to use as the stamp
                string imagePath = "stamp.png";
                using (Bitmap bmp = new Bitmap(100, 50))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        // Use System.Drawing.Color for GDI+ drawing
                        g.Clear(System.Drawing.Color.LightBlue);
                        g.DrawString("Stamp", new Font("Arial", 12), Brushes.Black, new PointF(10, 15));
                    }
                    bmp.Save(imagePath, ImageFormat.Png);
                }

                // Initialise the ImageStamp with fixed dimensions (points)
                ImageStamp imgStamp = new ImageStamp(imagePath);
                imgStamp.Width = 100; // Fixed width in points
                imgStamp.Height = 50; // Fixed height in points
                imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
                imgStamp.VerticalAlignment = VerticalAlignment.Bottom;
                imgStamp.BottomMargin = 10;

                // Apply the same stamp to every page (evaluation mode limit: max 4 pages)
                foreach (Page page in pdfDoc.Pages)
                {
                    page.AddStamp(imgStamp);
                }

                pdfDoc.Save("output.pdf");
            }
        }
    }
}
