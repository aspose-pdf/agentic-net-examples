using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPath);
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            int imageCounter = 1;
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                foreach (XImage image in page.Resources.Images)
                {
                    // Save the extracted image to a temporary file (binary extension)
                    string tempFile = $"image_{pageIndex}_{imageCounter}.bin";
                    using (FileStream fs = new FileStream(tempFile, FileMode.Create, FileAccess.Write))
                    {
                        image.Save(fs);
                    }

                    // Determine the real extension by inspecting the file signature
                    string realExtension = GetExtensionFromHeader(tempFile);
                    string finalFile = Path.ChangeExtension(tempFile, realExtension);
                    if (!tempFile.Equals(finalFile, StringComparison.OrdinalIgnoreCase))
                    {
                        File.Move(tempFile, finalFile);
                    }

                    // Verify the file signature (magic number)
                    bool isValid = ValidateSignature(finalFile);
                    Console.WriteLine($"{finalFile}: {(isValid ? "valid" : "corrupted or unknown format")}");
                    imageCounter++;
                }
            }
        }
    }

    // Detect image type from the first bytes of the file and return the proper extension
    private static string GetExtensionFromHeader(string filePath)
    {
        byte[] header = new byte[8];
        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            int bytesRead = fs.Read(header, 0, header.Length);
            if (bytesRead < 2)
                return ".bin";
        }

        // JPEG: FF D8 FF E0 (or FF D8 FF E1, etc.)
        if (header[0] == 0xFF && header[1] == 0xD8)
            return ".jpg";
        // PNG: 89 50 4E 47 0D 0A 1A 0A
        if (header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47)
            return ".png";
        // GIF: "GIF87a" or "GIF89a"
        if (header[0] == (byte)'G' && header[1] == (byte)'I' && header[2] == (byte)'F')
            return ".gif";
        // TIFF (little endian): 49 49 2A 00
        if (header[0] == 0x49 && header[1] == 0x49 && header[2] == 0x2A && header[3] == 0x00)
            return ".tif";
        // TIFF (big endian): 4D 4D 00 2A
        if (header[0] == 0x4D && header[1] == 0x4D && header[2] == 0x00 && header[3] == 0x2A)
            return ".tif";

        return ".bin"; // unknown format
    }

    // Validate the signature based on the file's extension (already detected)
    private static bool ValidateSignature(string filePath)
    {
        string extension = Path.GetExtension(filePath).ToLowerInvariant();
        byte[] header = new byte[8];
        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            int bytesRead = fs.Read(header, 0, header.Length);
            if (bytesRead < 2)
                return false;
        }

        switch (extension)
        {
            case ".jpg":
            case ".jpeg":
                return header[0] == 0xFF && header[1] == 0xD8;
            case ".png":
                return header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47;
            case ".gif":
                return header[0] == (byte)'G' && header[1] == (byte)'I' && header[2] == (byte)'F';
            case ".tif":
            case ".tiff":
                return (header[0] == 0x49 && header[1] == 0x49 && header[2] == 0x2A) ||
                       (header[0] == 0x4D && header[1] == 0x4D && header[2] == 0x00);
            default:
                return false;
        }
    }
}
