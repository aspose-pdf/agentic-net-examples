using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            int imageCounter = 0;

            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Absorb image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                page.Accept(absorber);

                // Process each found image placement
                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    // XImage represents the underlying image resource
                    XImage xImg = placement.Image;

                    // Determine file extension by inspecting the image header bytes
                    string ext = GetExtensionFromXImage(xImg);

                    // Build a unique file name per image
                    string outPath = Path.Combine(
                        outputDir,
                        $"page{pageNum}_img{++imageCounter}{ext}");

                    // Save the raw image data preserving original format and resolution
                    using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        // XImage.Save writes the image bytes exactly as stored in the PDF
                        xImg.Save(fs);
                    }

                    Console.WriteLine($"Saved image: {outPath}");
                }
            }
        }
    }

    // Helper: infer image file extension from the first bytes of the XImage data
    static string GetExtensionFromXImage(XImage img)
    {
        if (img == null)
            return ".bin";

        // Save the image to a memory stream to inspect its header
        using (var ms = new MemoryStream())
        {
            img.Save(ms);
            if (ms.Length < 4)
                return ".bin";

            byte[] header = ms.GetBuffer();

            // JPEG: FF D8 FF
            if (header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF)
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
            if (header[0] == 0x49 && header[1] == 0x49 && header[2] == 0x2A && header[3] == 0x00)
                return ".tiff";
            // TIFF (big endian): 4D 4D 00 2A
            if (header[0] == 0x4D && header[1] == 0x4D && header[2] == 0x00 && header[3] == 0x2A)
                return ".tiff";
        }
        // Fallback for unknown formats
        return ".bin";
    }
}
