using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "circle_gradient.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (var doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a Graph object that will contain the circle.
            // Use the double‑precision constructor (the float overload is obsolete).
            var graph = new Graph(200.0, 200.0)
            {
                // Position the graph on the page (lower‑left corner of the graph)
                Left = 100,
                Top  = 500,
                // Optional: no border around the graph area
                Border = new BorderInfo(BorderSide.All, 0f, Color.Transparent)
            };

            // Create a circle shape. Parameters: centerX, centerY, radius (all float, graph‑local coordinates)
            var circle = new Circle(100f, 100f, 75f);

            // Apply a radial gradient fill from blue (center) to green (edge)
            circle.GraphInfo.FillColor = new Color
            {
                PatternColorSpace = new GradientRadialShading(Color.Blue, Color.Green)
                {
                    // The gradient is centered on the circle
                    Start = new Point(100f, 100f),
                    End   = new Point(100f, 100f),
                    StartingRadius = 0f,
                    EndingRadius   = 75f
                }
            };

            // Optional: thin black outline for the circle
            circle.GraphInfo.Color = Color.Black;
            circle.GraphInfo.LineWidth = 1f;

            // Add the circle to the graph's shape collection
            graph.Shapes.Add(circle);

            // Add the graph (which now contains the circle) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with gradient circle saved to '{outputPath}'.");
    }
}
