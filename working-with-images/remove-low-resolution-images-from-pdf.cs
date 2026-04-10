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

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                // Create an absorber to find image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();

                // Perform the search
                doc.Pages[pageIndex].Accept(absorber);

                // Examine each found image
                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    // If either horizontal or vertical DPI is below 72, remove the image
                    if (placement.Resolution.X < 72 || placement.Resolution.Y < 72)
                    {
                        placement.Hide(); // Deletes the image from the page
                    }
                }
            }

            // Clean up unused resources after deletions
            doc.OptimizeResources();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}