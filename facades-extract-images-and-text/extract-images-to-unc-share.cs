using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Destination UNC network share (ensure proper escaping)
        const string uncPath = @"\\myServer\shared\Images";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the destination directory exists (creates it if missing)
        Directory.CreateDirectory(uncPath);

        try
        {
            // PdfExtractor implements IDisposable – use a using block for deterministic cleanup
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the source PDF
                extractor.BindPdf(inputPdf);

                // Enable image extraction
                extractor.ExtractImage();

                int imageIndex = 1;
                // Iterate through all extracted images
                while (extractor.HasNextImage())
                {
                    // Retrieve the next image as a stream
                    using (MemoryStream imageStream = new MemoryStream())
                    {
                        extractor.GetNextImage(imageStream);
                        imageStream.Position = 0; // reset stream position before saving

                        // Determine image format from the stream (Aspose.Pdf can infer from data)
                        // Save with appropriate extension based on the image format
                        string extension = GetImageExtension(imageStream);
                        string fileName = $"Image_{imageIndex}{extension}";
                        string fullPath = Path.Combine(uncPath, fileName);

                        // Write the image to the UNC location
                        using (FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                        {
                            imageStream.CopyTo(file);
                        }

                        Console.WriteLine($"Saved image {imageIndex} to {fullPath}");
                    }

                    imageIndex++;
                }

                if (imageIndex == 1)
                {
                    Console.WriteLine("No images were found in the PDF.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }

    // Helper method to infer image file extension from the stream header
    private static string GetImageExtension(Stream imageStream)
    {
        // Read first few bytes to identify common image signatures
        byte[] header = new byte[8];
        imageStream.Read(header, 0, header.Length);
        imageStream.Position = 0; // reset after reading

        // JPEG
        if (header[0] == 0xFF && header[1] == 0xD8)
            return ".jpg";

        // PNG
        if (header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47)
            return ".png";

        // GIF
        if (header[0] == 0x47 && header[1] == 0x49 && header[2] == 0x46)
            return ".gif";

        // BMP
        if (header[0] == 0x42 && header[1] == 0x4D)
            return ".bmp";

        // Default to .bin if format is unknown
        return ".bin";
    }
}