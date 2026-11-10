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
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // PdfExtractor implements IDisposable, so wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document
            extractor.BindPdf(inputPdf);

            // Extract all images from the document
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate through extracted images
            while (extractor.HasNextImage())
            {
                // Get the next image into a memory stream
                using (MemoryStream imgStream = new MemoryStream())
                {
                    extractor.GetNextImage(imgStream);
                    imgStream.Position = 0; // Reset stream position for reading

                    // Load the image using System.Drawing.Image
                    using (Image img = Image.FromStream(imgStream))
                    {
                        // Build the output TIFF file path
                        string tiffPath = Path.Combine(outputFolder, $"Image_{imageIndex}.tiff");

                        // Save the image as TIFF (lossless archival format)
                        img.Save(tiffPath, ImageFormat.Tiff);
                        Console.WriteLine($"Saved image {imageIndex} to {tiffPath}");
                    }
                }

                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}