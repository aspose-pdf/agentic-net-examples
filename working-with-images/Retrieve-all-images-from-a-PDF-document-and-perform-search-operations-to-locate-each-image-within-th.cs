using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPath = "input.pdf";

        // Verify that the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create an absorber that will search for image placements in the document
            ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();

            // Perform the search on the whole document (all pages)
            absorber.Visit(doc);

            // Iterate over each found image placement and output its location details
            int imageIndex = 1;
            foreach (ImagePlacement placement in absorber.ImagePlacements)
            {
                Console.WriteLine($"Image #{imageIndex}:");
                Console.WriteLine($"  Page number          : {placement.Page.Number}");
                Console.WriteLine($"  Rectangle (LLX, LLY): {placement.Rectangle.LLX}, {placement.Rectangle.LLY}");
                Console.WriteLine($"  Width / Height       : {placement.Rectangle.Width} x {placement.Rectangle.Height}");
                Console.WriteLine($"  Resolution (X / Y)   : {placement.Resolution.X} / {placement.Resolution.Y}");
                Console.WriteLine($"  Rotation (degrees)   : {placement.Rotation}");
                Console.WriteLine();

                imageIndex++;
            }
        }
    }
}