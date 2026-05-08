using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            int imageIndex = 1;

            // Iterate over all pages (pages are 1‑based)
            foreach (Page page in pdfDoc.Pages)
            {
                // Iterate over the image collection (rule: foreach XImage, not dictionary)
                foreach (XImage img in page.Resources.Images)
                {
                    // Save the image to a memory stream first so we can inspect the header
                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms); // preserves original format
                        ms.Position = 0;

                        // Determine the original image format from the header bytes
                        string extension = GetExtensionFromHeader(ms);
                        string outPath = Path.Combine(outputFolder, $"image_{imageIndex}{extension}");

                        // Write the stream to the final file
                        using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                        {
                            ms.CopyTo(fs);
                        }

                        Console.WriteLine($"Saved image #{imageIndex} to '{outPath}'");
                        imageIndex++;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Inspects the first bytes of a stream that contains an image and returns a suitable file extension.
    /// Supports the most common raster formats. If the format cannot be identified, returns ".bin".
    /// </summary>
    private static string GetExtensionFromHeader(Stream imageStream)
    {
        // Read up to 12 bytes – enough for the signatures we check.
        byte[] header = new byte[12];
        int read = imageStream.Read(header, 0, header.Length);
        if (read < 4)
            return ".bin";

        // JPEG: FF D8 FF
        if (header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF)
            return ".jpg";

        // PNG: 89 50 4E 47 0D 0A 1A 0A
        if (header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47)
            return ".png";

        // GIF: 47 49 46 38 (GIF8)
        if (header[0] == 0x47 && header[1] == 0x49 && header[2] == 0x46 && header[3] == 0x38)
            return ".gif";

        // BMP: 42 4D
        if (header[0] == 0x42 && header[1] == 0x4D)
            return ".bmp";

        // TIFF (little endian): 49 49 2A 00 ; big endian: 4D 4D 00 2A
        if ((header[0] == 0x49 && header[1] == 0x49 && header[2] == 0x2A && header[3] == 0x00) ||
            (header[0] == 0x4D && header[1] == 0x4D && header[2] == 0x00 && header[3] == 0x2A))
            return ".tiff";

        // JPEG2000 (JP2): 00 00 00 0C 6A 50 20 20
        if (header[0] == 0x00 && header[1] == 0x00 && header[2] == 0x00 && header[3] == 0x0C &&
            header[4] == 0x6A && header[5] == 0x50 && header[6] == 0x20 && header[7] == 0x20)
            return ".jp2";

        // JBIG2: 97 4A 42 32 ("\x97JB2") – rare, but we include a simple check
        if (header[0] == 0x97 && header[1] == 0x4A && header[2] == 0x42 && header[3] == 0x32)
            return ".jb2";

        // Fallback for unknown formats
        return ".bin";
    }
}
