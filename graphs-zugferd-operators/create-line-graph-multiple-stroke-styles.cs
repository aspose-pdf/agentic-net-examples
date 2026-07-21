using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths for input (optional) and output PDF
        const string outputPath = "line_graph.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the size of the graph (width, height in points)
            double graphWidth = 500;
            double graphHeight = 300;

            // Create a Graph container that will hold the line series
            Graph graph = new Graph(graphWidth, graphHeight);

            // Position the graph on the page (optional: set margins)
            graph.Left = 50;   // distance from left edge of the page
            graph.Top = 400;   // distance from bottom edge of the page

            // ------------------------------
            // Series 1 – Solid line (red)
            // ------------------------------
            // Points for the first series (x1, y1, x2, y2, ... )
            float[] series1Points = { 0, 0, 100, 80, 200, 60, 300, 120, 400, 90, 500, 150 };
            Line series1 = new Line(series1Points);
            series1.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 2f
                // No DashArray → solid line
            };
            graph.Shapes.Add(series1);

            // ------------------------------
            // Series 2 – Dashed line (green)
            // ------------------------------
            float[] series2Points = { 0, 0, 100, 120, 200, 100, 300, 180, 400, 150, 500, 210 };
            Line series2 = new Line(series2Points);
            series2.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Green,
                LineWidth = 2f,
                // Dash pattern: 6 points on, 4 points off
                DashArray = new int[] { 6, 4 }
            };
            graph.Shapes.Add(series2);

            // ------------------------------
            // Series 3 – Dotted line (blue)
            // ------------------------------
            float[] series3Points = { 0, 0, 100, 60, 200, 40, 300, 100, 400, 70, 500, 130 };
            Line series3 = new Line(series3Points);
            series3.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,
                LineWidth = 2f,
                // Dotted pattern: short dash, short gap
                DashArray = new int[] { 1, 2 }
            };
            graph.Shapes.Add(series3);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Line graph saved to '{outputPath}'.");
    }
}