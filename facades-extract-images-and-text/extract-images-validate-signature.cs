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
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor to extract images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);          // Bind the source PDF
            extractor.ExtractImage();            // Enable image extraction mode

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save each image using its original format (no ImageFormat enum required)
                string imagePath = Path.Combine(outputDir, $"image_{imageIndex}.png");
                extractor.GetNextImage(imagePath);

                bool isValid = ValidateImageSignature(imagePath);
                Console.WriteLine($"{Path.GetFileName(imagePath)} - {(isValid ? "OK" : "CORRUPTED")}");

                imageIndex++;
            }
        }
    }

    // Checks the file signature (magic number) of common image formats
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
        if (header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47 &&
            header[4] == 0x0D && header[5] == 0x0A && header[6] == 0x1A && header[7] == 0x0A)
            return true;

        // GIF: "GIF87a" or "GIF89a"
        if (header[0] == (byte)'G' && header[1] == (byte)'I' && header[2] == (byte)'F' &&
            header[3] == (byte)'8' && (header[4] == (byte)'7' || header[4] == (byte)'9') && header[5] == (byte)'a')
            return true;

        // BMP: 42 4D
        if (header[0] == 0x42 && header[1] == 0x4D)
            return true;

        // Unknown or unsupported format
        return false;
    }
}
