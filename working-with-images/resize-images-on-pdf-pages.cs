using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // for ImagePlacementAbsorber (in Aspose.Pdf namespace, but keep for clarity)

class PageImageSize
{
    public int Page { get; set; }
    public double Width { get; set; }   // target width in points
    public double Height { get; set; }  // target height in points
}

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output_resized.pdf";
        const string configFilePath = "image_sizes.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(configFilePath))
        {
            Console.Error.WriteLine($"Config file not found: {configFilePath}");
            return;
        }

        // Load configuration: JSON array of { "Page": 1, "Width": 200, "Height": 150 }
        List<PageImageSize> config;
        try
        {
            config = JsonSerializer.Deserialize<List<PageImageSize>>(File.ReadAllText(configFilePath));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to parse config file: {ex.Message}");
            return;
        }

        // Build a quick lookup: page number -> (width, height)
        var sizeMap = config.ToDictionary(c => c.Page, c => (c.Width, c.Height));

        // Open PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate through pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                if (!sizeMap.TryGetValue(pageNum, out var targetSize))
                    continue; // no resizing defined for this page

                Page page = doc.Pages[pageNum];

                // Absorb all image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                page.Accept(absorber);

                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    // Hide the original image (removes it from the page)
                    placement.Hide();

                    // Retrieve original image bytes
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        placement.Image.Save(imgStream); // saves the image with its current transformations
                        imgStream.Position = 0; // reset stream for reading

                        // Determine new rectangle based on target width/height
                        // Keep the original lower‑left corner (LLX, LLY) as the anchor point
                        double llx = placement.Rectangle.LLX;
                        double lly = placement.Rectangle.LLY;
                        double urx = llx + targetSize.Width;
                        double ury = lly + targetSize.Height;
                        Aspose.Pdf.Rectangle newRect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                        // Add the image back with the new size
                        page.AddImage(imgStream, newRect);
                    }
                }
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPdfPath}'.");
    }
}