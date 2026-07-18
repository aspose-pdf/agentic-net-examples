using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            int imageIndex = 1;

            // Iterate through all pages (1‑based indexing)
            foreach (Page page in pdfDoc.Pages)
            {
                // Iterate through all images on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    // Save the image to a memory stream first
                    using (var ms = new MemoryStream())
                    {
                        img.Save(ms); // XImage.Save accepts a Stream
                        byte[] imageBytes = ms.ToArray();

                        // Determine the original image format by inspecting the header bytes
                        string extension = GetImageExtension(imageBytes);

                        // Build a unique file name for each extracted image
                        string outputPath = Path.Combine(outputFolder, $"image_{imageIndex}{extension}");

                        // Write the raw bytes to disk – this preserves the original format
                        File.WriteAllBytes(outputPath, imageBytes);

                        Console.WriteLine($"Extracted image {imageIndex}: {outputPath}");
                        imageIndex++;
                    }
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }

    /// <summary>
    /// Determines a file extension based on the first few bytes of an image.
    /// Returns ".bin" if the format cannot be identified.
    /// </summary>
    private static string GetImageExtension(byte[] data)
    {
        if (data == null || data.Length < 4)
            return ".bin";

        // JPEG: FF D8 FF
        if (data[0] == 0xFF && data[1] == 0xD8 && data[2] == 0xFF)
            return ".jpg";

        // PNG: 89 50 4E 47 0D 0A 1A 0A
        if (data.Length >= 8 &&
            data[0] == 0x89 && data[1] == 0x50 && data[2] == 0x4E && data[3] == 0x47 &&
            data[4] == 0x0D && data[5] == 0x0A && data[6] == 0x1A && data[7] == 0x0A)
            return ".png";

        // GIF: 47 49 46 38 ("GIF8")
        if (data[0] == 0x47 && data[1] == 0x49 && data[2] == 0x46 && data[3] == 0x38)
            return ".gif";

        // BMP: 42 4D ("BM")
        if (data[0] == 0x42 && data[1] == 0x4D)
            return ".bmp";

        // TIFF (little endian): 49 49 2A 00
        if (data[0] == 0x49 && data[1] == 0x49 && data[2] == 0x2A && data[3] == 0x00)
            return ".tiff";

        // TIFF (big endian): 4D 4D 00 2A
        if (data[0] == 0x4D && data[1] == 0x4D && data[2] == 0x00 && data[3] == 0x2A)
            return ".tiff";

        return ".bin";
    }
}
