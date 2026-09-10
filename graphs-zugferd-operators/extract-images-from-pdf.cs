using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            int imageCounter = 1;

            // Iterate over all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate over all image resources on the page
                foreach (XImage img in page.Resources.Images)
                {
                    // Save the image to a memory stream first
                    using (var ms = new MemoryStream())
                    {
                        img.Save(ms); // XImage.Save accepts a Stream, not a file path
                        ms.Position = 0;

                        // Determine the original format by inspecting the header bytes
                        byte[] header = new byte[8];
                        ms.Read(header, 0, header.Length);
                        string ext = DetectImageExtension(header);

                        // Build the output file path with the detected extension
                        string outPath = Path.Combine(outputDir, $"image_{imageCounter}{ext}");

                        // Write the stream to the file system
                        ms.Position = 0;
                        using (var fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                        {
                            ms.CopyTo(fs);
                        }

                        Console.WriteLine($"Saved image {imageCounter} to {outPath}");
                        imageCounter++;
                    }
                }
            }
        }
    }

    // Helper method to infer image file extension from the first bytes of the file.
    static string DetectImageExtension(byte[] header)
    {
        if (header.Length >= 3 && header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF)
            return ".jpg"; // JPEG
        if (header.Length >= 8 && header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47)
            return ".png"; // PNG
        if (header.Length >= 4 && header[0] == 0x47 && header[1] == 0x49 && header[2] == 0x46 && header[3] == 0x38)
            return ".gif"; // GIF
        if (header.Length >= 2 && header[0] == 0x42 && header[1] == 0x4D)
            return ".bmp"; // BMP
        if (header.Length >= 4 && (
                (header[0] == 0x49 && header[1] == 0x49 && header[2] == 0x2A && header[3] == 0x00) ||
                (header[0] == 0x4D && header[1] == 0x4D && header[2] == 0x00 && header[3] == 0x2A)))
            return ".tiff"; // TIFF
        // Simple heuristic for SVG – it starts with "<svg"
        string headerStr = System.Text.Encoding.ASCII.GetString(header);
        if (headerStr.TrimStart().StartsWith("<svg", StringComparison.OrdinalIgnoreCase))
            return ".svg";

        // Default fallback
        return ".png";
    }
}
