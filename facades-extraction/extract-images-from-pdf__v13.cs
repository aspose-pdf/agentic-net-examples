using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string imagesFolder = "Images";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(imagesFolder);

        try
        {
            // PdfExtractor implements IDisposable – wrap in using for deterministic disposal
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the source PDF
                extractor.BindPdf(inputPdfPath);

                // The ExtractImageMode property is not available in the referenced Aspose.Pdf version.
                // The default behavior extracts all images, so we simply call ExtractImage().
                extractor.ExtractImage();

                int index = 1;
                // Iterate through the extracted images
                while (extractor.HasNextImage())
                {
                    string imagePath = Path.Combine(imagesFolder, $"Image_{index}.png");
                    // Save the current image as PNG
                    extractor.GetNextImage(imagePath, ImageFormat.Png);
                    Console.WriteLine($"Saved image {index} to '{imagePath}'");
                    index++;
                }

                if (index == 1)
                {
                    Console.WriteLine("No images were found in the PDF.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during image extraction: {ex.Message}");
        }
    }
}
