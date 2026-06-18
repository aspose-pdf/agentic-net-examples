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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                // Create an absorber that finds image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();

                // Perform the search on the page
                doc.Pages[pageIndex].Accept(absorber);

                // Examine each found image placement
                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    // If either horizontal or vertical resolution is below 72 DPI, hide the image
                    if (placement.Resolution.X < 72 || placement.Resolution.Y < 72)
                    {
                        placement.Hide();
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}