using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPdf = "output.pdf";

        // Create a simple in‑memory PNG image (red square)
        byte[] pngBytes;
        using (var bmp = new Bitmap(200, 200))
        {
            using (var g = Graphics.FromImage(bmp))
            {
                // Fully‑qualified Color to avoid ambiguity with Aspose.Pdf.Color
                g.Clear(System.Drawing.Color.Red);
            }
            using (var ms = new MemoryStream())
            {
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                pngBytes = ms.ToArray();
            }
        }

        using (Document pdfDoc = new Document())
        {
            // Add a blank page (first page will have index 1)
            pdfDoc.Pages.Add();

            // Use fully‑qualified Aspose.Pdf.Image to avoid ambiguity
            Aspose.Pdf.Image img = new Aspose.Pdf.Image
            {
                // Load the image from an in‑memory stream instead of a file path
                ImageStream = new MemoryStream(pngBytes)
            };

            // Add the image to the page's Paragraphs collection
            pdfDoc.Pages[1].Paragraphs.Add(img);

            // Save the PDF document to disk
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with raster image saved to '{outputPdf}'.");
    }
}
