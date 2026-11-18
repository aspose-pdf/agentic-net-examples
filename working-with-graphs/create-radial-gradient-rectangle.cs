using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

// Alias the drawing Rectangle to avoid ambiguity with Aspose.Pdf.Rectangle (used for annotations)
using DrawRect = Aspose.Pdf.Drawing.Rectangle;

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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a Graph that covers the whole page – shapes must be added to a Graph, not directly to the page
            var graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Rectangle bounds: left = 100, bottom = 500, width = 200, height = 200
            var rectShape = new DrawRect(100, 500, 200, 200);

            // Define a radial gradient: opaque red at the centre, fully transparent at the edges
            var radialShading = new GradientRadialShading(
                Color.FromArgb(255, 255, 0, 0), // opaque red (A=255)
                Color.FromArgb(0,   255, 0, 0)  // transparent red (A=0)
            )
            {
                // Center of the rectangle (200, 600)
                Start = new Point(200, 600),
                End   = new Point(200, 600),
                StartingRadius = 0,
                EndingRadius   = 100 // half of the rectangle's smaller side
            };

            // Apply the gradient as the fill colour of the rectangle
            rectShape.GraphInfo.FillColor = new Color { PatternColorSpace = radialShading };
            // No stroke (border) for the rectangle
            rectShape.GraphInfo.Color = Color.Transparent;

            // Add the rectangle to the graph and the graph to the page
            graph.Shapes.Add(rectShape);
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rectangle with radial gradient saved to '{outputPath}'.");
    }
}
