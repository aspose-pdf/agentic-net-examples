using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // Open the encrypted PDF using the user password
            using (Document doc = new Document(inputPath, userPassword))
            {
                // Decrypt the document (optional, but ensures full access)
                doc.Decrypt();

                int imageIndex = 0;

                // Pages are 1‑based in Aspose.Pdf
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Iterate over all images on the page
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Save the image to a memory stream first
                        using (MemoryStream ms = new MemoryStream())
                        {
                            img.Save(ms); // XImage.Save accepts a Stream
                            ms.Position = 0;

                            string extension = GetImageExtensionFromHeader(ms);
                            string fileName = Path.Combine(outputDir, $"Page_{i}_Image_{++imageIndex}{extension}");

                            // Write the stream to the output file
                            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                            {
                                ms.CopyTo(fs);
                            }
                        }
                    }
                }

                Console.WriteLine($"Extracted {imageIndex} image(s) to '{outputDir}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Determines a suitable file extension by inspecting the first bytes of the image stream
    static string GetImageExtensionFromHeader(Stream stream)
    {
        // Ensure we can read the first 8 bytes (more than enough for the formats we check)
        byte[] header = new byte[8];
        int bytesRead = stream.Read(header, 0, header.Length);
        if (bytesRead < 2)
            return ".img";

        // JPEG: FF D8
        if (header[0] == 0xFF && header[1] == 0xD8)
            return ".jpg";
        // PNG: 89 50 4E 47 0D 0A 1A 0A
        if (header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47)
            return ".png";
        // GIF: 47 49 46 38
        if (header[0] == 0x47 && header[1] == 0x49 && header[2] == 0x46 && header[3] == 0x38)
            return ".gif";
        // TIFF (little endian): 49 49 2A 00
        if (header[0] == 0x49 && header[1] == 0x49 && header[2] == 0x2A && header[3] == 0x00)
            return ".tif";
        // TIFF (big endian): 4D 4D 00 2A
        if (header[0] == 0x4D && header[1] == 0x4D && header[2] == 0x00 && header[3] == 0x2A)
            return ".tif";
        // BMP: 42 4D
        if (header[0] == 0x42 && header[1] == 0x4D)
            return ".bmp";

        // Fallback for unknown formats
        return ".img";
    }
}
