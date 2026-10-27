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

        // Bind the PDF and extract all images using Aspose.Pdf.Facades
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);
            // The ExtractImageMode property does not exist in the referenced Aspose.Pdf version.
            // Calling ExtractImage() extracts all images by default, so we simply invoke it.
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string tempPath = Path.Combine(outputDir, $"image_{imageIndex}.bin"); // temporary extension
                extractor.GetNextImage(tempPath);

                bool isValid = IsValidImage(tempPath);
                Console.WriteLine($"{Path.GetFileName(tempPath)}: {(isValid ? "valid" : "corrupted")}");

                if (isValid)
                {
                    // Determine proper extension from file signature and rename
                    string properExt = GetImageExtension(tempPath);
                    string finalPath = Path.ChangeExtension(tempPath, properExt);
                    File.Move(tempPath, finalPath);
                    Console.WriteLine($"Renamed to {Path.GetFileName(finalPath)}");
                }
                else
                {
                    // Optionally delete corrupted files
                    // File.Delete(tempPath);
                }

                imageIndex++;
            }
        }
    }

    // Checks the file header against known image signatures
    static bool IsValidImage(string path)
    {
        byte[] header = new byte[8];
        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            int read = fs.Read(header, 0, header.Length);
            if (read < 4) return false;
        }

        // PNG: 89 50 4E 47 0D 0A 1A 0A
        if (header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47)
            return true;
        // JPEG: FF D8 FF
        if (header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF)
            return true;
        // GIF: 47 49 46 38
        if (header[0] == 0x47 && header[1] == 0x49 && header[2] == 0x46 && header[3] == 0x38)
            return true;
        // BMP: 42 4D
        if (header[0] == 0x42 && header[1] == 0x4D)
            return true;

        return false;
    }

    // Returns the appropriate file extension based on the detected signature
    static string GetImageExtension(string path)
    {
        byte[] header = new byte[8];
        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            fs.Read(header, 0, header.Length);
        }

        if (header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47)
            return ".png";
        if (header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF)
            return ".jpg";
        if (header[0] == 0x47 && header[1] == 0x49 && header[2] == 0x46 && header[3] == 0x38)
            return ".gif";
        if (header[0] == 0x42 && header[1] == 0x4D)
            return ".bmp";

        return ".bin";
    }
}
