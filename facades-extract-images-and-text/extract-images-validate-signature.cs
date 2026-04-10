using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Extract images using PdfExtractor (facade)
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);
            extractor.ExtractImage(); // start extraction

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each extracted image; default format is PNG
                string imagePath = Path.Combine(outputDir, $"image-{imageIndex}.png");
                extractor.GetNextImage(imagePath);
                Console.WriteLine($"Saved image {imageIndex} to {imagePath}");

                // Validate file signature (magic number)
                if (ValidateImageSignature(imagePath, out string format))
                {
                    Console.WriteLine($"Image {imageIndex} signature OK ({format})");
                }
                else
                {
                    Console.WriteLine($"Image {imageIndex} signature INVALID");
                }

                imageIndex++;
            }
        }
    }

    // Checks the first bytes of a file against known image signatures.
    static bool ValidateImageSignature(string filePath, out string format)
    {
        format = "Unknown";
        byte[] header = new byte[8];
        try
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                int read = fs.Read(header, 0, header.Length);
                if (read < 4) return false;
            }

            // JPEG: FF D8 FF
            if (header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF)
            {
                format = "JPEG";
                return true;
            }
            // PNG: 89 50 4E 47 0D 0A 1A 0A
            if (header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47 &&
                header[4] == 0x0D && header[5] == 0x0A && header[6] == 0x1A && header[7] == 0x0A)
            {
                format = "PNG";
                return true;
            }
            // GIF87a or GIF89a: 47 49 46 38 37 61 or 47 49 46 38 39 61
            if (header[0] == 0x47 && header[1] == 0x49 && header[2] == 0x46 && header[3] == 0x38 &&
                (header[4] == 0x37 || header[4] == 0x39) && header[5] == 0x61)
            {
                format = "GIF";
                return true;
            }
            // BMP: 42 4D
            if (header[0] == 0x42 && header[1] == 0x4D)
            {
                format = "BMP";
                return true;
            }
            // TIFF (little endian): 49 49 2A 00
            if (header[0] == 0x49 && header[1] == 0x49 && header[2] == 0x2A && header[3] == 0x00)
            {
                format = "TIFF-LE";
                return true;
            }
            // TIFF (big endian): 4D 4D 00 2A
            if (header[0] == 0x4D && header[1] == 0x4D && header[2] == 0x00 && header[3] == 0x2A)
            {
                format = "TIFF-BE";
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading {filePath}: {ex.Message}");
            return false;
        }
    }
}