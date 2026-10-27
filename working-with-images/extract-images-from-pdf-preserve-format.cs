using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            int imageCounter = 1;

            // Iterate over all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Iterate over the XImage collection – NOT as a dictionary
                foreach (XImage img in page.Resources.Images)
                {
                    // Save the image to a memory stream first
                    using (var ms = new MemoryStream())
                    {
                        img.Save(ms);
                        byte[] imageBytes = ms.ToArray();

                        // Determine original image format by inspecting the header bytes
                        string extension = DetectExtensionFromHeader(imageBytes);

                        // Build a unique file name with the detected extension
                        string fileName = $"Image_{imageCounter}{extension}";
                        string outputPath = Path.Combine(outputDir, fileName);

                        // Write the image bytes to disk
                        File.WriteAllBytes(outputPath, imageBytes);

                        Console.WriteLine($"Saved {outputPath}");
                        imageCounter++;
                    }
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }

    // Detects common image formats by examining the first few bytes (file header)
    private static string DetectExtensionFromHeader(byte[] data)
    {
        if (data.Length < 4)
            return ".bin";

        // JPEG: FF D8 FF E0 or FF D8 FF E1 ...
        if (data[0] == 0xFF && data[1] == 0xD8)
            return ".jpg";

        // PNG: 89 50 4E 47
        if (data[0] == 0x89 && data[1] == 0x50 && data[2] == 0x4E && data[3] == 0x47)
            return ".png";

        // GIF: 47 49 46 38
        if (data[0] == 0x47 && data[1] == 0x49 && data[2] == 0x46 && data[3] == 0x38)
            return ".gif";

        // TIFF (little endian): 49 49 2A 00
        // TIFF (big endian):    4D 4D 00 2A
        if ((data[0] == 0x49 && data[1] == 0x49 && data[2] == 0x2A && data[3] == 0x00) ||
            (data[0] == 0x4D && data[1] == 0x4D && data[2] == 0x00 && data[3] == 0x2A))
            return ".tif";

        // BMP: 42 4D
        if (data[0] == 0x42 && data[1] == 0x4D)
            return ".bmp";

        // JPEG2000: 00 00 00 0C 6A 50 20 20 (or similar)
        if (data.Length >= 8 && data[0] == 0x00 && data[1] == 0x00 && data[2] == 0x00 && data[3] == 0x0C &&
            data[4] == 0x6A && data[5] == 0x50 && data[6] == 0x20 && data[7] == 0x20)
            return ".jp2";

        // Fallback to generic binary extension
        return ".bin";
    }
}
