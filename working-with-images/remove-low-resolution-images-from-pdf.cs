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
            // Create an absorber that will collect all image placements in the document
            ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();

            // Perform the search on the whole document
            absorber.Visit(doc);

            // Iterate over each found image placement
            foreach (ImagePlacement placement in absorber.ImagePlacements)
            {
                // Check the horizontal and vertical resolution (DPI)
                if (placement.Resolution.X < 72 || placement.Resolution.Y < 72)
                {
                    // Remove the image from the page
                    placement.Hide();
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Images with DPI < 72 removed. Output saved to '{outputPath}'.");
    }
}