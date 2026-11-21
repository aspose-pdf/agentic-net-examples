using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths
        const string outputPath = "continuous_series.pdf";

        // Create a new PDF document inside a using block (ensures disposal)
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the graph will be placed (llx, lly, urx, ury)
            // Use fully qualified type to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle graphRect = new Aspose.Pdf.Rectangle(50, 500, 550, 50);

            // Calculate graph dimensions from the rectangle
            double graphWidth = graphRect.URX - graphRect.LLX;   // 500
            double graphHeight = Math.Abs(graphRect.URY - graphRect.LLY); // 450 (absolute value)

            // Create the Graph object with explicit width and height
            Graph graph = new Graph(graphWidth, graphHeight);
            // Position the graph inside the page using the rectangle's lower‑left corner
            graph.Left = graphRect.LLX;
            graph.Top = graphRect.LLY; // Use Top instead of non‑existent Bottom

            // Helper to add a line segment to the graph
            void AddSegment(float x0, float y0, float x1, float y1, Aspose.Pdf.Color color)
            {
                // PositionArray expects points in the order: x0, y0, x1, y1, ...
                Line line = new Line(new float[] { x0, y0, x1, y1 });
                line.GraphInfo.Color = color;
                line.GraphInfo.LineWidth = 2f; // optional thickness
                graph.Shapes.Add(line);
            }

            // Example data: three line segments with different colors
            // Segment 1: (0,0) to (10,20) - Red
            AddSegment(0, 0, 10, 20, Aspose.Pdf.Color.Red);
            // Segment 2: (10,20) to (20,10) - Green
            AddSegment(10, 20, 20, 10, Aspose.Pdf.Color.Green);
            // Segment 3: (20,10) to (30,30) - Blue
            AddSegment(20, 10, 30, 30, Aspose.Pdf.Color.Blue);

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the document as PDF (no SaveOptions needed for PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with continuous colored line segments saved to '{outputPath}'.");
    }
}
