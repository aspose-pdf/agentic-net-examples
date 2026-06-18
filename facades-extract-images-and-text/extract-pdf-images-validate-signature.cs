using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Mapping of known image file signatures (magic numbers) to image format names.
    static readonly Dictionary<string, string> ImageSignatures = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        { "FFD8FF", "JPEG" },                                 // JPEG
        { "89504E470D0A1A0A", "PNG" },                        // PNG
        { "474946383761", "GIF87a" },                         // GIF87a
        { "474946383961", "GIF89a" },                         // GIF89a
        { "424D", "BMP" },                                    // BMP
        { "49492A00", "TIFF (little endian)" },              // TIFF little endian
        { "4D4D002A", "TIFF (big endian)" }                  // TIFF big endian
    };

    static void Main()
    {
        const string pdfPath = "sample.pdf";          // Input PDF containing images
        const string outputFolder = "ExtractedImages"; // Folder to store extracted images

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Extract images using PdfExtractor (Facade API)
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file
            extractor.BindPdf(pdfPath);

            // Enable image extraction
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Build a file name for the extracted image
                string imagePath = Path.Combine(outputFolder, $"image-{imageIndex}.bin");

                // Save the next image to disk
                extractor.GetNextImage(imagePath);

                // Validate the saved image file by checking its signature
                if (ValidateImageFile(imagePath, out string detectedFormat))
                {
                    Console.WriteLine($"Image {imageIndex}: OK ({detectedFormat}) -> {imagePath}");
                }
                else
                {
                    Console.WriteLine($"Image {imageIndex}: CORRUPTED or UNKNOWN format -> {imagePath}");
                }

                imageIndex++;
            }
        }
    }

    // Reads the first few bytes of a file and determines if they match a known image signature.
    static bool ValidateImageFile(string filePath, out string format)
    {
        format = null;

        // Read up to 8 bytes (the longest signature we check is 8 bytes for PNG)
        byte[] header = new byte[8];
        int bytesRead;

        try
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                bytesRead = fs.Read(header, 0, header.Length);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read file '{filePath}': {ex.Message}");
            return false;
        }

        // Convert the read bytes to an uppercase hex string without separators
        string hex = BitConverter.ToString(header, 0, bytesRead).Replace("-", string.Empty).ToUpperInvariant();

        // Check against known signatures (allow partial match for shorter signatures)
        foreach (var kvp in ImageSignatures)
        {
            if (hex.StartsWith(kvp.Key, StringComparison.OrdinalIgnoreCase))
            {
                format = kvp.Value;
                return true;
            }
        }

        // No matching signature found
        return false;
    }
}