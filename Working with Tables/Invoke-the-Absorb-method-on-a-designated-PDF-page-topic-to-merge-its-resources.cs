using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page to process (1‑based indexing)
            const int pageNumber = 1; // change as needed
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Invalid page number: {pageNumber}");
                return;
            }

            Page page = doc.Pages[pageNumber];

            // Create a GraphicsAbsorber to absorb (collect) graphics resources on the page
            GraphicsAbsorber graphicsAbsorber = new GraphicsAbsorber();

            // Invoke the Absorb (Visit) method on the designated page
            graphicsAbsorber.Visit(page);

            // At this point the absorber has gathered the page's graphics resources.
            // If the goal is to merge (consolidate) resources, we can call OptimizeResources.
            doc.OptimizeResources();

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed page saved to '{outputPath}'.");
    }
}