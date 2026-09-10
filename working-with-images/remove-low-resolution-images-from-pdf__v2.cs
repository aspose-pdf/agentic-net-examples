using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Search for image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
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