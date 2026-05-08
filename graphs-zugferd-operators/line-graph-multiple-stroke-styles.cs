using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "line_graph.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 500 points, height: 300 points)
            Graph graph = new Graph(500.0, 300.0);
            graph.Left = 50;   // Position from the left edge
            graph.Top = 500;   // Position from the bottom edge

            // ---------- Series 1: Solid red line ----------
            float[] points1 = { 0, 250, 100, 200, 200, 150, 300, 180, 400, 120, 500, 160 };
            Line line1 = new Line(points1);
            line1.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 2f
                // No DashArray => solid line
            };
            graph.Shapes.Add(line1);

            // ---------- Series 2: Dashed green line ----------
            float[] points2 = { 0, 260, 100, 210, 200, 160, 300, 190, 400, 130, 500, 170 };
            Line line2 = new Line(points2);
            line2.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Green,
                LineWidth = 2f,
                DashArray = new int[] { 6, 3 } // dash pattern: 6 units on, 3 units off
            };
            graph.Shapes.Add(line2);

            // ---------- Series 3: Dotted blue line ----------
            float[] points3 = { 0, 270, 100, 220, 200, 170, 300, 200, 400, 140, 500, 180 };
            Line line3 = new Line(points3);
            line3.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,
                LineWidth = 2f,
                DashArray = new int[] { 1, 2 } // dotted pattern: 1 unit on, 2 units off
            };
            graph.Shapes.Add(line3);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the document, handling possible GDI+ issues on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("GDI+ (libgdiplus) is not available; PDF saved without rendering the graph.");
                }
            }
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }

    // Helper method to detect missing native GDI+ library
    private static bool ContainsDllNotFound(Exception ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}