using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // ImagePlacementAbsorber

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "FilteredImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Use ImagePlacementAbsorber to locate image placements on the page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                page.Accept(absorber);

                int imageCounter = 0; // used to build a unique file name per page

                // Process each found image placement
                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    // The ImagePlacement provides the underlying XImage object
                    XImage xImg = placement.Image;

                    // Determine the MIME type of the image using a version‑agnostic helper
                    string mimeType = GetMimeType(xImg);

                    // Filter: keep only JPEG images (you can change the condition as needed)
                    if (mimeType.Equals("image/jpeg", StringComparison.OrdinalIgnoreCase))
                    {
                        imageCounter++;
                        // Build a unique file name for the extracted image
                        string extension = GetExtensionFromMime(mimeType);
                        string fileName = $"page{pageIndex}_img{imageCounter}{extension}";
                        string outPath = Path.Combine(outputDir, fileName);

                        // Save the image to disk. XImage.Save expects a Stream.
                        using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                        {
                            xImg.Save(fs);
                        }

                        Console.WriteLine($"Saved JPEG image: {outPath}");
                    }
                }
            }
        }
    }

    /// <summary>
    /// Returns a MIME type string for the supplied XImage by inspecting the first bytes of the encoded image.
    /// This avoids the removed XImage.ImageInfo property and works with all supported Aspose.Pdf versions.
    /// </summary>
    private static string GetMimeType(XImage img)
    {
        if (img == null)
            return "application/octet-stream";

        using (var ms = new MemoryStream())
        {
            img.Save(ms);
            byte[] data = ms.ToArray();
            if (data.Length >= 4)
            {
                // JPEG – FF D8
                if (data[0] == 0xFF && data[1] == 0xD8)
                    return "image/jpeg";
                // PNG – 89 50 4E 47
                if (data[0] == 0x89 && data[1] == 0x50 && data[2] == 0x4E && data[3] == 0x47)
                    return "image/png";
                // GIF – 47 49 46
                if (data[0] == 0x47 && data[1] == 0x49 && data[2] == 0x46)
                    return "image/gif";
                // BMP – 42 4D
                if (data[0] == 0x42 && data[1] == 0x4D)
                    return "image/bmp";
            }
        }
        return "application/octet-stream"; // fallback
    }

    /// <summary>
    /// Maps a MIME type to a conventional file extension.
    /// </summary>
    private static string GetExtensionFromMime(string mime)
    {
        return mime switch
        {
            "image/jpeg" => ".jpg",
            "image/png" => ".png",
            "image/gif" => ".gif",
            "image/bmp" => ".bmp",
            _ => ".bin",
        };
    }
}
