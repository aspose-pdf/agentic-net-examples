using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use PdfExtractor (IDisposable) to extract images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdfPath);

            // Extract all images from the document
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate over extracted images
            while (extractor.HasNextImage())
            {
                // Get the next image into a memory stream
                using (MemoryStream imgStream = new MemoryStream())
                {
                    // The overload requires a Stream destination
                    extractor.GetNextImage(imgStream);
                    imgStream.Position = 0; // reset for reading

                    // Create a System.Drawing.Image from the stream
                    using (Image img = Image.FromStream(imgStream))
                    {
                        // Build the output BMP file path
                        string bmpPath = Path.Combine(outputFolder, $"Image_{imageIndex}.bmp");

                        // Save the image as BMP preserving original resolution and color depth
                        img.Save(bmpPath, ImageFormat.Bmp);
                    }
                }

                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
