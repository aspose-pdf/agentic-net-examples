using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Detect image format from the first few bytes of the image stream.
    // Returns a file extension (including the dot) or ".bin" if unknown.
    static string GetImageExtension(byte[] header)
    {
        if (header.Length >= 4)
        {
            // JPEG: FF D8 FF
            if (header[0] == 0xFF && header[1] == 0xD8)
                return ".jpg";

            // PNG: 89 50 4E 47
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
        }

        // Unknown format – fallback to generic binary extension.
        return ".bin";
    }

    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // source PDF
        const string outputFolder  = "ExtractedImages";   // folder to store images

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            int imageCounter = 1;

            // Iterate through all pages (1‑based indexing).
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];

                // XImageCollection is enumerable; iterate directly.
                foreach (XImage xImg in page.Resources.Images)
                {
                    // Save the XImage to a memory stream.
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        xImg.Save(imgStream);               // Save uses the original image bytes.
                        imgStream.Position = 0;

                        // Read the first few bytes to identify the format.
                        byte[] header = new byte[8];
                        int bytesRead = imgStream.Read(header, 0, header.Length);
                        Array.Resize(ref header, bytesRead);

                        string extension = GetImageExtension(header);
                        string outputPath = Path.Combine(outputFolder,
                                                         $"image_{imageCounter}{extension}");

                        // Write the original bytes to the output file.
                        imgStream.Position = 0;
                        using (FileStream fileOut = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                        {
                            imgStream.CopyTo(fileOut);
                        }

                        Console.WriteLine($"Extracted: {outputPath}");
                        imageCounter++;
                    }
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}