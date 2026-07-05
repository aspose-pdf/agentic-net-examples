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
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Search for image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                page.Accept(absorber);

                Console.WriteLine($"Page {i}: {absorber.ImagePlacements.Count} image(s) found.");

                // Iterate each found image and report its resolution and visible size
                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    double dpiX = placement.Resolution.X; // horizontal resolution (dpi)
                    double dpiY = placement.Resolution.Y; // vertical resolution (dpi)
                    double width = placement.Rectangle.Width;   // visible width on page
                    double height = placement.Rectangle.Height; // visible height on page

                    Console.WriteLine($"  Image - Width:{width} Height:{height} DPI X:{dpiX} DPI Y:{dpiY}");
                }
            }
        }
    }
}