using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_moved.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Verify that the page contains vector graphics
            if (!page.HasVectorGraphics())
            {
                Console.WriteLine("No vector graphics found on the page.");
                doc.Save(outputPath);
                return;
            }

            // Extract vector graphics from the page using the correct absorber class
            GraphicsAbsorber absorber = new GraphicsAbsorber();
            absorber.Visit(page);

            // Ensure at least one graphic element was found
            if (absorber.Elements.Count == 0)
            {
                Console.WriteLine("No vector graphics extracted.");
                doc.Save(outputPath);
                return;
            }

            // Select the first graphic element as the element to move
            var graphic = absorber.Elements[0];

            // Remove the original element from the page
            graphic.Remove();

            // Define translation offsets (e.g., move 50 points right and 30 points up)
            double offsetX = 50.0;
            double offsetY = 30.0;

            // Update the element's position; this internally updates its Matrix
            Aspose.Pdf.Point currentPos = graphic.Position;
            graphic.Position = new Aspose.Pdf.Point(currentPos.X + offsetX, currentPos.Y + offsetY);

            // Add the modified element back onto the same page
            graphic.AddOnPage(page);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Vector graphic moved and saved to '{outputPath}'.");
    }
}
