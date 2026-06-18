using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

public class Program
{
    public static void Main()
    {
        // Create a sample image in memory
        using (Bitmap bitmap = new Bitmap(200, 200))
        {
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(System.Drawing.Color.LightBlue);
                using (Pen pen = new Pen(System.Drawing.Color.DarkBlue, 5))
                {
                    graphics.DrawEllipse(pen, 20, 20, 160, 160);
                }
            }

            using (MemoryStream imageMemoryStream = new MemoryStream())
            {
                bitmap.Save(imageMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] imageBytes = imageMemoryStream.ToArray();

                // Create a sample PDF containing the image
                using (Document doc = new Document())
                {
                    Page page = doc.Pages.Add();
                    Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image();
                    pdfImage.ImageStream = new MemoryStream(imageBytes);
                    pdfImage.FixWidth = 200;
                    pdfImage.FixHeight = 200;
                    page.Paragraphs.Add(pdfImage);
                    doc.Save("input.pdf");
                }
            }
        }

        // Reopen the PDF and apply optimization with image compression options
        using (Document doc = new Document("input.pdf"))
        {
            OptimizationOptions options = new OptimizationOptions();
            options.CompressObjects = true;
            // Configure image compression options (the ImageCompressionOptions property is read‑only, so we modify it directly)
            options.ImageCompressionOptions.MaxResolution = 150; // down‑scale images higher than 150 DPI
            // JPEG quality can be set if the property exists in the used Aspose.Pdf version
            // Uncomment the following line if JpegQuality is available:
            // options.ImageCompressionOptions.JpegQuality = 75; // JPEG quality (0‑100)
            // Subset fonts to keep the file size low
            options.SubsetFonts = true;

            doc.OptimizeResources(options);
            doc.Save("output.pdf");
        }
    }
}
