using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                int imageIndex = 1;

                // XImageCollection is enumerable; iterate directly
                foreach (XImage xImg in page.Resources.Images)
                {
                    // Save the image to a memory stream first
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        xImg.Save(imgStream);
                        imgStream.Position = 0;

                        // Detect the image format from the header bytes
                        string extension = DetectImageExtension(imgStream);
                        if (string.IsNullOrEmpty(extension))
                            extension = "bin"; // fallback

                        // Build a unique file name: page{page}_img{index}.{ext}
                        string fileName = $"page{pageNum}_img{imageIndex}.{extension}";
                        string outPath  = Path.Combine(outputDir, fileName);

                        // Write the stream to the output file
                        using (FileStream outFs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                        {
                            imgStream.CopyTo(outFs);
                        }

                        Console.WriteLine($"Saved image: {outPath}");
                    }

                    imageIndex++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }

    // Determines the image file extension based on common header signatures
    private static string DetectImageExtension(Stream stream)
    {
        byte[] header = new byte[8];
        int bytesRead = stream.Read(header, 0, header.Length);
        if (bytesRead < 4) return null;

        // JPEG: FF D8
        if (header[0] == 0xFF && header[1] == 0xD8)
            return "jpg";

        // PNG: 89 50 4E 47
        if (header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47)
            return "png";

        // BMP: 42 4D
        if (header[0] == 0x42 && header[1] == 0x4D)
            return "bmp";

        // GIF: 47 49 46
        if (header[0] == 0x47 && header[1] == 0x49 && header[2] == 0x46)
            return "gif";

        // TIFF (little endian): 49 49 2A 00
        // TIFF (big endian):    4D 4D 00 2A
        if ((header[0] == 0x49 && header[1] == 0x49 && header[2] == 0x2A && header[3] == 0x00) ||
            (header[0] == 0x4D && header[1] == 0x4D && header[2] == 0x00 && header[3] == 0x2A))
            return "tiff";

        // Unknown format
        return null;
    }
}