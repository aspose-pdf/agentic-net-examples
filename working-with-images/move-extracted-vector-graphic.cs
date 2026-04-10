using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

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
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Extract vector graphics from the page
            using (GraphicsAbsorber absorber = new GraphicsAbsorber())
            {
                absorber.Visit(page); // Populate absorber.Elements with graphic elements

                if (absorber.Elements.Count == 0)
                {
                    Console.WriteLine("No vector graphics found on the page.");
                }
                else
                {
                    // Take the first extracted graphic element
                    GraphicElement element = absorber.Elements[0];

                    // Desired translation (move right by 50 points, up by 30 points)
                    double dx = 50;
                    double dy = 30;

                    // The Matrix property is read‑only; to move the element we adjust its Position.
                    // Position is a Point (X, Y) in the current coordinate space.
                    Aspose.Pdf.Point currentPos = element.Position;
                    element.Position = new Aspose.Pdf.Point(currentPos.X + dx, currentPos.Y + dy);

                    // If you need the resulting matrix (for inspection or further calculations):
                    Aspose.Pdf.Matrix updatedMatrix = Aspose.Pdf.Matrix.Translate(dx, dy, element.Matrix);
                    // The updatedMatrix reflects the translation applied via Position.
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Vector graphic moved and saved to '{outputPath}'.");
    }
}