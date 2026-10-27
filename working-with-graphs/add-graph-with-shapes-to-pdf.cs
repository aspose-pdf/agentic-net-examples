using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text; // Required for TextFragment, TextState, FontStyles, etc.

// Alias ambiguous Rectangle types for clarity
using PdfRect = Aspose.Pdf.Rectangle;          // page/annotation coordinates
using ShapeRect = Aspose.Pdf.Drawing.Rectangle; // shape rectangle

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_graph.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Add a new blank page at the end of the document
            Page graphPage = doc.Pages.Add();

            // Create a Graph container that will hold all drawing primitives
            Graph graph = new Graph(graphPage.PageInfo.Width, graphPage.PageInfo.Height);

            // -------------------------------------------------
            // Draw X and Y axes
            // -------------------------------------------------
            // X‑axis
            Line xAxis = new Line(new float[] { 50, 500, 550, 500 });
            xAxis.GraphInfo.Color = Color.Black;
            xAxis.GraphInfo.LineWidth = 2;
            graph.Shapes.Add(xAxis);

            // Y‑axis
            Line yAxis = new Line(new float[] { 50, 100, 50, 500 });
            yAxis.GraphInfo.Color = Color.Black;
            yAxis.GraphInfo.LineWidth = 2;
            graph.Shapes.Add(yAxis);

            // -------------------------------------------------
            // Plot sample data points and connect them with lines
            // -------------------------------------------------
            // Sample data (X, Y) in page coordinates
            var points = new (float X, float Y)[]
            {
                (100, 150),
                (200, 250),
                (300, 200),
                (400, 350),
                (500, 300)
            };

            // Draw circles for each data point
            foreach (var (x, y) in points)
            {
                // Circle is an Ellipse with equal width/height
                Ellipse circle = new Ellipse(x - 5, y - 5, x + 5, y + 5);
                circle.GraphInfo.Color = Color.Blue;          // stroke color
                circle.GraphInfo.FillColor = Color.LightBlue; // fill color
                circle.GraphInfo.LineWidth = 1;
                graph.Shapes.Add(circle);
            }

            // Connect points with lines
            for (int i = 0; i < points.Length - 1; i++)
            {
                var (x1, y1) = points[i];
                var (x2, y2) = points[i + 1];
                Line segment = new Line(new float[] { x1, y1, x2, y2 });
                segment.GraphInfo.Color = Color.Red;
                segment.GraphInfo.LineWidth = 1.5f;
                graph.Shapes.Add(segment);
            }

            // Add the completed graph to the page
            graphPage.Paragraphs.Add(graph);

            // -------------------------------------------------
            // Add a title using a TextFragment
            // -------------------------------------------------
            TextFragment title = new TextFragment("Sample Graph");
            title.Position = new Position(200, 550);
            // Modify the existing TextState instance instead of assigning a new one
            title.TextState.FontSize = 18;
            title.TextState.FontStyle = FontStyles.Bold;
            title.TextState.ForegroundColor = Color.DarkGreen;
            graphPage.Paragraphs.Add(title);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graph added and saved to '{outputPath}'.");
    }
}
