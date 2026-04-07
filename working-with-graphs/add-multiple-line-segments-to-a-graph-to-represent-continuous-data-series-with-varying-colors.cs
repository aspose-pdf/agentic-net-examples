using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a graph container (width, height) – use double literals as required by the non‑obsolete constructor
            Graph graph = new Graph(500.0, 300.0);

            // Add multiple line segments with varying colors
            AddLineToGraph(graph, new float[] { 50, 250, 150, 150 }, Color.Red);   // Segment 1
            AddLineToGraph(graph, new float[] { 150, 150, 250, 200 }, Color.Green); // Segment 2
            AddLineToGraph(graph, new float[] { 250, 200, 350, 100 }, Color.Blue);  // Segment 3

            // Attach the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF – guard the call on non‑Windows platforms where libgdiplus may be missing
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF with graph saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("Running on a non‑Windows platform. libgdiplus is required for Graph rendering and PDF saving.");
                Console.WriteLine("Skipping doc.Save(...) to avoid TypeInitializationException.");
            }
        }
    }

    // Helper to create a line shape, set its color, and add it to the graph
    static void AddLineToGraph(Graph graph, float[] points, Color lineColor)
    {
        // Position array defines the line's start and end points (x1, y1, x2, y2, ...)
        Line line = new Line(points);
        line.GraphInfo = new GraphInfo
        {
            Color = lineColor,
            LineWidth = 2f // float literal as recommended
        };
        graph.Shapes.Add(line);
    }
}
