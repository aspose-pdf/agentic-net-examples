using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 500pt, height: 300pt)
            Graph graph = new Graph(500, 300);

            // ------------------------------
            // Series 1 – Solid red line
            // ------------------------------
            float[] points1 = { 50, 250, 450, 250 }; // horizontal line
            Line line1 = new Line(points1);
            line1.GraphInfo = new GraphInfo
            {
                Color = Color.Red,
                LineWidth = 2f
                // No DashArray => solid line
            };
            graph.Shapes.Add(line1);

            // ------------------------------
            // Series 2 – Dashed green line
            // ------------------------------
            float[] points2 = { 50, 200, 450, 200 };
            Line line2 = new Line(points2);
            line2.GraphInfo = new GraphInfo
            {
                Color = Color.Green,
                LineWidth = 2f,
                // Dash pattern: 5 units on, 3 units off
                DashArray = new int[] { 5, 3 }
            };
            graph.Shapes.Add(line2);

            // ------------------------------
            // Series 3 – Dotted blue line
            // ------------------------------
            float[] points3 = { 50, 150, 450, 150 };
            Line line3 = new Line(points3);
            line3.GraphInfo = new GraphInfo
            {
                Color = Color.Blue,
                LineWidth = 2f,
                // Dash pattern: 1 unit on, 2 units off (creates a dotted effect)
                DashArray = new int[] { 1, 2 }
            };
            graph.Shapes.Add(line3);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save("LineGraph.pdf");
        }

        Console.WriteLine("Line graph PDF created: LineGraph.pdf");
    }
}