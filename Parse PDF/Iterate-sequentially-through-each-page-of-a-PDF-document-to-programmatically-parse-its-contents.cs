using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count; // Pages are 1‑based
            Console.WriteLine($"Document contains {pageCount} pages.");

            // Iterate sequentially through each page
            for (int i = 1; i <= pageCount; i++)
            {
                Page page = doc.Pages[i];

                // ----- Extract text from the current page -----
                TextAbsorber textAbsorber = new TextAbsorber();
                textAbsorber.Visit(page);               // Perform search on this page
                string pageText = textAbsorber.Text ?? string.Empty;
                Console.WriteLine($"--- Text on page {i} ---");
                Console.WriteLine(pageText);

                // ----- Extract image placements from the current page -----
                ImagePlacementAbsorber imageAbsorber = new ImagePlacementAbsorber();
                imageAbsorber.Visit(page);               // Perform search on this page
                int imageCount = imageAbsorber.ImagePlacements.Count;
                Console.WriteLine($"Found {imageCount} image(s) on page {i}.");

                // Example: iterate over each image placement (optional processing)
                // foreach (var placement in imageAbsorber.ImagePlacements)
                // {
                //     // Access placement properties such as placement.Rectangle, placement.Image, etc.
                // }
            }
        }
    }
}