using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block to ensure proper disposal.
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (Aspose.Pdf uses 1‑based indexing).
            Page page = doc.Pages[1];

            // Create a Graph that covers the whole page.
            // Graph constructor expects width and height as double.
            Graph graph = new Graph(page.MediaBox.Width, page.MediaBox.Height);

            // Enable bounds checking so that adding a shape that does not fit
            // throws BoundsOutOfRangeException. The container size is the page size.
            graph.Shapes.UpdateBoundsCheckMode(
                BoundsCheckMode.ThrowExceptionIfDoesNotFit,
                page.MediaBox.Width,
                page.MediaBox.Height);

            // Example shapes – some fit, some exceed the page bounds.
            var shapes = new Shape[]
            {
                // Fits inside the page.
                new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 200f, 100f),

                // Exceeds the right edge.
                new Aspose.Pdf.Drawing.Rectangle(500f, 100f, 200f, 100f),

                // Exceeds the top edge.
                new Aspose.Pdf.Drawing.Rectangle(100f, 800f, 150f, 200f)
            };

            foreach (var shape in shapes)
            {
                try
                {
                    // Attempt to add the shape; out‑of‑bounds shapes will throw.
                    graph.Shapes.Add(shape);
                }
                catch (BoundsOutOfRangeException ex)
                {
                    // Log the offending shape's coordinates.
                    if (shape is Aspose.Pdf.Drawing.Rectangle rect)
                    {
                        Console.WriteLine(
                            $"Shape out of bounds: Left={rect.Left}, Bottom={rect.Bottom}, " +
                            $"Width={rect.Width}, Height={rect.Height}. Message: {ex.Message}");
                    }
                    else
                    {
                        Console.WriteLine($"Shape out of bounds: {ex.Message}");
                    }
                }
            }

            // Add the graph (with the successfully added shapes) to the page.
            page.Paragraphs.Add(graph);

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}