using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // ImagePlacementAbsorber and related types are here

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create an absorber that will find all image placements in the document
            ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();

            // Perform the search on the whole document
            absorber.Visit(doc);

            // Iterate over each found image placement
            foreach (ImagePlacement placement in absorber.ImagePlacements)
            {
                // Image resolution is given in DPI (dots per inch) for X and Y axes
                // If either axis is lower than 72 DPI, hide (delete) the image from its page
                if (placement.Resolution.X < 72 || placement.Resolution.Y < 72)
                {
                    placement.Hide();
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
