using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ExtractedImages";
        Directory.CreateDirectory(outputFolder);

        // Create a sample PDF that contains at least one raster image.
        CreateSamplePdfWithImage(inputPdf);

        // Extract images from the generated PDF.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            // StartPage = 1, EndPage = 0 means "all pages".
            extractor.StartPage = 1;
            extractor.EndPage = 0;
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string outputPath = Path.Combine(outputFolder, $"image-{imageIndex}.png");
                extractor.GetNextImage(outputPath);
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }

    private static void CreateSamplePdfWithImage(string path)
    {
        // Build a simple bitmap in memory (red square).
        using (Bitmap bmp = new Bitmap(100, 100))
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Fully qualify the Color to avoid ambiguity with Aspose.Pdf.Color.
                g.Clear(System.Drawing.Color.Red);
            }

            using (MemoryStream imgStream = new MemoryStream())
            {
                bmp.Save(imgStream, System.Drawing.Imaging.ImageFormat.Png);
                imgStream.Position = 0;

                // Create a PDF and place the bitmap on the first page.
                using (Document doc = new Document())
                {
                    Page page = doc.Pages.Add();
                    Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image
                    {
                        ImageStream = imgStream
                    };
                    page.Paragraphs.Add(pdfImage);
                    doc.Save(path);
                }
            }
        }
    }
}
