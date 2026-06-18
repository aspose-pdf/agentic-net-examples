using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create a unique temporary folder for extracted images
        string tempFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempFolder);

        try
        {
            // Use PdfExtractor facade to extract images from pages 2‑5
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(inputPdf);
                extractor.StartPage = 2;   // inclusive start page (1‑based)
                extractor.EndPage   = 5;   // inclusive end page
                extractor.ExtractImage();

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    string outputPath = Path.Combine(tempFolder, $"image_{imageIndex}.png");
                    // Save each image as PNG (default format can be changed via overload)
                    extractor.GetNextImage(outputPath, System.Drawing.Imaging.ImageFormat.Png);
                    imageIndex++;
                }
            }

            Console.WriteLine($"Images extracted to temporary folder: {tempFolder}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}