using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_output.pdf";

        // Define a lookup table for border colors
        var borderColors = new Dictionary<string, Color>
        {
            { "rect", Color.FromRgb(1, 0, 0) },    // Red
            { "ellipse", Color.FromRgb(0, 1, 0) }, // Green
            { "line", Color.FromRgb(0, 0, 1) }     // Blue
        };

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height) – use double literals as required
            Graph graph = new Graph(400.0, 200.0)
            {
                Left = 50.0,
                Top = 500.0
            };

            // ---- Rectangle shape ----
            // Drawing.Rectangle expects float parameters
            var rectShape = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 150f, 80f);
            rectShape.GraphInfo = new GraphInfo
            {
                Color = borderColors["rect"],
                FillColor = Color.FromRgb(0.9, 0.9, 0.9),
                LineWidth = 2f
            };
            graph.Shapes.Add(rectShape);

            // ---- Ellipse shape ----
            var ellipseShape = new Ellipse(200f, 0f, 150f, 80f);
            ellipseShape.GraphInfo = new GraphInfo
            {
                Color = borderColors["ellipse"],
                FillColor = Color.FromRgb(0.9, 0.9, 0.9),
                LineWidth = 2f
            };
            graph.Shapes.Add(ellipseShape);

            // ---- Line shape ----
            float[] linePoints = { 0f, 120f, 350f, 120f };
            var lineShape = new Line(linePoints);
            lineShape.GraphInfo = new GraphInfo
            {
                Color = borderColors["line"],
                LineWidth = 2f
            };
            graph.Shapes.Add(lineShape);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with graph saved to '{outputPath}'.");
    }
}
