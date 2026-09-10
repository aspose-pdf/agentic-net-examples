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

        // Open the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                Page page = pdfDoc.Pages[pageNumber];
                int imageCounter = 1;

                // Iterate over all images defined in the page resources
                foreach (XImage xImage in page.Resources.Images)
                {
                    // Save the image to a memory stream first
                    using (var ms = new MemoryStream())
                    {
                        xImage.Save(ms);
                        ms.Position = 0;

                        // Determine the original image format by inspecting the header bytes
                        string extension = GetImageExtension(ms);

                        // Build a unique file name for each extracted image
                        string outputPath = Path.Combine(
                            outputFolder,
                            $"page{pageNumber}_img{imageCounter}{extension}");

                        // Write the image bytes to disk preserving the original format
                        using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                        {
                            ms.CopyTo(fileStream);
                        }

                        Console.WriteLine($"Extracted image saved to: {outputPath}");
                    }

                    imageCounter++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }

    /// <summary>
    /// Inspects the first bytes of a stream to infer the image file extension.
    /// Returns ".bin" when the format cannot be identified.
    /// </summary>
    private static string GetImageExtension(Stream imageStream)
    {
        // Read up to 8 bytes (enough for PNG, JPEG, etc.)
        byte[] header = new byte[8];
        int bytesRead = imageStream.Read(header, 0, header.Length);
        // Reset position for later copying
        imageStream.Position = 0;

        if (bytesRead >= 3 && header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF)
            return ".jpg"; // JPEG
        if (bytesRead >= 8 && header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47)
            return ".png"; // PNG
        if (bytesRead >= 6 && header[0] == 0x47 && header[1] == 0x49 && header[2] == 0x46 && header[3] == 0x38)
            return ".gif"; // GIF
        if (bytesRead >= 2 && header[0] == 0x42 && header[1] == 0x4D)
            return ".bmp"; // BMP
        if (bytesRead >= 4 && ((header[0] == 0x49 && header[1] == 0x49 && header[2] == 0x2A && header[3] == 0x00) ||
                               (header[0] == 0x4D && header[1] == 0x4D && header[2] == 0x00 && header[3] == 0x2A)))
            return ".tiff"; // TIFF
        // Simple SVG detection – SVG files are XML text that start with '<' and contain "<svg"
        if (bytesRead >= 4 && header[0] == 0x3C) // '<'
        {
            // Read a small chunk as text to look for "<svg"
            using (var reader = new StreamReader(imageStream, leaveOpen: true))
            {
                char[] buffer = new char[100];
                int charRead = reader.ReadBlock(buffer, 0, buffer.Length);
                string snippet = new string(buffer, 0, charRead).ToLowerInvariant();
                imageStream.Position = 0; // reset again for copying
                if (snippet.Contains("<svg"))
                    return ".svg";
            }
        }
        return ".bin"; // fallback
    }
}
