using System;
using System.IO;
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
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create an absorber to find image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();

                // Perform the search on the page
                page.Accept(absorber);

                Console.WriteLine($"Page {i} contains {absorber.ImagePlacements.Count} image(s).");

                // Report resolution (DPI) for each found image
                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    // Resolution.X and Resolution.Y give horizontal and vertical DPI
                    Console.WriteLine($"  Image resolution: {placement.Resolution.X} DPI (X), {placement.Resolution.Y} DPI (Y)");
                    // Optional: report the visible size of the image on the page
                    Console.WriteLine($"  Visible size: {placement.Rectangle.Width} x {placement.Rectangle.Height}");
                }
            }
        }
    }
}