using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create the absorber that will collect image placement information
            ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();

            // Iterate over all pages (1‑based indexing) and let the absorber visit each page
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.Accept(absorber);
            }

            // Output the collected image placement details
            foreach (ImagePlacement placement in absorber.ImagePlacements)
            {
                // The rectangle gives the visible position and size of the image on the page
                Aspose.Pdf.Rectangle rect = placement.Rectangle;

                Console.WriteLine($"Page: {placement.Page.Number}");
                Console.WriteLine($"  LLX: {rect.LLX}, LLY: {rect.LLY}");
                Console.WriteLine($"  Width: {rect.Width}, Height: {rect.Height}");
                Console.WriteLine($"  Resolution X: {placement.Resolution.X}, Y: {placement.Resolution.Y}");
                Console.WriteLine($"  Rotation: {placement.Rotation} degrees");
                Console.WriteLine();
            }
        }
    }
}