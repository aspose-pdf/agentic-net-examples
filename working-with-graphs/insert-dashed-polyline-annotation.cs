using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "polyline_graph.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Create a Graph (size only). Position is set via Left/Top properties.
            Graph graph = new Graph(500f, 300f); // width, height
            graph.Left = 50f;
            graph.Top = 400f;
            page.Paragraphs.Add(graph);

            // Define a polyline using a Line shape (multiple points)
            // Points: (0,0) → (100,50) → (200,-30) → (300,80) → (400,0)
            float[] points = new float[] { 0f, 0f, 100f, 50f, 200f, -30f, 300f, 80f, 400f, 0f };
            Line polyline = new Line(points);

            // Configure line appearance: width, color, dashed pattern
            polyline.GraphInfo.LineWidth = 2f;
            polyline.GraphInfo.Color = Aspose.Pdf.Color.Black;
            // Dash pattern: 5 units on, 3 units off
            polyline.GraphInfo.DashArray = new int[] { 5, 3 };

            // Add the polyline to the graph
            graph.Shapes.Add(polyline);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
