using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a unique temporary directory for extracted images
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeImages_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        Console.WriteLine($"Images will be saved to: {tempDir}");

        // Initialize the PDF extractor (Facades API)
        PdfExtractor extractor = new PdfExtractor();

        // Load the PDF file
        extractor.BindPdf(inputPath);

        // Define the page range (pages 2 through 5, inclusive)
        extractor.StartPage = 2;
        extractor.EndPage   = 5;

        // Perform image extraction for the specified pages
        extractor.ExtractImage();

        int imageIndex = 1;
        // Iterate over all extracted images
        while (extractor.HasNextImage())
        {
            // Get the next image into a memory stream (required overload)
            using (MemoryStream imgStream = new MemoryStream())
            {
                extractor.GetNextImage(imgStream);
                // Reset position so we can read the header for extension detection
                imgStream.Position = 0;

                // Guess the image file extension from its header bytes
                string extension = GetImageExtension(imgStream);
                string filePath = Path.Combine(tempDir, $"image_{imageIndex}{extension}");

                // Write the image to disk
                using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    imgStream.CopyTo(file);
                }

                Console.WriteLine($"Saved {filePath}");
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }

    // Helper method to infer common image extensions from the stream header
    static string GetImageExtension(Stream stream)
    {
        if (!stream.CanSeek) return ".bin";

        long originalPos = stream.Position;
        byte[] header = new byte[8];
        stream.Read(header, 0, header.Length);
        stream.Position = originalPos;

        // PNG signature
        if (header.Length >= 8 &&
            header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47)
            return ".png";

        // JPEG signature
        if (header.Length >= 3 &&
            header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF)
            return ".jpg";

        // GIF signature
        if (header.Length >= 3 &&
            header[0] == 0x47 && header[1] == 0x49 && header[2] == 0x46)
            return ".gif";

        // BMP signature
        if (header.Length >= 2 &&
            header[0] == 0x42 && header[1] == 0x4D)
            return ".bmp";

        // Default fallback
        return ".bin";
    }
}
