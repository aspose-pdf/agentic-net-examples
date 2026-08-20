using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "LineGraph.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 500 points, height: 300 points)
            Graph graph = new Graph(500, 300);

            // Position the graph on the page (optional: set margins)
            graph.Left = 50;
            graph.Top = 500; // Y coordinate from bottom

            // ------------------------------
            // Series 1 – Solid red line
            // ------------------------------
            // Define points for the line (x1, y1, x2, y2, ...)
            float[] series1Points = { 0, 0, 100, 80, 200, 120, 300, 180, 400, 150 };
            Line line1 = new Line(series1Points);
            line1.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 2f,
                // Solid line – no dash array needed
            };
            graph.Shapes.Add(line1);

            // ------------------------------
            // Series 2 – Dashed green line
            // ------------------------------
            float[] series2Points = { 0, 0, 100, 60, 200, 100, 300, 140, 400, 110 };
            Line line2 = new Line(series2Points);
            line2.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Green,
                LineWidth = 2f,
                // Dash pattern: 6 points on, 4 points off
                DashArray = new int[] { 6, 4 }
            };
            graph.Shapes.Add(line2);

            // ------------------------------
            // Series 3 – Dotted blue line
            // ------------------------------
            float[] series3Points = { 0, 0, 100, 40, 200, 80, 300, 100, 400, 70 };
            Line line3 = new Line(series3Points);
            line3.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,
                LineWidth = 2f,
                // Dotted pattern: 2 points on, 2 points off
                DashArray = new int[] { 2, 2 }
            };
            graph.Shapes.Add(line3);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Line graph PDF saved to '{outputPath}'.");
    }
}