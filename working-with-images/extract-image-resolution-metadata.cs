using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Collect report lines for each image found
            var reportLines = new List<string>();

            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Search for image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                page.Accept(absorber);

                // Iterate over each image placement to extract resolution metadata
                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    // Image rectangle (visible dimensions) in points
                    double llx = placement.Rectangle.LLX;
                    double lly = placement.Rectangle.LLY;
                    double width = placement.Rectangle.Width;
                    double height = placement.Rectangle.Height;

                    // Image resolution in DPI (horizontal and vertical)
                    double resX = placement.Resolution.X;
                    double resY = placement.Resolution.Y;

                    // Build a human‑readable report entry
                    string line = $"Page {pageIndex}: Image at ({llx}, {lly}), " +
                                  $"size {width}×{height} pt, resolution {resX}×{resY} DPI";
                    reportLines.Add(line);
                }
            }

            // Output the collected metadata
            foreach (string line in reportLines)
            {
                Console.WriteLine(line);
            }

            // Example of saving the document if needed (optional)
            // doc.Save("output.pdf");
        }
    }
}