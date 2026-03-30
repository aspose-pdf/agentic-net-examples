using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string watermarkText = "Sample Watermark";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Retrieve the next image into a memory stream
                using (MemoryStream imageStream = new MemoryStream())
                {
                    extractor.GetNextImage(imageStream);
                    imageStream.Position = 0;

                    // Load the image into a Bitmap for processing
                    using (Bitmap bitmap = new Bitmap(imageStream))
                    {
                        // Draw the watermark text onto the image
                        using (Graphics graphics = Graphics.FromImage(bitmap))
                        {
                            // Fully qualify System.Drawing.Font and FontStyle to avoid ambiguity with Aspose.Pdf.Facades.FontStyle
                            using (System.Drawing.Font font = new System.Drawing.Font("Arial", 20f, System.Drawing.FontStyle.Bold))
                            {
                                SizeF textSize = graphics.MeasureString(watermarkText, font);
                                float x = bitmap.Width - textSize.Width - 10f;
                                float y = bitmap.Height - textSize.Height - 10f;
                                // Fully qualify Color and Brush as well
                                using (Brush brush = new SolidBrush(System.Drawing.Color.FromArgb(128, 255, 255, 255)))
                                {
                                    graphics.DrawString(watermarkText, font, brush, x, y);
                                }
                            }
                        }

                        // Save the watermarked image as PNG
                        string outputFile = $"image-{imageIndex}.png";
                        bitmap.Save(outputFile, ImageFormat.Png);
                        Console.WriteLine($"Saved watermarked image: {outputFile}");
                    }
                }
                imageIndex++;
            }
        }
    }
}
