using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Determines image format by inspecting the file header (magic numbers).
    static string GetImageFormat(string filePath)
    {
        byte[] header = new byte[8];
        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            // Read up to 8 bytes; fewer bytes are possible for very small files.
            int bytesRead = fs.Read(header, 0, header.Length);
        }

        // JPEG: FF D8 FF
        if (header.Length >= 3 && header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF)
            return "jpg";

        // PNG: 89 50 4E 47 0D 0A 1A 0A
        if (header.Length >= 8 &&
            header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47 &&
            header[4] == 0x0D && header[5] == 0x0A && header[6] == 0x1A && header[7] == 0x0A)
            return "png";

        // GIF: 47 49 46 38
        if (header.Length >= 4 && header[0] == 0x47 && header[1] == 0x49 && header[2] == 0x46 && header[3] == 0x38)
            return "gif";

        // BMP: 42 4D
        if (header.Length >= 2 && header[0] == 0x42 && header[1] == 0x4D)
            return "bmp";

        // TIFF (little endian): 49 49 2A 00
        if (header.Length >= 4 && header[0] == 0x49 && header[1] == 0x49 && header[2] == 0x2A && header[3] == 0x00)
            return "tif";

        // TIFF (big endian): 4D 4D 00 2A
        if (header.Length >= 4 && header[0] == 0x4D && header[1] == 0x4D && header[2] == 0x00 && header[3] == 0x2A)
            return "tif";

        // Unknown or corrupted format
        return null;
    }

    static void Main()
    {
        const string inputPdf   = "sample.pdf";               // PDF containing images
        const string outputDir  = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Use PdfExtractor (Facade) to pull images from the PDF.
        using (Aspose.Pdf.Facades.PdfExtractor extractor = new Aspose.Pdf.Facades.PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractImage(); // Prepare extraction of images.

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Temporary file without extension; will be renamed after validation.
                string tempPath = Path.Combine(outputDir, $"image_{imageIndex}.bin");

                // Save the next image to the temporary file.
                extractor.GetNextImage(tempPath);

                // Verify the file signature (magic number) to ensure the image is not corrupted.
                string format = GetImageFormat(tempPath);
                if (format == null)
                {
                    Console.WriteLine($"Image {imageIndex}: unknown or corrupted file signature.");
                    // Optionally delete the invalid file.
                    File.Delete(tempPath);
                }
                else
                {
                    // Rename the file with the correct extension.
                    string finalPath = Path.ChangeExtension(tempPath, format);
                    File.Move(tempPath, finalPath);
                    Console.WriteLine($"Image {imageIndex}: extracted as {format.ToUpper()} and appears valid.");
                }

                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction and validation completed.");
    }
}