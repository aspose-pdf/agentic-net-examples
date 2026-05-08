using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Drawing.Imaging; // added for ImageFormat
using Aspose.Pdf;
using Aspose.Pdf.Devices; // still needed for other device‑related types (e.g., Resolution)

// Configuration model matching the JSON file structure
public class PageResizeConfig
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
        const string configPath     = "resizeConfig.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Load configuration: map page number -> (width, height)
        Dictionary<int, (double width, double height)> pageSizeMap = LoadConfiguration(configPath);

        // Open the PDF document (lifecycle rule: wrap in using)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing rule)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                if (!pageSizeMap.TryGetValue(pageIndex, out var targetSize))
                    continue; // No resize instruction for this page

                Page page = pdfDoc.Pages[pageIndex];

                // Absorb image placements on the current page
                ImagePlacementAbsorber imgAbsorber = new ImagePlacementAbsorber();
                page.Accept(imgAbsorber);

                // Process each image found on the page
                foreach (ImagePlacement imgPlacement in imgAbsorber.ImagePlacements)
                {
                    // Save the original image to a memory stream
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        // Use System.Drawing.Imaging.ImageFormat (correct enum)
                        imgPlacement.Image.Save(imgStream, ImageFormat.Png);
                        imgStream.Position = 0; // Reset stream for reading

                        // Remove the original image from the page
                        imgPlacement.Hide();

                        // Determine new rectangle using the same lower‑left corner
                        double llx = imgPlacement.Rectangle.LLX;
                        double lly = imgPlacement.Rectangle.LLY;
                        double urx = llx + targetSize.width;
                        double ury = lly + targetSize.height;
                        Aspose.Pdf.Rectangle newRect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                        // Add the resized image back to the page
                        page.AddImage(imgStream, newRect);
                    }
                }
            }

            // Save the modified PDF (save rule: use Document.Save)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPdfPath}'.");
    }

    // Helper to read the JSON configuration file
    private static Dictionary<int, (double width, double height)> LoadConfiguration(string configFilePath)
    {
        string json = File.ReadAllText(configFilePath);
        PageResizeConfig[]? configs = JsonSerializer.Deserialize<PageResizeConfig[]>(json);

        var map = new Dictionary<int, (double width, double height)>();
        if (configs != null)
        {
            foreach (var cfg in configs)
            {
                // Ensure positive dimensions before adding
                if (cfg.Width > 0 && cfg.Height > 0)
                {
                    map[cfg.Page] = (cfg.Width, cfg.Height);
                }
            }
        }
        return map;
    }
}
