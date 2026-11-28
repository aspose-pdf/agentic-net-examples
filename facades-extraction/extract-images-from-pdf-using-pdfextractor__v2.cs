using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF document
                extractor.BindPdf(pdfPath);

                // The ExtractImageMode property is not available in the referenced Aspose.Pdf version.
                // Image extraction is performed by calling ExtractImage(), which extracts all images by default.
                // If a newer version provides the property, you could enable it with:
                // extractor.ExtractImageMode = Aspose.Pdf.Facades.ExtractImageMode.ActuallyUsed;

                // Extract images – this prepares the internal image list.
                extractor.ExtractImage();

                int imageIndex = 1;
                // Iterate over the extracted images.
                while (extractor.HasNextImage())
                {
                    // Keep the image in memory to avoid temporary files.
                    using (var imgStream = new MemoryStream())
                    {
                        extractor.GetNextImage(imgStream);
                        imgStream.Position = 0;
                        using (Image img = Image.FromStream(imgStream))
                        {
                            string outPath = Path.Combine(outputDir, $"image_{imageIndex}.jpg");
                            img.Save(outPath, ImageFormat.Jpeg);
                            Console.WriteLine($"Saved {outPath}");
                        }
                    }
                    imageIndex++;
                }

                Console.WriteLine($"Extracted {imageIndex - 1} image(s).");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
