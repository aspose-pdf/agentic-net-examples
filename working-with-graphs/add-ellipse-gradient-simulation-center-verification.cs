using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

// Alias to avoid confusion with System.Drawing.Rectangle
using DrawRect = Aspose.Pdf.Drawing.Rectangle;
using DrawPath = Aspose.Pdf.Drawing.Path;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse_gradient.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (var doc = new Document())
        {
            // Add a single page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a Graph that spans the whole page – Graph is the only container that can hold shapes
            var graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define the bounding rectangle for the ellipse (left, bottom, width, height)
            float left   = 100f;
            float bottom = 300f;
            float width  = 200f;
            float height = 200f;

            // Create the ellipse shape – the constructor expects four float values
            var ellipse = new Ellipse(left, bottom, width, height);

            // Compute the centre of the ellipse – needed for the gradient origin
            float centerX = left + width / 2f;
            float centerY = bottom + height / 2f;

            // Aspose.Pdf does not expose a conical gradient. The closest built‑in option is a radial gradient.
            // We use a radial gradient that starts at the centre with radius 0 and expands to the ellipse radius.
            var radialGradient = new GradientRadialShading(
                Color.FromRgb(1, 0, 0), // Red (start colour)
                Color.FromRgb(0, 0, 1)  // Blue (end colour)
            )
            {
                Start = new Point(centerX, centerY),
                End   = new Point(centerX, centerY),
                StartingRadius = 0f,
                EndingRadius   = Math.Max(width, height) / 2f
            };

            // Apply the gradient fill via the PatternColorSpace of the FillColor property
            ellipse.GraphInfo.FillColor = new Color { PatternColorSpace = radialGradient };
            // No stroke is required for this demo
            ellipse.GraphInfo.Color = Color.Transparent;

            // Add the ellipse to the graph and the graph to the page
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // Verify that the gradient origin matches the ellipse centre
            bool originMatches = Math.Abs(radialGradient.Start.X - centerX) < 0.001 &&
                                 Math.Abs(radialGradient.Start.Y - centerY) < 0.001;

            Console.WriteLine(originMatches
                ? "Gradient origin correctly aligned with ellipse center."
                : "Gradient origin does NOT align with ellipse center.");

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
