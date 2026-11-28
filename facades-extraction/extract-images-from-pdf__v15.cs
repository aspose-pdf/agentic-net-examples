using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ImagesExtracted";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // PdfExtractor implements IDisposable, so wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdf);

            // Optional: extract images defined in resources as well
            // extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

            // Extract all images from the PDF
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string imagePath = Path.Combine(outputFolder, $"image_{imageIndex}.png");
                // Save the next image to the file system (PNG format)
                extractor.GetNextImage(imagePath, ImageFormat.Png);
                Console.WriteLine($"Saved image {imageIndex} to '{imagePath}'.");
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
