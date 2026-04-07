using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "ArcWithRadialGradient.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width = 400, height = 200)
            Graph graph = new Graph(400, 200);
            page.Paragraphs.Add(graph);

            // Define arc parameters
            float centerX = 200f;   // X coordinate of the arc center
            float centerY = 100f;   // Y coordinate of the arc center
            float radius  = 80f;    // Radius of the arc
            float startAngle = 0f;  // Beginning angle in degrees
            float sweepAngle = 180f; // Sweep angle (ending angle = start + sweep)

            // Create the arc shape
            Arc arc = new Arc(centerX, centerY, radius, startAngle, startAngle + sweepAngle);

            // Create a radial gradient shading (from LightBlue to LightGreen)
            GradientRadialShading radialShading = new GradientRadialShading(
                Aspose.Pdf.Color.LightBlue,
                Aspose.Pdf.Color.LightGreen);

            // Configure the gradient circles (both centered at the arc center)
            radialShading.Start = new Point(centerX, centerY);
            radialShading.End   = new Point(centerX, centerY);
            radialShading.StartingRadius = 0;          // Inner circle radius
            radialShading.EndingRadius   = radius;    // Outer circle radius

            // Apply the gradient as the fill color of the arc via GraphInfo
            arc.GraphInfo = new GraphInfo
            {
                // FillColor uses a PatternColorSpace that holds the radial shading
                FillColor = new Color { PatternColorSpace = radialShading },
                // Optional: set a stroke color and line width
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1
            };

            // Add the arc to the graph
            graph.Shapes.Add(arc);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with filled arc saved to '{outputPath}'.");
    }
}