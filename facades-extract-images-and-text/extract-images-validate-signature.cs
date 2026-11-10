using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Extract images from the PDF using PdfExtractor (Facades API)
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            // The ExtractImageMode property does not exist in the current Aspose.Pdf.Facades version.
            // Image extraction is performed by calling ExtractImage() which extracts all images by default.
            extractor.ExtractImage();

            int index = 1;
            while (extractor.HasNextImage())
            {
                string outPath = Path.Combine(outputDir, $"Image_{index}.png");
                // Save each image as PNG (you can change the format if needed)
                extractor.GetNextImage(outPath, ImageFormat.Png);
                index++;
            }
        }

        // Validate each saved image by checking its file signature (magic number)
        foreach (string filePath in Directory.GetFiles(outputDir))
        {
            bool isValid = ValidateImageSignature(filePath);
            Console.WriteLine($"{Path.GetFileName(filePath)}: {(isValid ? "OK" : "Corrupted or unknown format")}");
        }
    }

    // Checks the first bytes of a file against known image signatures.
    // Returns true if the signature matches JPEG, PNG, GIF, BMP, or TIFF.
    static bool ValidateImageSignature(string filePath)
    {
        byte[] header = new byte[8];
        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            int bytesRead = fs.Read(header, 0, header.Length);
            if (bytesRead < 4) return false;
        }

        // JPEG: FF D8 FF
        if (header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF)
            return true;

        // PNG: 89 50 4E 47 0D 0A 1A 0A
        if (header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47)
            return true;

        // GIF (87a or 89a): 47 49 46 38
        if (header[0] == 0x47 && header[1] == 0x49 && header[2] == 0x46 && header[3] == 0x38)
            return true;

        // BMP: 42 4D
        if (header[0] == 0x42 && header[1] == 0x4D)
            return true;

        // TIFF (little-endian): 49 49 2A 00
        // TIFF (big-endian):    4D 4D 00 2A
        if ((header[0] == 0x49 && header[1] == 0x49 && header[2] == 0x2A && header[3] == 0x00) ||
            (header[0] == 0x4D && header[1] == 0x4D && header[2] == 0x00 && header[3] == 0x2A))
            return true;

        return false;
    }
}
