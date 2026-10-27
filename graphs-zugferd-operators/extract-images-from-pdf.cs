using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // XImage class

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

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            int pageNumber = 1;
            foreach (Page page in doc.Pages)
            {
                int imageIndex = 1;
                // Iterate over images; XImageCollection is not a dictionary
                foreach (XImage img in page.Resources.Images)
                {
                    // Save the image to a memory stream first so we can inspect the header
                    byte[] imageBytes;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms); // XImage.Save accepts a Stream
                        imageBytes = ms.ToArray();
                    }

                    // Determine the original format from the header bytes
                    string ext = GetExtensionFromHeader(imageBytes);
                    string fileName = $"page{pageNumber}_img{imageIndex}{ext}";
                    // Fully qualify System.IO.Path to avoid ambiguity with Aspose.Pdf.Drawing.Path
                    string outPath = System.IO.Path.Combine(outputDir, fileName);

                    // Write the raw bytes to disk – this preserves the original format and resolution
                    File.WriteAllBytes(outPath, imageBytes);

                    Console.WriteLine($"Saved: {outPath}");
                    imageIndex++;
                }
                pageNumber++;
            }
        }
    }

    // Inspects the first few bytes of an image to infer its format.
    static string GetExtensionFromHeader(byte[] data)
    {
        if (data == null || data.Length < 4)
            return ".bin"; // fallback

        // JPEG: FF D8 FF
        if (data.Length >= 3 && data[0] == 0xFF && data[1] == 0xD8 && data[2] == 0xFF)
            return ".jpg";
        // PNG: 89 50 4E 47 0D 0A 1A 0A
        if (data.Length >= 8 && data[0] == 0x89 && data[1] == 0x50 && data[2] == 0x4E && data[3] == 0x47)
            return ".png";
        // GIF: 47 49 46 38
        if (data[0] == 0x47 && data[1] == 0x49 && data[2] == 0x46 && data[3] == 0x38)
            return ".gif";
        // BMP: 42 4D
        if (data[0] == 0x42 && data[1] == 0x4D)
            return ".bmp";
        // TIFF (little endian): 49 49 2A 00
        // TIFF (big endian):    4D 4D 00 2A
        if ((data[0] == 0x49 && data[1] == 0x49 && data[2] == 0x2A && data[3] == 0x00) ||
            (data[0] == 0x4D && data[1] == 0x4D && data[2] == 0x00 && data[3] == 0x2A))
            return ".tif";

        // Unknown – default to generic image extension
        return ".img";
    }
}
