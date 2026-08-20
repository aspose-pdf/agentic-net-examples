using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "collage.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // ---------- First graph: rectangle ----------
            // Width = 150, Height = 100 (use double overload for Graph)
            Graph graph1 = new Graph(150.0, 100.0);
            // Position the graph on the page (double values)
            graph1.Left = 50.0;   // X coordinate (points from left)
            graph1.Top  = 700.0;  // Y coordinate (points from bottom)

            // Create a rectangle shape inside the graph (drawing rectangle, not PDF rectangle)
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 150f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightBlue,
                Color     = Color.DarkBlue,
                LineWidth = 2f
            };
            graph1.Shapes.Add(rect);
            // Add the graph to the page
            page.Paragraphs.Add(graph1);

            // ---------- Second graph: ellipse ----------
            Graph graph2 = new Graph(120.0, 120.0);
            graph2.Left = 250.0;
            graph2.Top  = 650.0;

            var ellipse = new Ellipse(0f, 0f, 120f, 120f);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGreen,
                Color     = Color.DarkGreen,
                LineWidth = 2f
            };
            graph2.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph2);

            // ---------- Third graph: line ----------
            Graph graph3 = new Graph(200.0, 50.0);
            graph3.Left = 100.0;
            graph3.Top  = 500.0;

            // Line constructor expects a float array {x1, y1, x2, y2}
            float[] linePoints = { 0f, 0f, 200f, 0f };
            Line line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color     = Color.Red,
                LineWidth = 3f
            };
            graph3.Shapes.Add(line);
            page.Paragraphs.Add(graph3);

            // Save the PDF with the collage of graphs
            doc.Save(outputPath);
        }

        Console.WriteLine($"Collage PDF saved to '{outputPath}'.");
    }
}
