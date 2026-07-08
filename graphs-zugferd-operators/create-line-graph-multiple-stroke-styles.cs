using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class LineGraphExample
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the size of the graph (width x height in points)
            double graphWidth = 500;
            double graphHeight = 300;

            // Create a Graph container
            Graph graph = new Graph(graphWidth, graphHeight);

            // Position the graph on the page (optional: set margins)
            graph.Left = 50;   // 50 points from the left edge
            graph.Top = 400;   // 400 points from the bottom edge

            // ------------------------------
            // Series 1 – Solid line (red)
            // ------------------------------
            // Example data points for series 1
            float[] series1Points = {
                0,   0,    // (x0, y0)
                100, 80,   // (x1, y1)
                200, 150,  // (x2, y2)
                300, 120,  // (x3, y3)
                400, 200   // (x4, y4)
            };

            Line lineSeries1 = new Line(series1Points);
            lineSeries1.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 2f,
                // Solid line – no dash array needed
            };
            graph.Shapes.Add(lineSeries1);

            // ------------------------------
            // Series 2 – Dashed line (green)
            // ------------------------------
            float[] series2Points = {
                0,   0,
                100, 60,
                200, 130,
                300, 100,
                400, 180
            };

            Line lineSeries2 = new Line(series2Points);
            lineSeries2.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Green,
                LineWidth = 2f,
                // Dash pattern: 6 points on, 4 points off
                DashArray = new int[] { 6, 4 }
            };
            graph.Shapes.Add(lineSeries2);

            // ------------------------------
            // Series 3 – Dotted line (blue)
            // ------------------------------
            float[] series3Points = {
                0,   0,
                100, 40,
                200, 110,
                300, 80,
                400, 160
            };

            Line lineSeries3 = new Line(series3Points);
            lineSeries3.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,
                LineWidth = 2f,
                // Dotted pattern: 1 point on, 3 points off
                DashArray = new int[] { 1, 3 }
            };
            graph.Shapes.Add(lineSeries3);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            string outputPath = "LineGraph.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF with line graph saved to '{outputPath}'.");
        }
    }
}