using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfExtractor implements IDisposable, wrap it in a using block.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document.
            extractor.BindPdf(inputPdf);

            // The ExtractImageMode property does not exist in the referenced Aspose.Pdf version.
            // Image extraction is performed by calling ExtractImage() which extracts all images by default.
            // extractor.ExtractImageMode = ExtractImageMode.DefinedInResources; // <-- removed for compatibility

            // Extract images from the document.
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate over each extracted image.
            while (extractor.HasNextImage())
            {
                // Store the image data in a memory stream as PNG.
                using (MemoryStream imgStream = new MemoryStream())
                {
                    // GetNextImage writes the image to the provided stream using the specified format.
                    extractor.GetNextImage(imgStream, ImageFormat.Png);
                    imgStream.Position = 0; // Reset stream position before writing to file.

                    // Define sequential PNG file name.
                    string outputPath = $"image{imageIndex}.png";

                    // Write the memory stream to a PNG file.
                    using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        imgStream.CopyTo(file);
                    }

                    Console.WriteLine($"Extracted image {imageIndex} to '{outputPath}'.");
                }
                imageIndex++;
            }
        }
    }
}
