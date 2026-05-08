using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;
using Aspose.Pdf.Text;

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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Check if the page contains any vector graphics
            if (!page.HasVectorGraphics())
            {
                Console.WriteLine("No vector graphics found on the first page.");
                doc.Save(outputPath);
                return;
            }

            // -----------------------------------------------------------------
            // Extract vector graphics from the page.
            // The correct absorber class is GraphicsAbsorber (not VectorGraphicsAbsorber).
            // -----------------------------------------------------------------
            GraphicsAbsorber absorber = new GraphicsAbsorber();
            absorber.Visit(page);

            // Ensure at least one graphic element was extracted
            if (absorber.Elements.Count == 0)
            {
                Console.WriteLine("Vector graphics extraction returned no elements.");
                doc.Save(outputPath);
                return;
            }

            // Take the first extracted graphic element (e.g., a SubPath)
            GraphicElement graphic = absorber.Elements[0];

            // -----------------------------------------------------------------
            // Move the graphic to new coordinates.
            // -----------------------------------------------------------------
            double deltaX = 100.0; // shift right
            double deltaY = 50.0;  // shift up

            // Current position of the graphic
            Point currentPos = graphic.Position;

            // Compute new position
            Point newPos = new Point(currentPos.X + deltaX, currentPos.Y + deltaY);

            // Apply the new position
            graphic.Position = newPos;

            // -----------------------------------------------------------------
            // Remove the original graphic from the page and add the modified one.
            // -----------------------------------------------------------------
            graphic.Remove();          // Remove original instance
            graphic.AddOnPage(page);   // Add the modified instance back

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Vector graphic moved and saved to '{outputPath}'.");
    }
}
