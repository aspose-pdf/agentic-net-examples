using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Create an absorber that finds image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();

                // Perform the search
                page.Accept(absorber);

                // Examine each found image placement
                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    // If either horizontal or vertical DPI is lower than 72, remove the image
                    if (placement.Resolution.X < 72 || placement.Resolution.Y < 72)
                    {
                        placement.Hide(); // Deletes the image from the page
                    }
                }
            }

            // Clean up any now‑unused resources (optional but recommended)
            doc.OptimizeResources();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}