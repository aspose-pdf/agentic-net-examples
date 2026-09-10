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

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 500pt, height: 400pt)
            Graph graph = new Graph(500, 400)
            {
                // Position the graph on the page (optional)
                Left = 50,
                Top = 500
            };

            // Lookup table: shape index -> border color
            Dictionary<int, Color> borderColors = new Dictionary<int, Color>
            {
                { 0, Color.FromRgb(1, 0, 0) },       // Red
                { 1, Color.FromRgb(0, 1, 0) },       // Green
                { 2, Color.FromRgb(0, 0, 1) },       // Blue
                { 3, Color.FromRgb(1, 0.5, 0) }      // Orange
            };

            // Rectangle shape (index 0)
            Aspose.Pdf.Drawing.Rectangle rect1 = new Aspose.Pdf.Drawing.Rectangle(0, 0, 150, 100);
            rect1.GraphInfo = new GraphInfo
            {
                Color = borderColors[0],                     // Border color from lookup
                LineWidth = 2,
                FillColor = Color.FromRgb(0.9, 0.9, 0.9)     // Light gray fill
            };
            graph.Shapes.Add(rect1);

            // Ellipse shape (index 1)
            Ellipse ellipse = new Ellipse(200, 0, 150, 100);
            ellipse.GraphInfo = new GraphInfo
            {
                Color = borderColors[1],
                LineWidth = 2,
                FillColor = Color.FromRgb(0.9, 0.9, 0.9)
            };
            graph.Shapes.Add(ellipse);

            // Line shape (index 2)
            float[] linePoints = { 0, 150, 300, 150 };
            Line line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = borderColors[2],
                LineWidth = 3
            };
            graph.Shapes.Add(line);

            // Second rectangle shape (index 3)
            Aspose.Pdf.Drawing.Rectangle rect2 = new Aspose.Pdf.Drawing.Rectangle(0, 200, 150, 100);
            rect2.GraphInfo = new GraphInfo
            {
                Color = borderColors[3],
                LineWidth = 2,
                FillColor = Color.FromRgb(0.9, 0.9, 0.9)
            };
            graph.Shapes.Add(rect2);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with graph saved to '{outputPath}'.");
    }
}