using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "arc_gradient.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (size can be larger than the shape)
            // NOTE: Graph constructor expects double values
            Graph graph = new Graph(500.0, 500.0);

            // Create an Arc: center at (250,250), radius 100, start angle 0°, sweep to 180°
            Arc arc = new Arc(250, 250, 100, 0, 180);
            // Alpha and Beta are the start and sweep angles (optional – the constructor already sets them)
            arc.Alpha = 0;    // start angle
            arc.Beta = 180;   // sweep angle

            // Define a radial gradient from Yellow (center) to Green (edge)
            GradientRadialShading radial = new GradientRadialShading(Aspose.Pdf.Color.Yellow, Aspose.Pdf.Color.Green)
            {
                Start = new Point(250, 250),          // center of gradient
                End = new Point(250, 250),            // same center for radial gradient
                StartingRadius = 0,                   // inner radius
                EndingRadius = 100                    // outer radius
            };

            // Apply the gradient as the shading on the shape itself
            arc.Shading = radial; // <-- corrected: use Arc.Shading, not GraphInfo.Shading

            // Set outline (stroke) properties via GraphInfo
            arc.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Black, // outline color
                LineWidth = 2f                  // float literal for line width
            };

            // Add the arc to the graph
            graph.Shapes.Add(arc);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
