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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                // Create an absorber to find image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();

                // Perform the search on the page
                doc.Pages[pageIndex].Accept(absorber);

                // Examine each found image placement
                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    // Image resolution is expressed in DPI (horizontal and vertical)
                    double dpiX = placement.Resolution.X;
                    double dpiY = placement.Resolution.Y;

                    // Delete images whose DPI is lower than 72 on either axis
                    if (dpiX < 72 || dpiY < 72)
                    {
                        placement.Hide(); // removes the image from the page
                    }
                }
            }

            // Optional: remove now‑unused resources to shrink the file
            doc.OptimizeResources();

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}