using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Graph constructor now uses double literals as required by the API
            Graph graph = new Graph(400.0, 200.0);

            // Create a circle shape (center at (200,100), radius 50)
            Circle circle = new Circle(200f, 100f, 50f);

            // Define a radial gradient from red to blue
            GradientRadialShading radialGradient = new GradientRadialShading(
                Color.FromRgb(1.0, 0.0, 0.0),   // start color (red)
                Color.FromRgb(0.0, 0.0, 1.0)    // end color (blue)
            );

            // Use the gradient as a pattern fill color
            Color gradientColor = new Color { PatternColorSpace = radialGradient };
            circle.GraphInfo.FillColor = gradientColor;

            // Optional: set a stroke color and line width (float literal)
            circle.GraphInfo.Color = Color.Black;
            circle.GraphInfo.LineWidth = 1f;

            // Add the circle to the graph
            graph.Shapes.Add(circle);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            const string outputPath = "CircleWithGradient.pdf";

            // Guard Document.Save on platforms that lack GDI+ (e.g., macOS/Linux without libgdiplus)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("Skipping Document.Save() because GDI+ (libgdiplus) is not available on this platform.");
            }
        }
    }
}