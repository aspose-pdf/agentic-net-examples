using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const int targetPage = 5; // page to extract images from

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfExtractor is a Facades class used for extracting resources such as images.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file.
            extractor.BindPdf(pdfPath);

            // Limit extraction to page 5 only.
            extractor.StartPage = targetPage;
            extractor.EndPage   = targetPage;

            // The ExtractImageMode property is not available in the current Aspose.Pdf version.
            // Extraction of images is performed by calling ExtractImage() which extracts all images by default.
            extractor.ExtractImage();

            int imageIndex = 0;
            while (extractor.HasNextImage())
            {
                // Save the raw extracted image to a temporary file.
                string tempPath = $"temp_page{targetPage}_image_{imageIndex + 1}.png";
                extractor.GetNextImage(tempPath);

                // Load the temporary image, convert to grayscale, and save as JPEG.
                using (Bitmap original = new Bitmap(tempPath))
                using (Bitmap gray = new Bitmap(original.Width, original.Height))
                {
                    using (Graphics g = Graphics.FromImage(gray))
                    {
                        var matrix = new ColorMatrix(new float[][]
                        {
                            new float[] {0.299f, 0.299f, 0.299f, 0, 0},
                            new float[] {0.587f, 0.587f, 0.587f, 0, 0},
                            new float[] {0.114f, 0.114f, 0.114f, 0, 0},
                            new float[] {0,      0,      0,      1, 0},
                            new float[] {0,      0,      0,      0, 1}
                        });
                        var attrs = new ImageAttributes();
                        attrs.SetColorMatrix(matrix);
                        g.DrawImage(original,
                                    new Rectangle(0, 0, original.Width, original.Height),
                                    0, 0, original.Width, original.Height,
                                    GraphicsUnit.Pixel, attrs);
                    }

                    string outFile = $"page{targetPage}_image_{imageIndex + 1}.jpg";
                    gray.Save(outFile, ImageFormat.Jpeg);
                    Console.WriteLine($"Saved grayscale image: {outFile}");
                }

                // Clean up the temporary file.
                try { File.Delete(tempPath); } catch { /* ignore */ }

                imageIndex++;
            }

            if (imageIndex == 0)
                Console.WriteLine($"No images found on page {targetPage}.");
        }
    }
}
