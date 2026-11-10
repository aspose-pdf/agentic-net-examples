using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfExtractor lives here
using System.Drawing;             // For image manipulation (Windows GDI+)
using System.Drawing.Imaging;     // For saving images

class Program
{
    static void Main()
    {
        const string pdfPath        = "input.pdf";
        const string outputFolder   = "ExtractedImages";
        const string watermarkText  = "Sample Watermark";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor (Facades API) to pull images out of the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(pdfPath);

            // Enable image extraction
            extractor.ExtractImage();

            int imageNumber = 1;

            // Iterate over all extracted images
            while (extractor.HasNextImage())
            {
                // Retrieve the next image into a memory stream
                using (MemoryStream imgStream = new MemoryStream())
                {
                    extractor.GetNextImage(imgStream);
                    imgStream.Position = 0; // reset for reading

                    // Load the image for watermarking
                    using (Bitmap bitmap = new Bitmap(imgStream))
                    {
                        // Draw the watermark text onto the image
                        using (Graphics graphics = Graphics.FromImage(bitmap))
                        {
                            // Fully‑qualified System.Drawing types to avoid ambiguity with Aspose.Pdf.Facades
                            using (System.Drawing.Font font = new System.Drawing.Font("Arial", 20, System.Drawing.FontStyle.Bold))
                            {
                                Brush brush = Brushes.White; // solid white brush
                                // Measure text size to position it at bottom‑right
                                SizeF textSize = graphics.MeasureString(watermarkText, font);
                                PointF position = new PointF(
                                    bitmap.Width - textSize.Width - 10,
                                    bitmap.Height - textSize.Height - 10);

                                graphics.DrawString(watermarkText, font, brush, position);
                            }
                        }

                        // Save the watermarked image as PNG
                        string outPath = Path.Combine(outputFolder, $"Image_{imageNumber}.png");
                        bitmap.Save(outPath, ImageFormat.Png);
                        Console.WriteLine($"Saved watermarked image: {outPath}");
                    }
                }

                imageNumber++;
            }
        }
    }
}
