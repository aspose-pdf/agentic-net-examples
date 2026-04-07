using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Output PDF file path
        const string outputPath = "graph_output.pdf";

        // Lookup table mapping shape index to border color
        var borderColors = new Dictionary<int, Color>
        {
            { 0, Color.Red },
            { 1, Color.Green },
            { 2, Color.Blue },
            { 3, Color.Orange },
            { 4, Color.Purple }
        };

        // Create a new PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 500 points, height: 300 points)
            Graph graph = new Graph(500, 300)
            {
                // Position the graph on the page (optional)
                Left = 50,
                Top = 400
            };

            // Shape 0: Rectangle with border color from lookup table
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(0, 0, 200, 100);
            rectShape.GraphInfo = new GraphInfo
            {
                Color = borderColors[0],          // Border color
                FillColor = Color.LightGray,      // Optional fill color
                LineWidth = 2
            };
            graph.Shapes.Add(rectShape);

            // Shape 1: Ellipse with border color from lookup table
            Aspose.Pdf.Drawing.Ellipse ellipseShape = new Aspose.Pdf.Drawing.Ellipse(250, 0, 150, 100);
            ellipseShape.GraphInfo = new GraphInfo
            {
                Color = borderColors[1],
                FillColor = Color.LightYellow,
                LineWidth = 2
            };
            graph.Shapes.Add(ellipseShape);

            // Shape 2: Line with border color from lookup table
            float[] linePoints = { 0, 150, 400, 150 };
            Aspose.Pdf.Drawing.Line lineShape = new Aspose.Pdf.Drawing.Line(linePoints);
            lineShape.GraphInfo = new GraphInfo
            {
                Color = borderColors[2],
                LineWidth = 3
            };
            graph.Shapes.Add(lineShape);

            // Shape 3: Second rectangle with a different border color
            Aspose.Pdf.Drawing.Rectangle rectShape2 = new Aspose.Pdf.Drawing.Rectangle(0, 200, 120, 80);
            rectShape2.GraphInfo = new GraphInfo
            {
                Color = borderColors[3],
                FillColor = Color.LightGreen,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(rectShape2);

            // Shape 4: Second ellipse with a different border color
            Aspose.Pdf.Drawing.Ellipse ellipseShape2 = new Aspose.Pdf.Drawing.Ellipse(250, 200, 120, 80);
            ellipseShape2.GraphInfo = new GraphInfo
            {
                Color = borderColors[4],
                FillColor = Color.LightCoral,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(ellipseShape2);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with graph saved to '{outputPath}'.");
    }
}