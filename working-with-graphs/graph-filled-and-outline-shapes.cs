using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height) – size in points (double literals required)
            Graph graph = new Graph(500.0, 300.0);

            // -------------------------------------------------
            // Filled rectangle (stroke + fill)
            // -------------------------------------------------
            // Parameters: left, bottom, width, height (float literals required)
            Aspose.Pdf.Drawing.Rectangle filledRect = new Aspose.Pdf.Drawing.Rectangle(50f, 150f, 150f, 100f);
            // Set visual properties via GraphInfo
            filledRect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightBlue,   // Fill color
                Color = Aspose.Pdf.Color.DarkBlue,       // Stroke color
                LineWidth = 2f                            // Stroke thickness (float)
            };
            // Add the shape to the graph
            graph.Shapes.Add(filledRect);

            // -------------------------------------------------
            // Outline‑only rectangle (stroke without fill)
            // -------------------------------------------------
            Aspose.Pdf.Drawing.Rectangle outlineRect = new Aspose.Pdf.Drawing.Rectangle(250f, 150f, 150f, 100f);
            // Only set stroke properties; omit FillColor for no fill
            outlineRect.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Red,   // Stroke color
                LineWidth = 3f                  // Stroke thickness (float)
                // No FillColor -> shape will be unfilled (stroke‑only)
            };
            graph.Shapes.Add(outlineRect);

            // -------------------------------------------------
            // Optional: a simple line to demonstrate stroke rendering
            // -------------------------------------------------
            float[] linePoints = { 50f, 50f, 400f, 50f }; // x1, y1, x2, y2 (float literals)
            Line line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Green,
                LineWidth = 1.5f
            };
            graph.Shapes.Add(line);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF to a file
            string outputPath = "Graph_StrokeAndFill.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
    }
}