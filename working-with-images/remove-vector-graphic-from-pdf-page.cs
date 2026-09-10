using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Assume we want to modify the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create an absorber to find all vector graphics on the page
            GraphicsAbsorber absorber = new GraphicsAbsorber();
            absorber.Visit(page);

            // If no vector graphics are present, nothing to delete
            if (absorber.Elements.Count == 0)
            {
                Console.WriteLine("No vector graphics found on the page.");
            }
            else
            {
                // Example: delete the first vector graphic found.
                // Replace this logic with your own criteria (e.g., bounding box, operator count, etc.).
                var target = absorber.Elements[0];

                // Remove the graphic from the page
                target.Remove();

                Console.WriteLine("Specified vector graphic has been removed.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
