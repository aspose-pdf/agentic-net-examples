using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPdfPath = "output.pdf";

        // Create a high‑resolution bitmap in memory (no external file needed)
        using (Bitmap bmp = new Bitmap(2000, 2000))
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(System.Drawing.Color.White);
                using (Pen pen = new Pen(System.Drawing.Color.Blue, 10))
                {
                    g.DrawEllipse(pen, 100, 100, 1800, 1800);
                }
                g.DrawString("High‑Res Image", new Font("Arial", 120), System.Drawing.Brushes.Black, new PointF(300, 900));
            }

            using (MemoryStream imgStream = new MemoryStream())
            {
                // Encode the bitmap as JPEG into the memory stream
                bmp.Save(imgStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                imgStream.Position = 0; // rewind for reading

                // Create the PDF and place the image using absolute coordinates
                using (Document pdfDoc = new Document())
                {
                    Page page = pdfDoc.Pages.Add();

                    // Fully‑qualified Aspose.Pdf.Rectangle to avoid ambiguity with System.Drawing.Rectangle
                    Aspose.Pdf.Rectangle imageRect = new Aspose.Pdf.Rectangle(100, 500, 500, 800);
                    page.AddImage(imgStream, imageRect);

                    pdfDoc.Save(outputPdfPath);
                }
            }
        }

        Console.WriteLine($"High‑resolution image inserted and saved to '{outputPdfPath}'.");
    }
}
