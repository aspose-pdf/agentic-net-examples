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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create an absorber that will find all image placements on the page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();

                // Perform the search on the current page
                page.Accept(absorber);

                // Examine each found image
                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    // ImageResolution is expressed in DPI (X = horizontal, Y = vertical)
                    if (placement.Resolution.X < 72 || placement.Resolution.Y < 72)
                    {
                        // Hide removes the image from the page content
                        placement.Hide();
                    }
                }
            }

            // Remove now‑unused resources (optional but recommended)
            doc.OptimizeResources();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}