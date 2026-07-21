using System;
using System.IO;
using System.Drawing.Imaging; // Added for ImageFormat
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (Facade) to extract images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdf);

            // Extract images defined in the PDF resources
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Retrieve the next image as PNG into a memory stream
                using (MemoryStream pngStream = new MemoryStream())
                {
                    // GetNextImage returns a bool indicating success; we ignore it here
                    extractor.GetNextImage(pngStream, ImageFormat.Png);
                    pngStream.Position = 0; // Reset stream position for reading

                    string outputPath = Path.Combine(outputDir, $"image-{imageIndex}.png");

                    // Directly write the PNG stream to disk. The extracted PNG is already losslessly
                    // compressed; additional recompression would require an external library such as
                    // ImageSharp, which is omitted here to avoid missing assembly references.
                    using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        pngStream.CopyTo(fileStream);
                    }
                }

                imageIndex++;
            }
        }

        Console.WriteLine("All images extracted and saved with lossless compression.");
    }
}
