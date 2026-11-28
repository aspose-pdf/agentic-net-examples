using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // PdfExtractor implements IDisposable, so wrap it in a using block
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the source PDF document
                extractor.BindPdf(pdfPath);

                // Extract images from all pages (default mode, no page range specified)
                extractor.ExtractImage();

                int imageIndex = 1;
                // Iterate through all extracted images and save each as PNG
                while (extractor.HasNextImage())
                {
                    string outPath = Path.Combine(outputDir, $"Image_{imageIndex}.png");
                    // Save the next image directly to file in PNG format
                    extractor.GetNextImage(outPath, ImageFormat.Png);
                    imageIndex++;
                }
            }

            Console.WriteLine($"Extraction complete. Images saved to '{outputDir}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
