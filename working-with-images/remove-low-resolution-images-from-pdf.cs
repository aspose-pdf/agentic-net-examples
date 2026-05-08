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
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Create a fresh absorber for the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();

                // Perform the search on the page
                doc.Pages[i].Accept(absorber);

                // Examine each found image placement
                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    // Resolution is expressed in DPI (dots per inch)
                    double dpiX = placement.Resolution.X;
                    double dpiY = placement.Resolution.Y;

                    // If either horizontal or vertical DPI is lower than 72, remove the image
                    if (dpiX < 72 || dpiY < 72)
                    {
                        placement.Hide(); // Deletes the image from the page
                    }
                }
            }

            // Optional: clean up unused resources after deletions
            doc.OptimizeResources();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}