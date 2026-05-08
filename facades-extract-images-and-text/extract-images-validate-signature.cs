using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Known file signatures (magic numbers) for common image formats
    private static readonly byte[] PngSignature = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
    private static readonly byte[] JpegSignature = new byte[] { 0xFF, 0xD8 };
    private static readonly byte[] GifSignature = new byte[] { 0x47, 0x49, 0x46, 0x38 };
    private static readonly byte[] BmpSignature = new byte[] { 0x42, 0x4D };

    static void Main()
    {
        const string inputPdf = "sample.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (facade) to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdf);

            // Prepare the extractor for image extraction
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Build a file name for the extracted image
                string imagePath = Path.Combine(outputDir, $"image_{imageIndex}.png");

                // Save the next image to the file system
                extractor.GetNextImage(imagePath);

                // Verify the saved image by checking its file signature (magic number)
                if (IsFileSignatureValid(imagePath, out string format))
                {
                    Console.WriteLine($"Image {imageIndex}: valid {format} file saved to '{imagePath}'.");
                }
                else
                {
                    Console.WriteLine($"Image {imageIndex}: corrupted or unknown format at '{imagePath}'.");
                }

                imageIndex++;
            }
        }
    }

    // Checks the file's magic number against known image signatures.
    // Returns true if a known signature matches; also outputs the detected format.
    private static bool IsFileSignatureValid(string filePath, out string format)
    {
        format = "unknown";

        // Read enough bytes to cover the longest signature we check (PNG = 8 bytes)
        byte[] header = new byte[8];
        int bytesRead = 0;
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

        if (bytesRead >= PngSignature.Length && StartsWith(header, PngSignature))
        {
            format = "PNG";
            return true;
        }
        if (bytesRead >= JpegSignature.Length && StartsWith(header, JpegSignature))
        {
            format = "JPEG";
            return true;
        }
        if (bytesRead >= GifSignature.Length && StartsWith(header, GifSignature))
        {
            format = "GIF";
            return true;
        }
        if (bytesRead >= BmpSignature.Length && StartsWith(header, BmpSignature))
        {
            format = "BMP";
            return true;
        }

        return false;
    }

    // Helper to compare the start of a byte array with a signature pattern
    private static bool StartsWith(byte[] source, byte[] signature)
    {
        if (source.Length < signature.Length) return false;
        for (int i = 0; i < signature.Length; i++)
        {
            if (source[i] != signature[i]) return false;
        }
        return true;
    }
}