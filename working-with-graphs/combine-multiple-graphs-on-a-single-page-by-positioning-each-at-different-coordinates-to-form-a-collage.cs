using System;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // ---------- First graph: rectangle ----------
            // Graph dimensions (width, height) – use double literals as required by the new constructor
            Graph rectGraph = new Graph(200.0, 100.0);
            // Position the graph on the page (coordinates from bottom‑left)
            rectGraph.Left = 50;   // X coordinate
            rectGraph.Top  = 700;  // Y coordinate

            // Create a rectangle shape (positioned relative to the graph origin)
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color     = Color.Black,
                LineWidth = 2f
            };
            rectGraph.Shapes.Add(rect);
            page.Paragraphs.Add(rectGraph);

            // ---------- Second graph: line ----------
            Graph lineGraph = new Graph(150.0, 50.0);
            lineGraph.Left = 300;
            lineGraph.Top  = 600;

            // Line defined by an array of points {x1, y1, x2, y2}
            float[] linePoints = { 0f, 0f, 150f, 0f };
            Line line = new Line(linePoints);
            line.GraphInfo = new GraphInfo
            {
                Color     = Color.Red,
                LineWidth = 3f
            };
            lineGraph.Shapes.Add(line);
            page.Paragraphs.Add(lineGraph);

            // ---------- Third graph: ellipse ----------
            Graph ellipseGraph = new Graph(120.0, 80.0);
            ellipseGraph.Left = 150;
            ellipseGraph.Top  = 400;

            // Ellipse positioned at (0,0) within its graph container
            Ellipse ellipse = new Ellipse(0f, 0f, 120f, 80f);
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color     = Color.Blue,
                LineWidth = 1.5f
            };
            ellipseGraph.Shapes.Add(ellipse);
            page.Paragraphs.Add(ellipseGraph);

            // Save the resulting PDF containing the collage of graphs
            doc.Save("collage.pdf");
        }
    }
}
