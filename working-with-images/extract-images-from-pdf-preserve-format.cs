using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";
        const string outFolder = "ExtractedImages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outFolder);

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                int imageIndex = 0;

                // Iterate directly over the XImage collection (not a dictionary)
                foreach (XImage img in page.Resources.Images)
                {
                    imageIndex++;

                    // Save the image to a memory stream first
                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms); // XImage.Save accepts a Stream
                        ms.Position = 0;

                        // Detect original format from the first bytes of the stream
                        string ext = DetectExtensionFromHeader(ms);
                        if (string.IsNullOrEmpty(ext))
                            ext = ".bin"; // fallback

                        string fileName = $"page{pageNum}_img{imageIndex}{ext}";
                        string outPath  = Path.Combine(outFolder, fileName);

                        // Write the stream to disk using the detected extension
                        using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                        {
                            ms.CopyTo(fs);
                        }

                        Console.WriteLine($"Saved image: {outPath}");
                    }
                }
            }
        }
    }

    // Detect image format by inspecting the first few bytes (file header)
    private static string DetectExtensionFromHeader(Stream stream)
    {
        // Read enough bytes for all supported formats (max 12 bytes for WebP detection)
        byte[] header = new byte[12];
        int bytesRead = stream.Read(header, 0, header.Length);
        if (bytesRead < 4)
            return null;

        // JPEG: FF D8 FF
        if (header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF)
            return ".jpg";

        // PNG: 89 50 4E 47 0D 0A 1A 0A
        if (header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47)
            return ".png";

        // GIF: 47 49 46 38
        if (header[0] == 0x47 && header[1] == 0x49 && header[2] == 0x46 && header[3] == 0x38)
            return ".gif";

        // BMP: 42 4D
        if (header[0] == 0x42 && header[1] == 0x4D)
            return ".bmp";

        // TIFF (little endian): 49 49 2A 00
        // TIFF (big endian):    4D 4D 00 2A
        if ((header[0] == 0x49 && header[1] == 0x49 && header[2] == 0x2A && header[3] == 0x00) ||
            (header[0] == 0x4D && header[1] == 0x4D && header[2] == 0x00 && header[3] == 0x2A))
            return ".tif";

        // JPEG2000 (JP2): 00 00 00 0C 6A 50 20 20
        if (bytesRead >= 8 && header[0] == 0x00 && header[1] == 0x00 && header[2] == 0x00 && header[3] == 0x0C &&
            header[4] == 0x6A && header[5] == 0x50 && header[6] == 0x20 && header[7] == 0x20)
            return ".jp2";

        // WebP: RIFF....WEBP
        if (bytesRead >= 12 &&
            header[0] == (byte)'R' && header[1] == (byte)'I' && header[2] == (byte)'F' && header[3] == (byte)'F' &&
            header[8] == (byte)'W' && header[9] == (byte)'E' && header[10] == (byte)'B' && header[11] == (byte)'P')
            return ".webp";

        // Unknown format
        return null;
    }
}
