using System;
using System.IO;
using Aspose.Pdf;                       // Core PDF classes
using Aspose.Pdf.Vector;                // ImagePlacementAbsorber and ImagePlacement

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (lifecycle rule: use using)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];

                // Absorb image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                page.Accept(absorber);

                int imageIndex = 1;
                foreach (ImagePlacement imgPlacement in absorber.ImagePlacements)
                {
                    // Save the image to a memory stream first
                    using (MemoryStream ms = new MemoryStream())
                    {
                        imgPlacement.Image.Save(ms); // XImage.Save(Stream) keeps original format
                        ms.Position = 0;

                        // Determine file extension by inspecting the header bytes
                        byte[] header = new byte[8];
                        ms.Read(header, 0, header.Length);
                        string extension = GetExtensionFromHeader(header);

                        // Build a file name that reflects page and image order
                        string fileName = $"page{pageNum}_img{imageIndex}{extension}";
                        string outPath = Path.Combine(outputFolder, fileName);

                        // Write the image bytes to disk
                        File.WriteAllBytes(outPath, ms.ToArray());

                        Console.WriteLine($"Saved: {outPath}");
                    }
                    imageIndex++;
                }
            }
        }
    }

    // Helper: infer image file extension from the first bytes of the file
    static string GetExtensionFromHeader(byte[] header)
    {
        if (header.Length < 4)
            return ".bin"; // unknown

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
        if (header[0] == 0x49 && header[1] == 0x49 && header[2] == 0x2A && header[3] == 0x00)
            return ".tiff";
        // TIFF (big endian): 4D 4D 00 2A
        if (header[0] == 0x4D && header[1] == 0x4D && header[2] == 0x00 && header[3] == 0x2A)
            return ".tiff";

        // Fallback to PNG if format cannot be determined
        return ".png";
    }
}