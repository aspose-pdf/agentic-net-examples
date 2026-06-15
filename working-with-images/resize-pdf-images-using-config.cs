using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // for ImagePlacementAbsorber

// Configuration model for target image size per page
public class PageSizeConfig
{
    public double Width { get; set; }
    public double Height { get; set; }
}

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_resized.pdf";
        const string configPath = "config.json";

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

        // Load configuration: page number (1‑based) -> target width/height
        Dictionary<int, PageSizeConfig> pageSizeMap = LoadConfiguration(configPath);

        // Open the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate through each page that has a size entry in the config
            foreach (var kvp in pageSizeMap)
            {
                int pageNumber = kvp.Key;
                PageSizeConfig targetSize = kvp.Value;

                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                {
                    Console.WriteLine($"Page {pageNumber} is out of range, skipping.");
                    continue;
                }

                Page page = doc.Pages[pageNumber];

                // Absorb all image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                page.Accept(absorber);

                // Process each found image
                foreach (ImagePlacement imgPlacement in absorber.ImagePlacements)
                {
                    // Preserve the original position (lower‑left corner)
                    double originX = imgPlacement.Rectangle.LLX;
                    double originY = imgPlacement.Rectangle.LLY;

                    // Extract the original image bytes
                    using (MemoryStream originalStream = new MemoryStream())
                    {
                        imgPlacement.Image.Save(originalStream);
                        originalStream.Position = 0;

                        // Remove the original image from the page
                        imgPlacement.Hide();

                        // Define a new rectangle with the target dimensions
                        Aspose.Pdf.Rectangle newRect = new Aspose.Pdf.Rectangle(
                            originX,
                            originY,
                            originX + targetSize.Width,
                            originY + targetSize.Height);

                        // Add the image back with the new size
                        page.AddImage(originalStream, newRect);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPdfPath}'.");
    }

    // Helper to read the JSON configuration file
    private static Dictionary<int, PageSizeConfig> LoadConfiguration(string configFilePath)
    {
        string json = File.ReadAllText(configFilePath);
        // The JSON is expected to be a dictionary with string keys (page numbers)
        var rawDict = JsonSerializer.Deserialize<Dictionary<string, PageSizeConfig>>(json);
        var result = new Dictionary<int, PageSizeConfig>();

        if (rawDict != null)
        {
            foreach (var kvp in rawDict)
            {
                if (int.TryParse(kvp.Key, out int pageNum))
                {
                    result[pageNum] = kvp.Value;
                }
            }
        }

        return result;
    }
}