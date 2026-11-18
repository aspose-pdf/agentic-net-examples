using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "arc_graph.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the area for the graph (left‑lower‑x, left‑lower‑y, upper‑right‑x, upper‑right‑y)
            var graphRect = new Aspose.Pdf.Rectangle(100, 500, 400, 200);

            // Graph size is the rectangle size
            double graphWidth = graphRect.URX - graphRect.LLX;
            double graphHeight = graphRect.URY - graphRect.LLY;

            // Create the Graph container and position it
            var graph = new Graph(graphWidth, graphHeight)
            {
                Left = (float)graphRect.LLX,
                Top = (float)graphRect.LLY
            };

            // Arc parameters (degrees)
            double startAngle = 45;   // start at 45°
            double sweepAngle = 270;  // sweep 270° clockwise

            // Bounding rectangle for the arc (can be smaller than the graph area)
            var arcRect = new Aspose.Pdf.Rectangle(120, 480, 380, 220);
            float centerX = (float)((arcRect.LLX + arcRect.URX) / 2);
            float centerY = (float)((arcRect.LLY + arcRect.URY) / 2);
            float radius = (float)Math.Min((arcRect.URX - arcRect.LLX) / 2, (arcRect.URY - arcRect.LLY) / 2);

            // Create the arc shape: Arc(centerX, centerY, radius, startAngle, sweepAngle)
            var arc = new Arc(centerX, centerY, radius, (float)startAngle, (float)sweepAngle);

            // Radial gradient fill (blue → light blue)
            var gradientFill = new Color
            {
                PatternColorSpace = new GradientRadialShading(
                    Color.FromRgb(0, 0, 1),          // inner color (blue)
                    Color.FromRgb(0.5f, 0.5f, 1)     // outer color (light blue)
                )
                {
                    Start = new Point(centerX, centerY),
                    End = new Point(centerX, centerY),
                    StartingRadius = 0,
                    EndingRadius = radius
                }
            };
            arc.GraphInfo.FillColor = gradientFill;

            // Stroke (outline) properties
            arc.GraphInfo.Color = Color.Black;
            arc.GraphInfo.LineWidth = 1;

            // Add the arc to the graph and the graph to the page
            graph.Shapes.Add(arc);
            page.Paragraphs.Add(graph);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
