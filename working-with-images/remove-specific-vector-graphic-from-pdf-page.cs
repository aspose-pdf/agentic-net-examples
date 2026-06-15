using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector; // GraphicsAbsorber and related types

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

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Choose the page to process (Aspose.Pdf uses 1‑based indexing)
            int pageNumber = 1;
            if (pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine("Requested page number exceeds page count.");
                return;
            }

            Page page = doc.Pages[pageNumber];

            // Verify that the page actually contains vector graphics
            if (!page.HasVectorGraphics())
            {
                Console.WriteLine("No vector graphics found on the selected page.");
                doc.Save(outputPath);
                return;
            }

            // Absorb all vector graphics on the page using the correct absorber class
            GraphicsAbsorber absorber = new GraphicsAbsorber();
            absorber.Visit(page);

            // Ensure there is at least one vector graphic to remove
            if (absorber.Elements.Count == 0)
            {
                Console.WriteLine("Graphics absorber returned an empty collection.");
                doc.Save(outputPath);
                return;
            }

            // Example logic: remove the first vector graphic.
            // Replace this with custom criteria (e.g., bounding rectangle) as needed.
            var graphicToRemove = absorber.Elements[0];
            graphicToRemove.Remove(); // Removes the element from the page

            // Save the modified PDF
            doc.Save(outputPath);
            Console.WriteLine($"Specified vector graphic removed. Saved to '{outputPath}'.");
        }
    }
}
