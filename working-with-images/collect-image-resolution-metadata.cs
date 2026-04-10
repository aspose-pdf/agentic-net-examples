using System;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Ensure the file exists before processing
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // List to hold resolution info for reporting
            var imageResolutions = new List<string>();

            // Iterate over all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Use ImagePlacementAbsorber to locate images on the page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                page.Accept(absorber);

                // Collect resolution data for each image placement
                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    var res = placement.Resolution; // Resolution.X and Resolution.Y
                    string info = $"Page {pageIndex}: " +
                                  $"Image at [{placement.Rectangle.LLX}, {placement.Rectangle.LLY}, " +
                                  $"{placement.Rectangle.Width}, {placement.Rectangle.Height}] " +
                                  $"Resolution = {res.X} DPI (X), {res.Y} DPI (Y)";
                    imageResolutions.Add(info);
                }
            }

            // Output the collected metadata
            Console.WriteLine("Image Resolution Report:");
            foreach (string line in imageResolutions)
            {
                Console.WriteLine(line);
            }
        }
    }
}