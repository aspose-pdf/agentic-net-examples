using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    // Checks the file signature (magic number) of common image formats.
    // Returns true if the signature matches a known image type, false otherwise.
    static bool ValidateImageSignature(string filePath)
    {
        // Read the first 8 bytes (enough for PNG, JPEG, GIF, BMP, TIFF)
        byte[] header = new byte[8];
        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            int bytesRead = fs.Read(header, 0, header.Length);
            if (bytesRead < 4) return false; // too short to be valid
        }

        // JPEG: FF D8 FF
        if (header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF)
            return true;

        // PNG: 89 50 4E 47 0D 0A 1A 0A
        if (header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47 &&
            header[4] == 0x0D && header[5] == 0x0A && header[6] == 0x1A && header[7] == 0x0A)
            return true;

        // GIF87a or GIF89a: 47 49 46 38 37 61 or 47 49 46 38 39 61
        if (header[0] == 0x47 && header[1] == 0x49 && header[2] == 0x46 && header[3] == 0x38 &&
            (header[4] == 0x37 || header[4] == 0x39) && header[5] == 0x61)
            return true;

        // BMP: 42 4D
        if (header[0] == 0x42 && header[1] == 0x4D)
            return true;

        // TIFF (little endian): 49 49 2A 00
        // TIFF (big endian):    4D 4D 00 2A
        if ((header[0] == 0x49 && header[1] == 0x49 && header[2] == 0x2A && header[3] == 0x00) ||
            (header[0] == 0x4D && header[1] == 0x4D && header[2] == 0x00 && header[3] == 0x2A))
            return true;

        // Unknown or unsupported format
        return false;
    }

    static void Main()
    {
        const string inputPdf = "sample.pdf"; // PDF containing images
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Use Aspose.Pdf.Facades.PdfExtractor to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdf);

            // Perform the extraction (no ExtractImageMode assignment – see fix)
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Build a file name for the extracted image
                string imagePath = Path.Combine(outputDir, $"image_{imageIndex}.png");

                // Save the next image to the file system using PNG format explicitly
                extractor.GetNextImage(imagePath, ImageFormat.Png);

                // Validate the saved image's file signature
                bool isValid = ValidateImageSignature(imagePath);
                Console.WriteLine(isValid
                    ? $"Image {imageIndex} saved and validated: {imagePath}"
                    : $"Image {imageIndex} may be corrupted (invalid signature): {imagePath}");

                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction and validation completed.");
    }
}
