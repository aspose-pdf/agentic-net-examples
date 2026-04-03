using System;
using System.IO;
using Aspose.Pdf;
using System.Drawing;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string imagePath = "image.png";
        const string outputPdf = "output.pdf";

        // Ensure the raster image exists; if it does not, create a simple placeholder image.
        string actualImagePath = imagePath;
        string tempImagePath = null;
        if (!File.Exists(imagePath))
        {
            // Create a 100x100 red square bitmap as a placeholder.
            using (Bitmap bmp = new Bitmap(100, 100))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(System.Drawing.Color.Red);
                }
                tempImagePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");
                bmp.Save(tempImagePath, ImageFormat.Png);
                actualImagePath = tempImagePath;
            }
        }

        using (Document pdfDoc = new Document())
        {
            // Add a blank page (pages are 1‑based).
            pdfDoc.Pages.Add();

            // Create an Aspose.Pdf.Image and set its source file.
            Aspose.Pdf.Image pdfImg = new Aspose.Pdf.Image
            {
                File = actualImagePath
            };

            // Add the image to the first page via the Paragraphs collection.
            pdfDoc.Pages[1].Paragraphs.Add(pdfImg);

            // Save the PDF document.
            pdfDoc.Save(outputPdf);
        }

        // Clean up the temporary placeholder image if one was created.
        if (tempImagePath != null && File.Exists(tempImagePath))
        {
            try { File.Delete(tempImagePath); } catch { /* ignore cleanup errors */ }
        }

        Console.WriteLine($"PDF with image saved to '{outputPdf}'.");
    }
}
